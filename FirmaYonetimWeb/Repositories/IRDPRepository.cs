using FirmaYonetimWeb.Models;

namespace FirmaYonetimWeb.Repositories
{
    public interface IRDPRepository
    {
        void RDPEkle(RDP RDP);
        void RDPSil(int id);
    }
}
