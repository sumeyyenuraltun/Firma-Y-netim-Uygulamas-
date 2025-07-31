using System.ComponentModel.DataAnnotations;

namespace FirmaYonetimWeb.Models
{
    public class WizardView : BaseModel

    {
        public int SelectedBelediyeId { get; set; }
        public int SelectedKaynakTuruId { get; set; }

        public int SelectedVPNAltTuruId { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı doldurulması zorunlu.")]
        public string KullaniciAdi { get; set; }
        [Required(ErrorMessage = "Şifre doldurulması zorunlu.")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Host doldurulması zorunlu.")]
        public string Host { get; set; }
        public bool Dogrulama { get; set; }

        [MaxLength(5, ErrorMessage = "Port en fazla 5 hane olabilir.")]
        public string Port { get; set; }

        [Required(ErrorMessage = "Servis adresi doldurulması zorunlu.")]
        public string ServiceEndpoint { get; set; }
        [Required(ErrorMessage = "Servis adı doldurulması zorunlu.")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "IP adresi doldurulması zorunlu.")]
        public string IP { get; set; }

        [Required(ErrorMessage = "Kod alanı boş bırakılamaz.")]
        public string Kod { get; set; }


        public List<Belediye> Belediyeler { get; set; }
        public List<KaynakTuru> KaynakTurleri { get; set; }

        public List<VPNAltTuru> VPNAltTuruleri { get; set; }
    }
}
