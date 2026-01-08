FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["CursosDev.Api/CursosDev.Api.csproj", "CursosDev.Api/"]
RUN dotnet restore "CursosDev.Api/CursosDev.Api.csproj"

COPY . .
WORKDIR "/src/CursosDev.Api"
RUN dotnet build "CursosDev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CursosDev.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CursosDev.Api.dll"]