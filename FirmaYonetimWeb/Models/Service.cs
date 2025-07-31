namespace FirmaYonetimWeb.Models
{
    public class Service: BaseModel
    {
   
        public int KaynakGirisId { get; set; }
        public string Endpoint { get; set; }

        public string ServiceName { get; set; }

        public KaynakGiris kaynakGiris { get; set; }
    }
}
