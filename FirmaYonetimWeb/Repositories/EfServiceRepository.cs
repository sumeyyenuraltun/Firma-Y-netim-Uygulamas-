using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfServiceRepository : IServiceRepository
    {
        private readonly DataContext _context;

        public EfServiceRepository(DataContext context)
        {
            _context = context;
        }

        public void ServiceEkle(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
        }

        public void ServiceSil(int id)
        {
            var service = _context.Services.FirstOrDefault(m => m.KaynakGirisId == id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
        }
    }
}
