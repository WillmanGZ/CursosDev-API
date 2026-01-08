using Microsoft.AspNetCore.Identity;

namespace CursosDev.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
