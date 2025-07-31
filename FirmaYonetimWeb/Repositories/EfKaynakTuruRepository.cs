using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfKaynakTuruRepository: IKaynakTuruRepository
    {
        private readonly DataContext _context;

        public EfKaynakTuruRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<KaynakTuru> KaynakTurleri => _context.KaynakTurleri;

        IEnumerable<KaynakTuru>IKaynakTuruRepository.KaynakTurleri { 
            get => _context.KaynakTurleri; set => throw new NotImplementedException();
    }

        public KaynakTuru GetById(int id)
        {
            return _context.KaynakTurleri.Find(id);
        }

        public void UpdateKaynakTuru(KaynakTuru kaynakTuru)
        {
            _context.KaynakTurleri.Update(kaynakTuru);
            _context.SaveChanges();
        }
    }
}
