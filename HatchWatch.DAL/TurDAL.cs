using System.Data;
using HatchWatch.Entities;
using MySqlConnector;

namespace HatchWatch.DAL;

public class TurDAL
{
    public List<Tur> TurListele()
    {
        List<Tur> turler = new();

        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_genres_select_all", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        baglanti.Open();

        using var okuyucu = komut.ExecuteReader();

        while (okuyucu.Read())
        {
            Tur tur = new()
            {
                GenreId = Convert.ToInt32(okuyucu["genre_id"]),
                GenreName = okuyucu["genre_name"].ToString() ?? ""
            };

            turler.Add(tur);
        }

        return turler;
    }

    public void TurEkle(Tur tur)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_genres_insert", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_genre_name", tur.GenreName);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void TurGuncelle(Tur tur)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_genres_update", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_genre_id", tur.GenreId);
        komut.Parameters.AddWithValue("p_genre_name", tur.GenreName);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void TurSil(int genreId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_genres_delete", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_genre_id", genreId);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }
}
