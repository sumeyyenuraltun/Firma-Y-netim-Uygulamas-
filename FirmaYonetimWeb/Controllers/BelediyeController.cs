using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using FirmaYonetimWeb.Entities;
using FirmaYonetimWeb.Models;
using FirmaYonetimWeb.Repositories;

namespace FirmaYonetimWeb.Controllers
{
    public class BelediyeController : Controller
    {
        private IBelediyeRepository _repository;
        private readonly IBelediyePersonelRepository _belediyePersonel;
        public BelediyeController(IBelediyeRepository repository, IBelediyePersonelRepository belediyePersonel)
        {
            _repository = repository;
            _belediyePersonel = belediyePersonel;
        }
        public IActionResult Index(int selectedBelediyeId)
        {

            var model = new BelediyeGorevModel
            {
                Belediyeler = _repository.Belediyeler.ToList(),
                SelectedBelediyeId = selectedBelediyeId,
                BelediyePersonelleri = _repository.BelediyePersonel(selectedBelediyeId)
            };
            return View("BelediyeList", model);
        }

        [HttpGet]
    
        public IActionResult BelediyeList(int selectedBelediyeId)
        {
            var model = new BelediyeGorevModel
            {
                Belediyeler = _repository.Belediyeler.ToList(),
                SelectedBelediyeId = selectedBelediyeId,
                BelediyePersonelleri =  _repository.BelediyePersonel(selectedBelediyeId)
               
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult BelediyePersonelList(int belediyeId)
        {
            var personeller = _repository.BelediyePersonel(belediyeId);
            return PartialView("PersonelList", personeller);
        }


        public IActionResult Edit(int id)
        {
            ViewBag.ActionMode = "Edit";
            return View(GetById(id));
        }

        [HttpPost]

        public IActionResult Edit(Belediye belediye)
        {
            _repository.UpdateBelediye(belediye);
            return RedirectToAction("BelediyeList");
        }

        public Belediye GetById(int id)
        {
            return _repository.GetById(id);
        }

        [HttpGet]

        public IActionResult WizardBelediyeler()
        {
            var belediyeler = _repository.Belediyeler.ToList();
            return View(belediyeler);
        }

        [HttpPost]

        public IActionResult DeleteBelediye(int id)
        {
            _repository.DeleteBelediye(id);
            return RedirectToAction("BelediyeList");
        }

        public IActionResult CreateBelediye()
        {
            ViewBag.ActionMode = "CreateBelediye";
            return View("Edit");
        }

        [HttpPost]

        public IActionResult CreateBelediye(Belediye belediye)
        {
            _repository.CreateBelediye(belediye);
            return RedirectToAction("BelediyeList");
        }
    }
}
