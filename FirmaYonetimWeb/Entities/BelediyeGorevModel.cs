using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Entities
{
    public class BelediyeGorevModel
    {
       
        public Gorev gorev { get; set; }
        public List<Belediye> Belediyeler { get; set; } = new();
        public List<Gorev> Gorevler { get; set; } = new List<Gorev>();

        public int SelectedBelediyeId { get; set; }
        public List<AppUser> Kullanicilar { get; set; } = new List<AppUser>();

        public string SelectedGorevDurumu { get; set; } = string.Empty; // Örnek: "Beklemede", "Devam Ediyor", "Tamamlandı"
        public string SelectedOnemDerecesi { get; set; } = string.Empty; // Örnek: "Düşük", "Orta", "Yüksek"

        public List<BelediyePersonel> BelediyePersonelleri { get; set; } = new List<BelediyePersonel>();
        public int SelectedBelediyePersonel { get; set; }
        public BelediyePersonel belediyePersonel { get; set; }

    }

}
