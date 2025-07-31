using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfPostrgeRepository: IPostrgreRepository
    {
        private readonly DataContext _context;
        public EfPostrgeRepository(DataContext context) { 
            _context = context;
        }

        public void PostgreEkle(PostrgreSQL postrgreSQL)
        {
            _context.PostrgreSQLs.Add(postrgreSQL);
            _context.SaveChanges();
        }

        public void PostgreSil(int id)
        {
            var postrgreSQL = _context.PostrgreSQLs.FirstOrDefault(m => m.KaynakGirisId == id);
            if (postrgreSQL != null)
            {
                _context.PostrgreSQLs.Remove(postrgreSQL);
                _context.SaveChanges();

            }
        }
    }
}
