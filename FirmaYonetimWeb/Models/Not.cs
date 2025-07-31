namespace FirmaYonetimWeb.Models
{
    public class Not: BaseModel
    {

        public int BelediyeId { get; set; }
        public string? Aciklama { get; set; }

        public Belediye Belediye { get; set; }
    }
}
