using Microsoft.AspNetCore.Localization.Routing;

namespace FirmaYonetimWeb.Models
{
    public class KaynakGiris: BaseModel
    {
     
        public int BelediyeKaynakId { get; set; }
        public string? KullaniciAdi { get; set; }
        public string sifre { get; set; }

        public BelediyeKaynak belediyeKaynak { get; set; }

        public ICollection<VPN> VPNs { get; set; }

        public ICollection<RDP> RDPs { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Any> Anys { get; set; }

        public ICollection<PostrgreSQL> PostrgreSQLs { get; set;}

        public ICollection<GeoServer> GeoServers { get; set; }



    }
}
