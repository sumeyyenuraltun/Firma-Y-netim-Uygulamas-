using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfBelediyeKaynakRepository: IBelediyeKaynakRepository
    {
        private readonly DataContext _context;

        public EfBelediyeKaynakRepository (DataContext context)
        {
            _context = context;
        }

        public void BelediyeKaynakEkle(BelediyeKaynak belediyeKaynak)
        {
            _context.BelediyeKaynakları.Add(belediyeKaynak);
            _context.SaveChanges();
        }
    }
}
