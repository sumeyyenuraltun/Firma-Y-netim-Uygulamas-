namespace FirmaYonetimWeb.Models
{
    public class VPNAltTuru: BaseModel
    {
        public string Ad { get; set; }

        public ICollection<BelediyeKaynak> BelediyeKaynaklar { get; set; }
    }
}
