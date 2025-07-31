using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Entities
{
    public class CreateUserModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]

        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı boş bırakılamaz.")]
        [Compare("Sifre", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string SifreTekrar { get; set; }

        [Required(ErrorMessage = "Ad boş bırakılamaz.")]
        public string Ad { get; set; }
        [Required(ErrorMessage = "Soyad boş bırakılamaz.")]
        public string Soyad { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }


    }
}
