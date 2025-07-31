using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IVPNTuruRepository
    {
        IEnumerable<VPNAltTuru> VPNAltTurleri { get; set; }

        VPNAltTuru GetById(int id);
        public void UpdateVpnTuru(VPNAltTuru VPNAltTuru);
    }
}
