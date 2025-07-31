using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IGeoServerRepository
    {
        void GeoServerEkle(GeoServer geoServer);
        void GeoServerSil(int id);
    }
}
