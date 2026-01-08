using CursosDev.Domain.Entities;

namespace CursosDev.Application.Services
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
