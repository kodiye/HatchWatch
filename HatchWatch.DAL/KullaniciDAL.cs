using System.Data;
using HatchWatch.Entities;
using MySqlConnector;

namespace HatchWatch.DAL;

public class KullaniciDAL
{
    public List<Kullanici> KullaniciListele()
    {
        List<Kullanici> kullanicilar = new();

        using var baglanti = VeritabaniBaglanti.BaglantiGetir();
        using var komut = new MySqlCommand("sp_users_select_all", baglanti);

        komut.CommandType = CommandType.StoredProcedure;

        baglanti.Open();

        using var okuyucu = komut.ExecuteReader();

        while (okuyucu.Read())
        {
            Kullanici kullanici = new()
            {
                UserId = Convert.ToInt32(okuyucu["user_id"]),
                Username = okuyucu["username"].ToString() ?? "",
                Email = okuyucu["email"].ToString() ?? "",
                Password = okuyucu["password"].ToString() ?? "",
                CreatedAt = Convert.ToDateTime(okuyucu["created_at"])
            };

            kullanicilar.Add(kullanici);
        }

        return kullanicilar;
    }
}