namespace FirmaYonetimWeb.Models
{
    public class ViewModel
    {
        public IEnumerable<KaynakGiris> kaynakGirisVm { get; set; }
        public IEnumerable<VPN> VpnVm { get; set; }

       
        public IEnumerable<Service> ServiceVm { get; set; }
        public IEnumerable<RDP> RDPVm { get; set; }
        public IEnumerable<Any> AnysVm { get; set; }
        public IEnumerable<PostrgreSQL> PostgreSQLVm { get; set; }
        public IEnumerable<GeoServer> GeoServerVm { get; set; }
        public int SelectedBelediyeId { get; set; }


    }
}
