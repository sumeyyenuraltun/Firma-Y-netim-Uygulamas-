using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IKaynakGirisRepository
    {
        void KaynakGirisEkle(KaynakGiris kaynakGiris);

        void KaynakGirisSil(int id);
    }
}
