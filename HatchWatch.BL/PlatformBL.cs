using HatchWatch.DAL;
using HatchWatch.Entities;

namespace HatchWatch.BL;

public class PlatformBL
{
    private readonly PlatformDAL platformDAL = new();

    public List<Platform> PlatformListele()
    {
        return platformDAL.PlatformListele();
    }

    public void PlatformEkle(Platform platform)
    {
        platformDAL.PlatformEkle(platform);
    }

    public void PlatformGuncelle(Platform platform)
    {
        platformDAL.PlatformGuncelle(platform);
    }

    public void PlatformSil(int platformId)
    {
        platformDAL.PlatformSil(platformId);
    }
}
