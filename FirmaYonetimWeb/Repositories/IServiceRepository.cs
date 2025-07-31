using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IServiceRepository
    {
        void ServiceEkle(Service service);
        void ServiceSil(int id);
    }
}
