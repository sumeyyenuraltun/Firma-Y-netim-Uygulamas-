using Microsoft.EntityFrameworkCore;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public class EfBelediyeRepository : IBelediyeRepository
    {
        private DataContext _context;

        public EfBelediyeRepository(DataContext context)
        {
            _context = context;
        }


        public IEnumerable<Belediye> Belediyeler => _context.Belediyeler;

        IEnumerable<Belediye> IBelediyeRepository.Belediyeler { get => _context.Belediyeler; set => throw new NotImplementedException(); }

        public List<BelediyePersonel> BelediyePersonel(int id)
        {
            return _context.BelediyePersonelleri
                .Where(m => m.belediyeId == id)
                .ToList();
        }



        public Belediye GetById(int id)
        {
            return _context.Belediyeler.Find(id);
        }

        public void UpdateBelediye(Belediye belediye)
        {
            var existingBelediye = _context.Belediyeler.Find(belediye.Id);
            if (existingBelediye != null)
            {
                existingBelediye.Ad = belediye.Ad;
                existingBelediye.UpdatedAt = DateTime.UtcNow;
                _context.Belediyeler.Update(existingBelediye);
                _context.SaveChanges();
            }

        }

        public void DeleteBelediye(int id)
        {
            var belediye = _context.Belediyeler.Find(id);
            _context.Belediyeler.Remove(belediye);
            _context.SaveChanges();
        }

        public void CreateBelediye(Belediye belediye)
        {

            var belediyeKayit = new Belediye
            {
                Ad = belediye.Ad,
                CreatedAt = DateTime.UtcNow,
                IsActive = true,

            };
            _context.Belediyeler.Add(belediyeKayit);
            _context.SaveChanges();
        }

        public List<BelediyeKaynak> BelediyeVpnKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 1)
                .Include(m => m.VPNAltTuru)
                .Include(m => m.KaynakGirisler)
                .ToList();
        }

        public List<BelediyeKaynak> BelediyeServiceKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 2)
                .Include(m => m.KaynakGirisler)
                .ThenInclude(x => x.Services)
                .ToList();
        }

        public List<BelediyeKaynak> BelediyeRdpKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 4)
                .Include(m => m.KaynakGirisler)
                .ThenInclude(x => x.RDPs)
                .ToList();
        }

        public List<BelediyeKaynak> BelediyeOpenVpnKayitlari(int id)
        {
            return _context.BelediyeKaynakları
            .Where(m => m.BelediyeId == id && m.KaynakTuruId == 1 && m.VPNAltTuruId == 1)
            .Include(m => m.VPNAltTuru)
            .Include(m => m.KaynakGirisler)
            .ThenInclude(x => x.VPNs)
            .ToList();
        }

        public List<BelediyeKaynak> BelediyeFortiKayitlari(int id)
        {
            return _context.BelediyeKaynakları
             .Where(m => m.BelediyeId == id && m.KaynakTuruId == 1 && m.VPNAltTuruId == 2)
             .Include(m => m.VPNAltTuru)
             .Include(m => m.KaynakGirisler)
             .ThenInclude(x => x.VPNs)
             .ToList();
        }

        public List<BelediyeKaynak> BelediyeWindowsVpnKayitlari(int id)
        {
            return _context.BelediyeKaynakları
            .Where(m => m.BelediyeId == id && m.KaynakTuruId == 1 && m.VPNAltTuruId == 3)
            .Include(m => m.VPNAltTuru)
            .Include(m => m.KaynakGirisler)
            .ThenInclude(x => x.VPNs)
            .ToList();
        }

        public List<BelediyeKaynak> BelediyePostgreKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 5)
                .Include(m => m.KaynakGirisler)
                .ThenInclude(x => x.PostrgreSQLs)
                .ToList();
        }

        public List<BelediyeKaynak> BelediyeAnyKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 3)
                .Include(m => m.KaynakGirisler)
                .ThenInclude(x => x.Anys)
                .ToList();
        }

        public List<BelediyeKaynak> BelediyeGeoServerKayitlari(int id)
        {
            return _context.BelediyeKaynakları.Where(m => m.BelediyeId == id && m.KaynakTuruId == 6)
                .Include(m => m.KaynakGirisler)
                .ThenInclude(x => x.GeoServers)
                .ToList();
        }


        public List<BelediyeKaynak> BelediyeServiceMegsis(int id)
        {
            return _context.BelediyeKaynakları
                .Include(m => m.KaynakGirisler)
                    .ThenInclude(x => x.Services)
                .Where(m => m.BelediyeId == id && m.KaynakTuruId == 2
                    && m.KaynakGirisler.Any(x => x.Services.Any(s => s.ServiceName == "MEGSIS")))
                .ToList();
        }
        public List<BelediyeKaynak> BelediyeServiceMaks(int id)
        {
            return _context.BelediyeKaynakları
                .Include(m => m.KaynakGirisler)
                    .ThenInclude(x => x.Services)
                .Where(m => m.BelediyeId == id && m.KaynakTuruId == 2
                    && m.KaynakGirisler.Any(x => x.Services.Any(s => s.ServiceName == "MAKS")))
                .ToList();
        }

        public List<Not> BelediyeNotlari(int id)
        {
            return _context.Notlar
                .Where(m => m.BelediyeId == id)
                .ToList();
        }

        public List<BelediyePersonel> BelediyePersonelList(int id)
        {
            return _context.BelediyePersonelleri.Where(m => m.belediyeId == id).ToList();
        }


    }
}