using System.Text;
using CursosDev.Application.Services;
using CursosDev.Application.UseCases;
using CursosDev.Domain.Entities;
using CursosDev.Domain.Ports.In;
using CursosDev.Domain.Ports.Out;
using CursosDev.Infrastructure;
using CursosDev.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger/OpenAPI with JWT Bearer support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CursosDev API",
        Version = "v1",
        Description = "API para gestión de cursos y lecciones con autenticación JWT"
    });

    // Add JWT Bearer authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT. Ejemplo: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// JWT Service
builder.Services.AddScoped<IJwtService, JwtService>();

// Repositories
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();

// Course Use Cases
builder.Services.AddScoped<IGetCourses, GetCoursesUseCase>();
builder.Services.AddScoped<IGetCourseById, GetCourseByIdUseCase>();
builder.Services.AddScoped<ICreateCourse, CreateCourseUseCase>();
builder.Services.AddScoped<IUpdateCourse, UpdateCourseUseCase>();
builder.Services.AddScoped<IPublishCourse, PublishCourseUseCase>();
builder.Services.AddScoped<IUnpublishCourse, UnpublishCourseUseCase>();
builder.Services.AddScoped<IDeleteCourse, DeleteCourseUseCase>();

// Lesson Use Cases
builder.Services.AddScoped<IGetLessons, GetLessonsUseCase>();
builder.Services.AddScoped<IGetLessonById, GetLessonByIdUseCase>();
builder.Services.AddScoped<IGetLessonsByCourseId, GetLessonsByCourseIdUseCase>();
builder.Services.AddScoped<ICreateLesson, CreateLessonUseCase>();
builder.Services.AddScoped<IUpdateLesson, UpdateLessonUseCase>();
builder.Services.AddScoped<IDeleteLesson, DeleteLessonUseCase>();

var app = builder.Build();

// Apply pending migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count());
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully.");
        }
        else
        {
            logger.LogInformation("Database is up to date. No migrations to apply.");
        }

        // Seed Test User
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var defaultUser = await userManager.FindByEmailAsync("test@cursosdev.com");
        if (defaultUser == null)
        {
            logger.LogInformation("Seeding default test user...");
            var user = new ApplicationUser
            {
                UserName = "test@cursosdev.com",
                Email = "test@cursosdev.com",
                FullName = "Test User",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Test123!");
            if (result.Succeeded)
            {
                logger.LogInformation("Default test user created successfully.");
            }
            else
            {
                logger.LogError("Failed to create default test user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying migrations or seeding data.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CursosDev API v1");
        options.RoutePrefix = string.Empty; // Swagger en la raíz
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
