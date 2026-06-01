using System.Data;
using HatchWatch.Entities;
using MySqlConnector;

namespace HatchWatch.DAL;

public class IzlemeListesiDAL
{
    public List<IzlemeListesi> IzlemeListesiListele()
    {
        List<IzlemeListesi> liste = new();

        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_watchlist_select_all", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        baglanti.Open();

        using var okuyucu = komut.ExecuteReader();

        while (okuyucu.Read())
        {
            IzlemeListesi kayit = new()
            {
                WatchlistId = Convert.ToInt32(okuyucu["watchlist_id"]),
                UserId = Convert.ToInt32(okuyucu["user_id"]),
                Username = okuyucu["username"].ToString() ?? "",
                ContentId = Convert.ToInt32(okuyucu["content_id"]),
                Title = okuyucu["title"].ToString() ?? "",
                WatchStatus = okuyucu["watch_status"].ToString() ?? "",
                UserRating = okuyucu["user_rating"] == DBNull.Value ? null : Convert.ToInt32(okuyucu["user_rating"]),
                AddedAt = Convert.ToDateTime(okuyucu["added_at"]),
                UpdatedAt = okuyucu["updated_at"] == DBNull.Value ? null : Convert.ToDateTime(okuyucu["updated_at"])
            };

            liste.Add(kayit);
        }

        return liste;
    }

    public void IzlemeListesiEkle(IzlemeListesi kayit)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_watchlist_insert", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        komut.Parameters.AddWithValue("p_user_id", kayit.UserId);
        komut.Parameters.AddWithValue("p_content_id", kayit.ContentId);
        komut.Parameters.AddWithValue("p_watch_status", kayit.WatchStatus);
        komut.Parameters.AddWithValue("p_user_rating", kayit.UserRating.HasValue ? kayit.UserRating.Value : DBNull.Value);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void IzlemeListesiGuncelle(IzlemeListesi kayit)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_watchlist_update", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        komut.Parameters.AddWithValue("p_watchlist_id", kayit.WatchlistId);
        komut.Parameters.AddWithValue("p_watch_status", kayit.WatchStatus);
        komut.Parameters.AddWithValue("p_user_rating", kayit.UserRating.HasValue ? kayit.UserRating.Value : DBNull.Value);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void IzlemeListesiSil(int watchlistId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_watchlist_delete", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_watchlist_id", watchlistId);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }
}
