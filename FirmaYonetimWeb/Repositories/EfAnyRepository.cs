using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfAnyRepository : IAnyRepository
    {
        private readonly DataContext _context;

        public EfAnyRepository (DataContext context)
        {
            _context = context;
        }
        public void AnyEkle(Any any)
        {
            _context.Anys.Add(any);
            _context.SaveChanges();
        }

        public void AnySil(int id)
        {
            var any = _context.Anys.FirstOrDefault(m => m.KaynakGirisId == id);
            if (any != null)
            {
                _context.Anys.Remove(any);
                _context.SaveChanges();
            }
        }
    }
}
