using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IAnyRepository
    {
        void AnyEkle(Any any);
        void AnySil(int id);
    }
}
