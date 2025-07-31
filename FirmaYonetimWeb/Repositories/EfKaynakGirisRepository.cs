using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfKaynakGirisRepository: IKaynakGirisRepository
    {
        private readonly DataContext _context;

        public EfKaynakGirisRepository(DataContext context)
        {
            _context = context;

        }

        public void KaynakGirisEkle(KaynakGiris kaynakGiris)
        {
            _context.KaynakGirisleri.Add(kaynakGiris);
            _context.SaveChanges();
        }

        public void KaynakGirisSil(int id)
        {
            var kaynakGiris = _context.KaynakGirisleri.Find(id);
            if (kaynakGiris != null)
            {
                _context.KaynakGirisleri.Remove(kaynakGiris);
                _context.SaveChanges();
            }
           
        }
    }
}
