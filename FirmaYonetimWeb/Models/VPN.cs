using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Models
{
    public class VPN : BaseModel
    {
        
        public int  KaynakGirisId {get; set;}

        public string? IP {  get; set;}

        public string? Host { get; set; }

        public bool Dogrulama { get; set; }

        [MaxLength(5, ErrorMessage = "Port en fazla 5 hane olabilir.")]
        public string? Port { get; set; }

        public KaynakGiris kaynakGiris {get; set;}

    }
}
