using HatchWatch.DAL;

namespace HatchWatch.BL;

public class IstatistikBL
{
    private readonly IstatistikDAL istatistikDAL = new();

    public int KullaniciIzlenenSayisi(int userId)
    {
        return istatistikDAL.KullaniciIzlenenSayisi(userId);
    }

    public decimal IcerikOrtalamaPuan(int contentId)
    {
        return istatistikDAL.IcerikOrtalamaPuan(contentId);
    }
}