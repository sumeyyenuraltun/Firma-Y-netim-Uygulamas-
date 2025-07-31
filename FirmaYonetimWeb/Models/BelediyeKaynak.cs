namespace FirmaYonetimWeb.Models
{
    public class BelediyeKaynak : BaseModel
    {
      
        public int BelediyeId { get; set; }
        public int KaynakTuruId { get; set; }
        public int? VPNAltTuruId { get; set; } //soru işareti int değer tutabileceği gibi null da olabilri demektir.

        public Belediye belediye { get; set; }
        public KaynakTuru kaynakTuru { get; set; }
        public VPNAltTuru VPNAltTuru { get; set; }

        public ICollection<KaynakGiris> KaynakGirisler { get; set; }
   




    }
}
