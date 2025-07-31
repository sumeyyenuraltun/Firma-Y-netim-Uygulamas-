using Microsoft.AspNetCore.Identity;

namespace FirmaYonetimWeb.Models
{
    public class AppRole: IdentityRole<int>
    {
        public string? Aciklama { get; set; }
    }
}
