using Microsoft.AspNetCore.Identity;

namespace FirmaYonetimWeb.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => string.Join(" ", FirstName, LastName);
    }
}
