using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FirmaYonetimWeb.Data;
using FirmaYonetimWeb.Models;
using FirmaYonetimWeb.Repositories;
using System;
using System.Collections.Generic;
using NuGet.Packaging.Signing;


namespace FirmaYonetimWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBelediyeRepository _belediyeRepository;
        private readonly IGeoServerRepository _geoServerRepository;
        private readonly IKaynakGirisRepository _kaynakGirisRepo;
        private readonly IVPNRepository _VPNRepository;
        private readonly INotRepository _notRepository;
        private readonly IRDPRepository _rdpRepository;
        private readonly IAnyRepository _anyRepository;
        private readonly IPostrgreRepository _postrgreRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, IBelediyeRepository belediyeRepository, INotRepository notRepository,IGeoServerRepository geoServerRepository, IKaynakGirisRepository kaynakGirisRepository, DataContext context, IRDPRepository RDPRepository, IVPNRepository VPNRepository, IAnyRepository anyRepository, IPostrgreRepository postrgreRepository, IServiceRepository serviceRepository)
        {
            _logger = logger;
            _belediyeRepository = belediyeRepository;
            _notRepository = notRepository;
             _context = context;
            _geoServerRepository = geoServerRepository;
            _kaynakGirisRepo = kaynakGirisRepository;
            _VPNRepository = VPNRepository;
            _rdpRepository = RDPRepository;
            _anyRepository = anyRepository;
            _serviceRepository = serviceRepository;
            _postrgreRepository = postrgreRepository;

        }

        [HttpGet]
        public IActionResult Index(int selectedBelediyeId)
        {
            var model = new HomeView
            {
                SelectedBelediyeId = selectedBelediyeId,
                Belediyeler = _belediyeRepository.Belediyeler.ToList(),
                BelediyeNotlari = _belediyeRepository.BelediyeNotlari(selectedBelediyeId),
                BelediyeVPNKayitlari = _belediyeRepository.BelediyeVpnKayitlari(selectedBelediyeId),
                BelediyeServiceKayitlari = _belediyeRepository.BelediyeServiceKayitlari(selectedBelediyeId),
                BelediyeRDPKayitlari = _belediyeRepository.BelediyeRdpKayitlari(selectedBelediyeId),
                BelediyeOpenVPNKayitlari = _belediyeRepository.BelediyeOpenVpnKayitlari(selectedBelediyeId),
                BelediyeFortiKayitlari = _belediyeRepository.BelediyeFortiKayitlari(selectedBelediyeId),
                BelediyeWindowsVpnKaynaklari = _belediyeRepository.BelediyeWindowsVpnKayitlari(selectedBelediyeId),
                BelediyePostgreKayitlari = _belediyeRepository.BelediyePostgreKayitlari(selectedBelediyeId),
                BelediyeAnyKayitlari = _belediyeRepository.BelediyeAnyKayitlari(selectedBelediyeId),
                BelediyeServiceMegsisKayitlari = _belediyeRepository.BelediyeServiceMegsis(selectedBelediyeId),
                BelediyeServiceMaksKayitlari = _belediyeRepository.BelediyeServiceMaks(selectedBelediyeId),
                BelediyeGeoServerKayitlari = _belediyeRepository.BelediyeGeoServerKayitlari(selectedBelediyeId),

            };
               
            return View("Index", model);
        }



        [HttpPost]
       
        public IActionResult NotEkleVeyaGuncelle(int selectedBelediyeId, string aciklama, int? noteId)
        {
            if (noteId.HasValue && noteId.Value > 0)
            {
             
                var existing = _context.Notlar.FirstOrDefault(n => n.Id == noteId.Value);
                if (existing != null)
                {
                    existing.Aciklama = aciklama;
                    existing.UpdatedAt = DateTime.UtcNow;
                    _context.Notlar.Update(existing);
                }
            }
            else
            {
                var yeniNot = new Not
                {
                    BelediyeId = selectedBelediyeId,
                    Aciklama = aciklama,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                _context.Notlar.Add(yeniNot);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", new { selectedBelediyeId = selectedBelediyeId });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GoWizard(int selectedBelediyeId)
        {
            var model = new WizardView
            {
                SelectedBelediyeId = selectedBelediyeId
            };
            return View("Wizard", model);
        }

        [HttpGet]
        public IActionResult GetAuditLogs()
        {
            var gecmis = _context.AuditLogs
                .Where(m => m.Type == "Update")
                .OrderByDescending(m => m.DateTime)
                .Take(50)
                .ToList();

            return View("AuditLog",gecmis);
        }


        [HttpGet]
        public IActionResult GetHistory(string tableName, string primaryKey)
        {
            
                var primaryKeyJson = $"{{\"Id\":{primaryKey}}}";

                var logs = _context.AuditLogs
                    .Where(log => log.TableName == tableName && log.PrimaryKey == primaryKeyJson)
                    .OrderByDescending(log => log.DateTime)
                    .ToList();

              
                return PartialView("AuditLog", logs);
            
            
        }
        [HttpPost]

        public IActionResult UpdateVpn(ViewModel model,int selectedBelediyeId)
        {
            if (model.kaynakGirisVm != null)
            {
                foreach(var item in model.kaynakGirisVm)
                {
                    var kaynakGiris = _context.KaynakGirisleri.Find(item.Id);

                    if (kaynakGiris != null)
                    {
                        if(kaynakGiris.KullaniciAdi != item.KullaniciAdi || kaynakGiris.sifre!= item.sifre)
                        {
                            kaynakGiris.KullaniciAdi = item.KullaniciAdi;
                            kaynakGiris.sifre = item.sifre;
                            kaynakGiris.UpdatedAt = DateTime.UtcNow;
                        }
                       
                    }
                }
            }

            if (model.VpnVm != null)
            {
                foreach (var item in model.VpnVm)
                {
                    var vpn = _context.VPNs.Find(item.Id);
                    if (vpn != null)
                    {
                        if(vpn.IP !=item.IP || vpn.Dogrulama !=item.Dogrulama || vpn.Host !=item.Host || vpn.Port != item.Port)
                        {
                            vpn.IP = item.IP;
                            vpn.Dogrulama = item.Dogrulama;
                            vpn.UpdatedAt = DateTime.UtcNow;
                            vpn.Host = item.Host;
                            vpn.Port = item.Port;
                        }
                        
                    }

                }
            }

            if (model.AnysVm != null)
            {
                foreach (var item in model.AnysVm)
                {
                    var any = _context.Anys.Find(item.Id);
                    if (any != null)
                    {
                        if(any.Kod != item.Kod)
                        {
                            any.Kod = item.Kod;
                            any.UpdatedAt = DateTime.UtcNow;
                        }
                      
                    }
                }
            }

            if(model.ServiceVm != null)
            {
                foreach(var item in model.ServiceVm)
                {
                    var service = _context.Services.Find(item.Id);
                    if(service!= null)
                    {
                        if(service.Endpoint != item.Endpoint || service.ServiceName != item.ServiceName)
                        {
                            service.Endpoint = item.Endpoint;
                            service.ServiceName = item.ServiceName;
                            service.UpdatedAt = DateTime.UtcNow;
                        }
                        
                    }

                }
            }

            if (model.RDPVm != null)
            {
                foreach (var item in model.RDPVm)
                {
                    var rdp = _context.RDPs.Find(item.Id);
                    if (rdp != null)
                    {
                        if(rdp.IP !=item.IP || rdp.VPN != item.VPN)
                        {
                            rdp.IP = item.IP;
                            rdp.VPN = item.VPN;
                            rdp.UpdatedAt = DateTime.UtcNow;
                        }
                     
                    }
                }
            }

            if (model.PostgreSQLVm != null)
            {
                foreach (var item in model.PostgreSQLVm)
                {
                    var postrgre = _context.PostrgreSQLs.Find(item.Id);
                    if (postrgre != null)
                    {
                        if(postrgre.IP !=item.IP || postrgre.Port != item.Port)
                        {
                            postrgre.IP = item.IP;
                            postrgre.Port = item.Port;
                            postrgre.UpdatedAt = DateTime.UtcNow;
                        }
                       
                    }
                }
            }

            if (model.GeoServerVm != null)
            {
                foreach(var item in model.GeoServerVm)
                {
                    var geoServer = _context.GeoServers.Find(item.Id);
                    if(geoServer.IP != item.IP || geoServer.Port != item.Port){
                        geoServer.IP = item.IP;
                        geoServer.Port = item.Port;
                        geoServer.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }



            _context.SaveChanges();
            return RedirectToAction("Index", new { selectedBelediyeId = selectedBelediyeId });
        }


        [HttpPost]
        public IActionResult SilKayitlar(string kartTipi, string ids)
        {
            try
            {
                if (string.IsNullOrEmpty(ids))
                {
                    return Json(new { success = false, message = "Seçili kayýt yok" });
                }

                var idList = ids.Split(',').Select(int.Parse).ToList();

                switch (kartTipi)
                {
                    case "GeoServer":
                        foreach (var id in idList)
                        {

                            _geoServerRepository.GeoServerSil(id);
                            _kaynakGirisRepo.KaynakGirisSil(id);
                        }
                       
                        break;

                    case "VPN":
                        foreach (var id in idList)
                        {
                            
                                _VPNRepository.VPNSil(id);
                                _kaynakGirisRepo.KaynakGirisSil(id);
                            
                        }
                        break;

                    case "RDP":
                        foreach (var id in idList)
                        {
                            
                                _rdpRepository.RDPSil(id);
                                _kaynakGirisRepo.KaynakGirisSil(id);
                             
                            
                        }
                        break;


                    case "AnyDesk":
                        foreach (var id in idList)
                        {

                            _anyRepository.AnySil(id);
                            _kaynakGirisRepo.KaynakGirisSil(id);

                        }
                        break;
                    case "PostgreSQL":
                        foreach (var id in idList)
                        {
                            _postrgreRepository.PostgreSil(id);
                            _kaynakGirisRepo.KaynakGirisSil(id);
                        }
                        break;
                    case "Services":
                        foreach (var id in idList)
                        {
                            _serviceRepository.ServiceSil(id);
                            _kaynakGirisRepo.KaynakGirisSil(id);
                        }
                        break;

                    default:
                        return Json(new { success = false, message = "Bilinmeyen kart tipi" });
                }

                return Json(new { success = true, message = kartTipi + " kayýtlarý silindi" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
