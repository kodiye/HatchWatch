using System.Data;
using HatchWatch.Entities;
using MySqlConnector;

namespace HatchWatch.DAL;

public class IcerikDAL
{
    public List<Icerik> IcerikListele()
    {
        List<Icerik> icerikler = new();

        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_contents_select_all", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        baglanti.Open();

        using var okuyucu = komut.ExecuteReader();

        while (okuyucu.Read())
        {
            Icerik icerik = new()
            {
                ContentId = Convert.ToInt32(okuyucu["content_id"]),
                Title = okuyucu["title"].ToString() ?? "",
                Description = okuyucu["description"].ToString() ?? "",
                ContentType = okuyucu["content_type"].ToString() ?? "",
                ReleaseYear = okuyucu["release_year"] == DBNull.Value ? null : Convert.ToInt32(okuyucu["release_year"]),
                DurationMinutes = okuyucu["duration_minutes"] == DBNull.Value ? null : Convert.ToInt32(okuyucu["duration_minutes"]),
                AgeLimit = okuyucu["age_limit"] == DBNull.Value ? null : Convert.ToInt32(okuyucu["age_limit"]),
                AverageRating = Convert.ToDecimal(okuyucu["average_rating"]),
                CreatedAt = Convert.ToDateTime(okuyucu["created_at"])
            };

            icerikler.Add(icerik);
        }

        return icerikler;
    }

    public void IcerikEkle(Icerik icerik)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_contents_insert", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        komut.Parameters.AddWithValue("p_title", icerik.Title);
        komut.Parameters.AddWithValue("p_description", icerik.Description);
        komut.Parameters.AddWithValue("p_content_type", icerik.ContentType);
        komut.Parameters.AddWithValue("p_release_year", icerik.ReleaseYear);
        komut.Parameters.AddWithValue("p_duration_minutes", icerik.DurationMinutes);
        komut.Parameters.AddWithValue("p_age_limit", icerik.AgeLimit);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void IcerikGuncelle(Icerik icerik)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_contents_update", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        komut.Parameters.AddWithValue("p_content_id", icerik.ContentId);
        komut.Parameters.AddWithValue("p_title", icerik.Title);
        komut.Parameters.AddWithValue("p_description", icerik.Description);
        komut.Parameters.AddWithValue("p_content_type", icerik.ContentType);
        komut.Parameters.AddWithValue("p_release_year", icerik.ReleaseYear);
        komut.Parameters.AddWithValue("p_duration_minutes", icerik.DurationMinutes);
        komut.Parameters.AddWithValue("p_age_limit", icerik.AgeLimit);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void IcerikSil(int contentId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_contents_delete", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_content_id", contentId);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }
}
