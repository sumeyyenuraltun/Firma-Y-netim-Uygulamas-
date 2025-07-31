using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfNotRepository : INotRepository
    {

        private readonly DataContext _context;

        public EfNotRepository (DataContext context)
        {
            _context = context;
        }
        public void EkleNot(Not not)
        {
            _context.Notlar.Add(not);
            _context.SaveChanges();
        }

        public void UpdateNot(Not not)
        {
            var existingNot = _context.Notlar.Find(not.Id);
            if (existingNot != null)
            {
                existingNot.Aciklama = not.Aciklama;
                existingNot.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }

    }
}
