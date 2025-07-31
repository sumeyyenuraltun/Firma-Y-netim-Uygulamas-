using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfGeoServerRepository : IGeoServerRepository
    {
        private readonly DataContext _context;

        public EfGeoServerRepository(DataContext context)
        {
            _context = context;
        }
        public void GeoServerEkle(GeoServer geoServer)
        {
            _context.GeoServers.Add(geoServer);
            _context.SaveChanges();
        }

        public void GeoServerSil(int id)
        {
            var geoServer = _context.GeoServers.FirstOrDefault(m => m.KaynakGirisId == id);

            if (geoServer != null)
            {
                _context.GeoServers.Remove(geoServer);
                _context.SaveChanges();
            }
            
        }
    }
}
