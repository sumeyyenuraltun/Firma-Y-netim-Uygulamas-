using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfRDPRepository: IRDPRepository
    {
        private readonly DataContext _context;

        public EfRDPRepository(DataContext context)
        {
            _context = context;
        }


        
        public void RDPEkle(RDP rdp)
        {
            _context.RDPs.Add(rdp);
            _context.SaveChanges();  
        }

        public void RDPSil(int id)
        {
           var rdp = _context.RDPs.FirstOrDefault(m => m.KaynakGirisId == id);
            if (rdp != null)
            {
                _context.RDPs.Remove(rdp);
                _context.SaveChanges();
            }
        }
    }
}
