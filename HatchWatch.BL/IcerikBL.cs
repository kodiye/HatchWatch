using HatchWatch.DAL;
using HatchWatch.Entities;

namespace HatchWatch.BL;

public class IcerikBL
{
    private readonly IcerikDAL icerikDAL = new();

    public List<Icerik> IcerikListele()
    {
        return icerikDAL.IcerikListele();
    }

    public void IcerikEkle(Icerik icerik)
    {
        icerikDAL.IcerikEkle(icerik);
    }

    public void IcerikGuncelle(Icerik icerik)
    {
        icerikDAL.IcerikGuncelle(icerik);
    }

    public void IcerikSil(int contentId)
    {
        icerikDAL.IcerikSil(contentId);
    }
}
