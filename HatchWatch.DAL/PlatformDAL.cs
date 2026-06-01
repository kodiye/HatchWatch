using System.Data;
using HatchWatch.Entities;
using MySqlConnector;

namespace HatchWatch.DAL;

public class PlatformDAL
{
    public List<Platform> PlatformListele()
    {
        List<Platform> platformlar = new();

        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_platforms_select_all", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        baglanti.Open();

        using var okuyucu = komut.ExecuteReader();

        while (okuyucu.Read())
        {
            Platform platform = new()
            {
                PlatformId = Convert.ToInt32(okuyucu["platform_id"]),
                PlatformName = okuyucu["platform_name"].ToString() ?? ""
            };

            platformlar.Add(platform);
        }

        return platformlar;
    }

    public void PlatformEkle(Platform platform)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_platforms_insert", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_platform_name", platform.PlatformName);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void PlatformGuncelle(Platform platform)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_platforms_update", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_platform_id", platform.PlatformId);
        komut.Parameters.AddWithValue("p_platform_name", platform.PlatformName);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }

    public void PlatformSil(int platformId)
    {
        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_platforms_delete", baglanti);

        komut.CommandType = CommandType.StoredProcedure;
        komut.Parameters.AddWithValue("p_platform_id", platformId);

        baglanti.Open();
        komut.ExecuteNonQuery();
    }
}
