using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IVPNRepository
    {
        void VPNEkle(VPN vpn);
        void VPNSil(int id);
    }
}
