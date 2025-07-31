using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfVPNRepository : IVPNRepository
    {
        private readonly DataContext _context;

        public EfVPNRepository(DataContext context)
        {
            _context = context;
        }

        public void VPNEkle(VPN vpn)
        {
            _context.VPNs.Add(vpn);
            _context.SaveChanges();
        }

        public void VPNSil(int id)
        {
            var vpn = _context.VPNs.FirstOrDefault(m => m.KaynakGirisId == id);
            if (vpn != null)
            {
                _context.VPNs.Remove(vpn);
                _context.SaveChanges();
            }

        }
    }
}
