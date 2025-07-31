using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IPostrgreRepository
    {
        void PostgreEkle(PostrgreSQL postrgreSQL);
        void PostgreSil(int id);
    }
}
