using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IBelediyeRepository
    {
       
        IEnumerable<Belediye> Belediyeler { get; set; }

        Belediye GetById(int id);

        public void UpdateBelediye(Belediye belediye);

        public void DeleteBelediye(int id);

        public void CreateBelediye(Belediye belediye);

        List<BelediyeKaynak> BelediyeVpnKayitlari(int id);

        List<BelediyeKaynak> BelediyeServiceKayitlari(int id);

        List<BelediyeKaynak> BelediyeRdpKayitlari(int id);

        List<BelediyeKaynak> BelediyeOpenVpnKayitlari(int id);

        List<BelediyeKaynak> BelediyeFortiKayitlari(int id);

        List<BelediyeKaynak> BelediyeWindowsVpnKayitlari(int id);

        List<BelediyeKaynak> BelediyePostgreKayitlari(int id);

        List<BelediyeKaynak> BelediyeAnyKayitlari(int id);

        List<BelediyeKaynak> BelediyeServiceMegsis(int id);
        List<BelediyeKaynak> BelediyeServiceMaks(int id);
        List<BelediyeKaynak> BelediyeGeoServerKayitlari(int id);

        List<BelediyePersonel> BelediyePersonel(int id);
     
        List<Not> BelediyeNotlari(int id);
     

    }
}
