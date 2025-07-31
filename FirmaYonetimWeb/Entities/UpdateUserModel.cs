using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Entities
{
    public class UpdateUserModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        
        public string KullaniciAdi { get; set; }


        public string? Sifre { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }

    }
}
