namespace FirmaYonetimWeb.Models
{
    public class KaynakTuru: BaseModel
    {
       

        public string Ad {  get; set; }

        public ICollection<BelediyeKaynak> BelediyeKaynaklar { get; set; }
    }
}
