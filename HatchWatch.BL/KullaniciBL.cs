using HatchWatch.DAL;
using HatchWatch.Entities;

namespace HatchWatch.BL;

public class KullaniciBL
{
    private readonly KullaniciDAL kullaniciDAL = new();

    public List<Kullanici> KullaniciListele()
    {
        return kullaniciDAL.KullaniciListele();
    }
}