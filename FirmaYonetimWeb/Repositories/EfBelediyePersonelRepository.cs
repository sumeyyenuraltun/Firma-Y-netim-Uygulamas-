using Microsoft.EntityFrameworkCore;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfBelediyePersonelRepository : IBelediyePersonelRepository
    {
        private readonly DataContext _context;

        public EfBelediyePersonelRepository(DataContext context)
        {
            _context = context;
        }

       
        public void BelediyePersonelEkle(BelediyePersonel belediyePersonel)
        {
            _context.BelediyePersonelleri.Add(belediyePersonel);
            _context.SaveChanges();
        }
    }
}
