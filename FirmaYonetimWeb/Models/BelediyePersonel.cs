using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Models
{
    public class BelediyePersonel: BaseModel
    {
        [Required(ErrorMessage = "Belediye seçimi zorunludur.")]
        public int belediyeId { get; set; }
        public Belediye? belediye { get; set; }

        [Required(ErrorMessage = "Personel adı zorunludur.")]
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
