using Microsoft.AspNetCore.Mvc;
using FirmaYonetimWeb.Models;
using FirmaYonetimWeb.Repositories;

namespace FirmaYonetimWeb.Controllers
{
    public class WizardController : Controller
    {

        private readonly IBelediyeRepository _belediyeRepository;
        private readonly IKaynakTuruRepository _kaynakTuruRepository;
        private readonly IVPNTuruRepository _VPNTuruRepository;
        private readonly IKaynakGirisRepository _kaynakGirisRepository;
        private readonly IVPNRepository _VPNRepository;
        private readonly IBelediyeKaynakRepository _belediyeKaynakRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRDPRepository _RDPRepository;
        private readonly IPostrgreRepository _postrgreRepository;
        private readonly IAnyRepository _anyRepository;
        private readonly IGeoServerRepository _geoServerRepository;

        public WizardController(IBelediyeRepository belediyeRepository, IKaynakTuruRepository kaynakTuruRepository, IVPNTuruRepository VPNTuruRepository, IKaynakGirisRepository kaynakGirisRepository, IVPNRepository VPNRepository, IBelediyeKaynakRepository belediyeKaynakRepository, IServiceRepository serviceRepository, IRDPRepository RDPRepository, IPostrgreRepository postrgreRepository, IAnyRepository anyRepository, IGeoServerRepository geoServerRepository)
        {
            _belediyeRepository = belediyeRepository;
            _kaynakTuruRepository = kaynakTuruRepository;
            _VPNTuruRepository = VPNTuruRepository;
            _VPNRepository = VPNRepository;
            _belediyeKaynakRepository = belediyeKaynakRepository;
            _kaynakGirisRepository = kaynakGirisRepository;
            _serviceRepository = serviceRepository;
            _RDPRepository = RDPRepository;
            _postrgreRepository = postrgreRepository;
            _anyRepository = anyRepository;
            _geoServerRepository = geoServerRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var model = new WizardView
            {
                Belediyeler = _belediyeRepository.Belediyeler.ToList(),
                KaynakTurleri = _kaynakTuruRepository.KaynakTurleri.ToList(),
                VPNAltTuruleri = _VPNTuruRepository.VPNAltTurleri.ToList(),
            };

            return View("Wizard",model);
        }

        
        [HttpPost]
        public IActionResult Index(WizardView model, int selectedBelediyeId)
        {


            var belediyeKaynak = new BelediyeKaynak
            {
                BelediyeId = model.SelectedBelediyeId,
                KaynakTuruId = model.SelectedKaynakTuruId,
                CreatedAt = DateTime.UtcNow,
                VPNAltTuruId = model.SelectedKaynakTuruId == 1 ? model.SelectedVPNAltTuruId : null,


            };

            _belediyeKaynakRepository.BelediyeKaynakEkle(belediyeKaynak);

            var kaynakGiris = new KaynakGiris
            {
                BelediyeKaynakId = belediyeKaynak.Id,
                KullaniciAdi = model.KullaniciAdi,
                sifre = model.Sifre,
                CreatedAt = DateTime.UtcNow,

            };

            _kaynakGirisRepository.KaynakGirisEkle(kaynakGiris);

            if (model.SelectedKaynakTuruId == 1 && !string.IsNullOrEmpty(model.IP))
            {

                var vpn = new VPN
                {
                    KaynakGirisId = kaynakGiris.Id,
                    IP = model.IP,
                    Host = model.Host,
                    Dogrulama = model.Dogrulama,
                    Port = model.Port,
                    CreatedAt = DateTime.UtcNow,

                };

                _VPNRepository.VPNEkle(vpn);
            }

            else if (model.SelectedKaynakTuruId == 2 && !string.IsNullOrEmpty(model.ServiceEndpoint) && !string.IsNullOrEmpty(model.ServiceName))
            {
                var service = new Service
                {
                    KaynakGirisId = kaynakGiris.Id,
                    Endpoint = model.ServiceEndpoint,
                    ServiceName = model.ServiceName,
                    CreatedAt = DateTime.UtcNow,


                };

                _serviceRepository.ServiceEkle(service);
            }

            else if (model.SelectedKaynakTuruId == 4 && !string.IsNullOrEmpty(model.IP))
            {
                var rdp = new RDP
                {
                    KaynakGirisId = kaynakGiris.Id,
                    IP = model.IP,
                    CreatedAt = DateTime.UtcNow,
                    VPN = model.Dogrulama,
                };

                _RDPRepository.RDPEkle(rdp);

            }

            else if (model.SelectedKaynakTuruId == 5 && !string.IsNullOrEmpty(model.IP) && !string.IsNullOrEmpty(model.Port))
            {
                var postgre = new PostrgreSQL
                {
                    KaynakGirisId = kaynakGiris.Id,
                    IP = model.IP,
                    Port = model.Port,
                    CreatedAt = DateTime.UtcNow,


                };

                _postrgreRepository.PostgreEkle(postgre);
            }

            else if (model.SelectedKaynakTuruId == 6 && !string.IsNullOrEmpty(model.IP) && !string.IsNullOrEmpty(model.Port))
            {
                var geoServer = new GeoServer
                {
                    KaynakGirisId = kaynakGiris.Id,
                    IP = model.IP,
                    Port = model.Port,
                    CreatedAt = DateTime.UtcNow,
                };
                _geoServerRepository.GeoServerEkle(geoServer);
            }

            else if (model.SelectedKaynakTuruId == 3 && !string.IsNullOrEmpty(model.Kod))
            {
                var any = new Any
                {
                    KaynakGirisId = kaynakGiris.Id,
                    Kod = model.Kod,
                    CreatedAt = DateTime.UtcNow,

                };

                _anyRepository.AnyEkle(any);
            }

            return RedirectToAction("Index", "Home", new { selectedBelediyeId = selectedBelediyeId });
        }

        

        


    }
}
