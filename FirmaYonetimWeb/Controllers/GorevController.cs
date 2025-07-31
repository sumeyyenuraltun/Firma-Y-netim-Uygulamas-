using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Controllers
{
    public class GorevController : Controller
    {

        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;

        public GorevController(DataContext context, UserManager<AppUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }
        public JsonResult GetPersonelByBelediye(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return Json(new List<object>()); 
            }

            var personeller = _context.BelediyePersonelleri
                .Where(p => p.belediyeId == id.Value)
                .Select(p => new
                {
                    id = p.Id,
                    fullName = p.FullName
                })
                .ToList();

            return Json(personeller);
        }




        [Authorize(Roles = "Admin")]
        public IActionResult CreateGorev(int selectedBelediyeId)
        {
            var model = new BelediyeGorevModel
            {
                gorev = new Gorev(),
                Belediyeler = _context.Belediyeler.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGorev(Gorev gorev,List<IFormFile?> dosyalar)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var dosyaAdlari = new List<string>();
                var newGorev = new Gorev
                {
                    Talep = gorev.Talep,
                    GorevTamamlanmaTarihi = null,
                    GorevGelmeTarihi = DateTime.UtcNow,
                    IslemeAlinmaTarihi = null,
                    GorevDurumu = "Beklemede",
                    CreatedAt = DateTime.UtcNow,
                    BelediyeId = gorev.BelediyeId,
                    GorevOlusturanId = user.Id,
                    GoreveAtananId = null,
                    OnemDerecesi = gorev.OnemDerecesi,
                    Not = gorev.Not,
                    BelediyePersonelId = gorev.BelediyePersonelId,

                };


                if (dosyalar != null && dosyalar.Any())
                {
                    foreach (var dosya in dosyalar)
                    {
                        if (dosya != null && dosya.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await dosya.CopyToAsync(stream);
                            }

                            dosyaAdlari.Add(fileName);
                        }
                    }
                }
                newGorev.DosyaAdi = dosyaAdlari;

                _context.Gorevler.Add(newGorev);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GorevList", "Gorev");
        }

        public IActionResult GorevFiltre()
        {
            var model = new BelediyeGorevModel
            {
                Belediyeler = _context.Belediyeler.ToList(),
                gorev = new Gorev(),
                Gorevler = _context.Gorevler
                              .Include(g => g.GorevOlusturan)
                              .OrderByDescending(g => g.CreatedAt)
                              .ToList(),
                Kullanicilar = _context.Kullanicilar.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()

            };
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> GorevFiltreAsync(BelediyeGorevModel belediyeGorevModel, string returnView)
        {
            List<Gorev> gorevler;
            var user = await _userManager.GetUserAsync(User);
            if (returnView == "GorevList")
            {
                if (belediyeGorevModel.SelectedBelediyeId == 0 && string.IsNullOrEmpty(belediyeGorevModel.SelectedGorevDurumu) && string.IsNullOrEmpty(belediyeGorevModel.SelectedOnemDerecesi))
                {
                    gorevler = _context.Gorevler.Include(g => g.GorevOlusturan)
                     .OrderByDescending(g => g.CreatedAt)
                     .ToList();
                }
                else
                {
                    gorevler = _context.Gorevler
                    .Include(g => g.GorevOlusturan)
                    .Where(m => (m.BelediyeId == belediyeGorevModel.SelectedBelediyeId || belediyeGorevModel.SelectedBelediyeId == 0) &&
                                 (m.GorevDurumu == belediyeGorevModel.SelectedGorevDurumu || string.IsNullOrEmpty(belediyeGorevModel.SelectedGorevDurumu)) &&
                                 (m.OnemDerecesi == belediyeGorevModel.SelectedOnemDerecesi || string.IsNullOrEmpty(belediyeGorevModel.SelectedOnemDerecesi)))
                    .OrderByDescending(g => g.CreatedAt)
                    .ToList();
                }
                belediyeGorevModel.Kullanicilar = _context.Kullanicilar.ToList();
                belediyeGorevModel.Belediyeler = _context.Belediyeler.ToList();
                belediyeGorevModel.BelediyePersonelleri = _context.BelediyePersonelleri.ToList();
                belediyeGorevModel.Gorevler = gorevler;

                return View("GorevList", belediyeGorevModel);
            }
            else
            {
                if (belediyeGorevModel.SelectedBelediyeId == 0 && string.IsNullOrEmpty(belediyeGorevModel.SelectedGorevDurumu) && string.IsNullOrEmpty(belediyeGorevModel.SelectedOnemDerecesi))
                {
                    gorevler = _context.Gorevler.Include(g => g.GorevOlusturan).Where(m=> m.GoreveAtananId==user.Id)
                     .OrderByDescending(g => g.CreatedAt)
                     .ToList();
                }
                else
                {
                    gorevler = _context.Gorevler
                    .Include(g => g.GorevOlusturan)
                    .Where(m => (m.BelediyeId == belediyeGorevModel.SelectedBelediyeId || belediyeGorevModel.SelectedBelediyeId == 0) &&
                                 (m.GorevDurumu == belediyeGorevModel.SelectedGorevDurumu || string.IsNullOrEmpty(belediyeGorevModel.SelectedGorevDurumu)) &&
                                 (m.OnemDerecesi == belediyeGorevModel.SelectedOnemDerecesi || string.IsNullOrEmpty(belediyeGorevModel.SelectedOnemDerecesi)) &&
                                 (m.GoreveAtananId==user.Id))
                    .OrderByDescending(g => g.CreatedAt)
                    .ToList();
                }
                belediyeGorevModel.Kullanicilar = _context.Kullanicilar.ToList();
                belediyeGorevModel.Belediyeler = _context.Belediyeler.ToList();
                belediyeGorevModel.BelediyePersonelleri = _context.BelediyePersonelleri.ToList();
                belediyeGorevModel.Gorevler = gorevler;

                return View("UserGorevList", belediyeGorevModel);
            }

        }


        [HttpGet]

        public IActionResult GorevList()
        {
            var model = new BelediyeGorevModel
            {
                Belediyeler = _context.Belediyeler.ToList(),
                gorev = new Gorev(),
                Gorevler = _context.Gorevler
                              .Include(g => g.GorevOlusturan)
                              .OrderByDescending(g => g.CreatedAt)
                              .ToList(),
                Kullanicilar = _context.Kullanicilar.ToList(), 
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()
            };
            return View(model);
        }

        [HttpPost]

        public IActionResult GorevList(BelediyeGorevModel belediyeGorevModel)
        {
            List<Gorev> gorevler;

            if (belediyeGorevModel.SelectedBelediyeId == 0)
            {
                gorevler = _context.Gorevler.Include(g => g.GorevOlusturan)
                 .OrderByDescending(g => g.CreatedAt)
                 .ToList();
            }
            else
            {
                gorevler = _context.Gorevler
                .Include(g => g.GorevOlusturan)
                .Where(m => m.BelediyeId == belediyeGorevModel.SelectedBelediyeId)
                .OrderByDescending(g => g.CreatedAt)
                .ToList();
            }
            belediyeGorevModel.Kullanicilar = _context.Kullanicilar.ToList();
            belediyeGorevModel.Belediyeler = _context.Belediyeler.ToList();
            belediyeGorevModel.BelediyePersonelleri = _context.BelediyePersonelleri.ToList();
            belediyeGorevModel.Gorevler = gorevler;
            return View(belediyeGorevModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteGorev(int id, string ReturnView)
        {
            var gorev = _context.Gorevler.Find(id);
            _context.Remove(gorev);
            _context.SaveChanges();
            if (ReturnView == "GorevList")
            {
                return RedirectToAction("GorevList");
            }
            else
            {
                return RedirectToAction("UserGorevList");
            }

        }

        [HttpGet]
        public IActionResult UpdateGorev(int id)
        {
            var gorev = _context.Gorevler.Find(id);
            if (gorev == null)
            {
                return NotFound();
            }

            var model = new BelediyeGorevModel
            {
                gorev = gorev,
                Belediyeler = _context.Belediyeler.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateGorev(Gorev gorev, List<IFormFile?> dosyalar, string ReturnView)
        {
           
                var user = await _userManager.GetUserAsync(User);

                var existingGorev = await _context.Gorevler.FirstOrDefaultAsync(g => g.Id == gorev.Id);
                if (existingGorev != null)
                {

                    if (!string.IsNullOrEmpty(gorev.OnemDerecesi)) //böyle yapıyoruz çünkü selcetten gelen verilen "" boş string olarak gözükebiliyor sadece nul yetmiyor
                    {
                        existingGorev.OnemDerecesi = gorev.OnemDerecesi;

                    }
                   
                    if (!string.IsNullOrEmpty(gorev.GorevDurumu))
                    {
                        existingGorev.GorevDurumu = gorev.GorevDurumu;
                    }
                    if (!string.IsNullOrEmpty(gorev.Not))
                    {
                        existingGorev.Not = gorev.Not;
                    }

                    if (gorev.GorevDurumu == "İşleme Alındı")
                    {
                        existingGorev.IslemeAlinmaTarihi = DateTime.UtcNow;
                        existingGorev.GorevTamamlanmaTarihi = null;
                    }
                    else if (gorev.GorevDurumu == "Tamamlandı")
                    {
                        existingGorev.GorevTamamlanmaTarihi = DateTime.UtcNow;
                    }
                    else if (gorev.GorevDurumu == "Beklemede")
                    {
                        existingGorev.IslemeAlinmaTarihi = null;
                        existingGorev.GorevTamamlanmaTarihi = null;
                    }
                    if (await _userManager.IsInRoleAsync(user, "Admin")&&gorev.GoreveAtananId!=null)
                    {
                        existingGorev.GoreveAtananId = gorev.GoreveAtananId;
                    }

                    if (dosyalar != null && dosyalar.Any())
                    {

                        if (existingGorev.DosyaAdi != null && existingGorev.DosyaAdi.Count > 0)
                        {

                            foreach (var eskiDosya in existingGorev.DosyaAdi)
                            {
                                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", eskiDosya);
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                                existingGorev.DosyaAdi.Clear();
                            }

                        }

                        var yeniDosyalar = new List<string>();
                        foreach (var dosya in dosyalar)
                        {
                            if (dosya != null && dosya.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dosya.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await dosya.CopyToAsync(stream);
                                }
                                yeniDosyalar.Add(fileName);
                            }
                        }
                        existingGorev.DosyaAdi = yeniDosyalar;
                    }

                    _context.Gorevler.Update(existingGorev);
                    await _context.SaveChangesAsync();
                }

            

            if (ReturnView == "GorevList")
            {
                return RedirectToAction("GorevList");
            }
            else
            {
                return RedirectToAction("UserGorevList");
            }
}

        public async Task<IActionResult> UserGorevList()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new BelediyeGorevModel
            {
                Gorevler = _context.Gorevler
                    .Include(g => g.GorevOlusturan)
                    .Where(g => g.GoreveAtananId == user.Id)
                    .OrderByDescending(g => g.CreatedAt)
                    .ToList(),
                Belediyeler = _context.Belediyeler.ToList(),
                Kullanicilar = _context.Kullanicilar.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UserGorevList(BelediyeGorevModel belediyeGorevModel)
        {
            var user = await _userManager.GetUserAsync(User);

            belediyeGorevModel.Gorevler = _context.Gorevler
                    .Include(g => g.GorevOlusturan)
                    .Where(g => g.GoreveAtananId == user.Id)
                    .OrderByDescending(g => g.CreatedAt)
                    .ToList();
            belediyeGorevModel.Belediyeler = _context.Belediyeler.ToList();
            belediyeGorevModel.Kullanicilar = _context.Kullanicilar.ToList();
            belediyeGorevModel.BelediyePersonelleri = _context.BelediyePersonelleri.ToList();
           
            return View(belediyeGorevModel);
        }

        public IActionResult GetPersonelBilgi(BelediyeGorevModel belediyeGorevModel)
        {
            var personel =_context.BelediyePersonelleri
                .Where(p=> p.belediyeId == belediyeGorevModel.SelectedBelediyeId)
                .ToList();

            belediyeGorevModel.BelediyePersonelleri = personel;
            return RedirectToAction("GorevList", belediyeGorevModel);
        }

    }
}
