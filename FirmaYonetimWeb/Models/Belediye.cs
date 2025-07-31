namespace FirmaYonetimWeb.Models
{
    public class Belediye: BaseModel
    {
        
        public string Ad { get; set; }

        public ICollection<BelediyeKaynak> BelediyeKaynaklar {  get; set; }
        public ICollection<BelediyePersonel> BelediyePersoneller { get; set; }


    }
}
