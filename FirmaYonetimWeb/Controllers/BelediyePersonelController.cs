using Microsoft.AspNetCore.Mvc;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Controllers
{
    public class BelediyePersonelController : Controller
    {
        private readonly DataContext _context;

        public BelediyePersonelController(DataContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var model = new BelediyeGorevModel
            {
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList(),
                Belediyeler = _context.Belediyeler.ToList()
            };

            return View("Index", model);
        }

        [HttpPost]

        public IActionResult Index(BelediyeGorevModel belediyeGorevModel)
        {
            List<BelediyePersonel> personeller;
            if(belediyeGorevModel.SelectedBelediyeId == 0)
            {
                personeller = _context.BelediyePersonelleri.ToList();
            }
            else
            {
                personeller = _context.BelediyePersonelleri
                    .Where(p => p.belediyeId == belediyeGorevModel.SelectedBelediyeId)
                    .ToList();
            }

            belediyeGorevModel.BelediyePersonelleri = personeller;
            belediyeGorevModel.Belediyeler = _context.Belediyeler.ToList();

            return View("Index", belediyeGorevModel);
        }

        [HttpPost]

        public IActionResult DeletePersonel(int id)
        {
            var personel = _context.BelediyePersonelleri.Find(id);
            if (personel != null)
            {
                _context.BelediyePersonelleri.Remove(personel);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        public IActionResult CreatePersonel()
        {
            var model = new BelediyeGorevModel
            {
                Belediyeler = _context.Belediyeler.ToList(),
                Kullanicilar = _context.Users.ToList(),
                Gorevler = _context.Gorevler.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreatePersonel(BelediyePersonel belediyePersonel)
        {
            if (ModelState.IsValid)
            {
                var newPersonel = new BelediyePersonel
                {
                    belediyeId = belediyePersonel.belediyeId,
                    FullName = belediyePersonel.FullName,
                    Email =belediyePersonel.Email,
                    PhoneNumber = belediyePersonel.PhoneNumber
                };
                _context.BelediyePersonelleri.Add(newPersonel);
                _context.SaveChanges();

            }
            return RedirectToAction("Index","BelediyePersonel");
        }

        public IActionResult EditPersonel(int id)
        {
            var personel = _context.BelediyePersonelleri.Find(id);
            if (personel == null)
            {
                return NotFound();
            }
            var model = new BelediyeGorevModel
            {
                Belediyeler = _context.Belediyeler.ToList(),
                Kullanicilar = _context.Users.ToList(),
                Gorevler = _context.Gorevler.ToList(),
                BelediyePersonelleri = _context.BelediyePersonelleri.ToList(),
                SelectedBelediyePersonel = id
            };

            return RedirectToAction("Index", model);

        }

        [HttpPost]
        public IActionResult EditPersonel(BelediyePersonel personel)
        {
            if (ModelState.IsValid)
            {
                var existingPersonel = _context.BelediyePersonelleri.Find(personel.Id);
                if(existingPersonel != null)
                {
                    existingPersonel.belediyeId = personel.belediyeId;
                    existingPersonel.FullName = personel.FullName;
                    existingPersonel.Email = personel.Email;
                    existingPersonel.PhoneNumber = personel.PhoneNumber;
                    _context.BelediyePersonelleri.Update(existingPersonel);
                    _context.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
    }
}
