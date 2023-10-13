using Microsoft.AspNetCore.Identity;

namespace PrimeBackend.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
