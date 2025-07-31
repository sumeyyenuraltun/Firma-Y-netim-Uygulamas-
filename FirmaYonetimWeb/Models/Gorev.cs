using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FirmaYonetimWeb.Models
{
    public class Gorev: BaseModel
    {
        [Required(ErrorMessage = "Belediye seçilmeli.")]
        public int BelediyeId { get; set; }
        public Belediye? belediye { get; set; }

        [Required(ErrorMessage = "Talep boş bırakılamaz.")]
        public string Talep { get; set; } = string.Empty;
        public DateTime? GorevGelmeTarihi { get; set; }
        public DateTime? IslemeAlinmaTarihi { get; set; }
        public DateTime? GorevTamamlanmaTarihi { get; set; }
        public string GorevDurumu { get; set; } = string.Empty; // Örnek: "Beklemede", "Devam Ediyor", "Tamamlandı"

        public int? GorevOlusturanId { get; set; } 
        public AppUser? GorevOlusturan { get; set; } 

        public int? GoreveAtananId { get; set; }
        public AppUser? GoreveAtanan { get; set; }

        [Required(ErrorMessage = "Önem derecesi seçilmeli.")]
        public string OnemDerecesi { get; set; } = string.Empty;
         public string? Not { get; set; }
        //    public string PersonelAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Belediye personeli seçilmeli.")]
        public int BelediyePersonelId { get; set; }
         public BelediyePersonel? BelediyePersonel { get; set; }

        [NotMapped] // veritabanına yansımamamsını sağlıyor
        public List<IFormFile>? Dosyalar { get; set; }

     //   public List<string> DosyaAdi { get; set; } =new List<string>();
        public string? DosyaAdiJson { get; set; } = "[]";

        [NotMapped]
        public List<string>? DosyaAdi
        {
            get => string.IsNullOrEmpty(DosyaAdiJson)
                ? null
                : JsonSerializer.Deserialize<List<string>>(DosyaAdiJson);
            set => DosyaAdiJson = value != null ? JsonSerializer.Serialize(value) : null;
        }

    }
}
