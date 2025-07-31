using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IKaynakTuruRepository
    {
        IEnumerable<KaynakTuru> KaynakTurleri { get; set; }

        KaynakTuru GetById(int id);
        public void UpdateKaynakTuru(KaynakTuru kaynakTuru);

    }
}
