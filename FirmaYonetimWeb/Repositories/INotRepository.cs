using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface INotRepository
    {
        void EkleNot(Not not);
        void UpdateNot(Not not);
    }
}
