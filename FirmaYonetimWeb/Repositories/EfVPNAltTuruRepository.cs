using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfVPNAltTuruRepository : IVPNTuruRepository
    {
        private DataContext _context;

        public EfVPNAltTuruRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<VPNAltTuru> VPNAltTurleri => _context.VPNAltTurleri;

        IEnumerable<VPNAltTuru> IVPNTuruRepository.VPNAltTurleri
        {
            get => _context.VPNAltTurleri; set => throw new NotImplementedException();
        }

        public VPNAltTuru GetById(int id)
        {
            return _context.VPNAltTurleri.Find(id);
        }

        public void UpdateVpnTuru(VPNAltTuru VPNAltTuru)
        {
            _context.VPNAltTurleri.Update(VPNAltTuru);
            _context.SaveChanges();
        }
    }
}
