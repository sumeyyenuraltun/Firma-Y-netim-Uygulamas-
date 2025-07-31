using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Entities
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Sifre { get; set; }

        public bool RememberMe { get; set; }
    }
}
