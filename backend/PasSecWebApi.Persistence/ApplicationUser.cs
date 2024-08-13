using Microsoft.AspNetCore.Identity;

namespace PasSecWebApi.Persistence
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
