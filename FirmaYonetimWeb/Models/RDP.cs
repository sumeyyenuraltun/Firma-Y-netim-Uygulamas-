namespace FirmaYonetimWeb.Models
{
    public class RDP : BaseModel
    {
        
        public int KaynakGirisId { get; set; }
        public string IP {  get; set; }

        public bool VPN { get; set; }

        public KaynakGiris kaynakGiris { get; set; }


    }
}
