namespace FirmaYonetimWeb.Models
{
    public class HomeView
    {
        public int SelectedBelediyeId { get; set; }
    
    
        public List<Belediye> Belediyeler { get; set; }
        public List<KaynakTuru> KaynakTurleri { get; set; }

        public List<BelediyeKaynak> BelediyeKaynaks { get; set; }

        public List<VPNAltTuru> VPNAltTuruleri { get; set; }

        public List<BelediyeKaynak> BelediyeVPNKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeOpenVPNKayitlari { get; set; }
        public List<BelediyeKaynak> BelediyeServiceKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeRDPKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeFortiKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeWindowsVpnKaynaklari { get; set; }

        public List<BelediyeKaynak> BelediyePostgreKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeAnyKayitlari { get; set; }
        public List<BelediyeKaynak> BelediyeServiceMaksKayitlari { get; set; }
        public List <BelediyeKaynak> BelediyeServiceMegsisKayitlari { get; set; }

        public List<BelediyeKaynak> BelediyeGeoServerKayitlari { get; set; }

        public List<Not> BelediyeNotlari { get; set; }

        public List<Audit> AuditLogs { get; set; }
    }
}
