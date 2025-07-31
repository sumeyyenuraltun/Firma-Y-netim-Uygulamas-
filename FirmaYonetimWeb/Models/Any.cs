namespace FirmaYonetimWeb.Models
{
    public class Any: BaseModel
    {
       
        public string Kod { get; set; }
        public int KaynakGirisId { get; set; }
        public KaynakGiris kaynakGiris { get; set; }
    }
}
