using HatchWatch.DAL;
using HatchWatch.Entities;

namespace HatchWatch.BL;

public class IzlemeListesiBL
{
    private readonly IzlemeListesiDAL izlemeListesiDAL = new();

    public List<IzlemeListesi> IzlemeListesiListele()
    {
        return izlemeListesiDAL.IzlemeListesiListele();
    }

    public void IzlemeListesiEkle(IzlemeListesi kayit)
    {
        izlemeListesiDAL.IzlemeListesiEkle(kayit);
    }

    public void IzlemeListesiGuncelle(IzlemeListesi kayit)
    {
        izlemeListesiDAL.IzlemeListesiGuncelle(kayit);
    }

    public void IzlemeListesiSil(int watchlistId)
    {
        izlemeListesiDAL.IzlemeListesiSil(watchlistId);
    }
}
