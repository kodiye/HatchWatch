using MySqlConnector;

namespace HatchWatch.DAL;

public class IstatistikDAL
{
    public int KullaniciIzlenenSayisi(int userId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("SELECT fn_user_watched_count(@p_user_id)", baglanti);

        komut.Parameters.AddWithValue("@p_user_id", userId);

        baglanti.Open();

        object? sonuc = komut.ExecuteScalar();

        return Convert.ToInt32(sonuc);
    }

    public decimal IcerikOrtalamaPuan(int contentId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("SELECT fn_content_average_rating(@p_content_id)", baglanti);

        komut.Parameters.AddWithValue("@p_content_id", contentId);

        baglanti.Open();

        object? sonuc = komut.ExecuteScalar();

        return Convert.ToDecimal(sonuc);
    }
}