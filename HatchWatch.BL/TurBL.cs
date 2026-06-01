using HatchWatch.DAL;
using HatchWatch.Entities;

namespace HatchWatch.BL;

public class TurBL
{
    private readonly TurDAL turDAL = new();

    public List<Tur> TurListele()
    {
        return turDAL.TurListele();
    }

    public void TurEkle(Tur tur)
    {
        turDAL.TurEkle(tur);
    }

    public void TurGuncelle(Tur tur)
    {
        turDAL.TurGuncelle(tur);
    }

    public void TurSil(int genreId)
    {
        turDAL.TurSil(genreId);
    }
}
