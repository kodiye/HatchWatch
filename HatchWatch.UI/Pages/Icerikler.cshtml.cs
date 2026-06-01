using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace HatchWatch.UI.Pages;

public class IceriklerModel : PageModel
{
    private readonly IcerikBL icerikBL = new();
    private readonly IstatistikBL istatistikBL = new();

    public List<Icerik> Icerikler { get; set; } = new();

    [BindProperty]
    public Icerik YeniIcerik { get; set; } = new();

    [BindProperty]
    public Icerik GuncellenecekIcerik { get; set; } = new();

    [BindProperty]
    public int SilinecekIcerikId { get; set; }

    [TempData]
    public string? BasariliMesaj { get; set; }

    public string? HataMesaji { get; set; }

    public string? PosterYolu(Icerik icerik)
    {
        string dosyaAdi = PosterDosyaAdi(icerik.Title);
        string[] uzantilar = [".jpg", ".jpeg", ".png", ".webp"];

        foreach (string uzanti in uzantilar)
        {
            string goreliYol = $"img/posters/{dosyaAdi}{uzanti}";
            string fizikselYol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "posters", $"{dosyaAdi}{uzanti}");

            if (System.IO.File.Exists(fizikselYol))
            {
                return "/" + goreliYol;
            }
        }

        return null;
    }

    public void OnGet()
    {
        ListeYukle();
    }

    public IActionResult OnPostEkle()
    {
        try
        {
            icerikBL.IcerikEkle(YeniIcerik);
            BasariliMesaj = "İçerik başarıyla eklendi.";
            return Redirect("/Icerikler#icerik-yonetimi");
        }
        catch (Exception ex)
        {
            HataMesaji = KullaniciMesaji(ex);
            ListeYukle();
            return Page();
        }
    }

    public IActionResult OnPostGuncelle()
    {
        try
        {
            icerikBL.IcerikGuncelle(GuncellenecekIcerik);
            BasariliMesaj = "İçerik başarıyla güncellendi.";
            return Redirect("/Icerikler#icerik-yonetimi");
        }
        catch (Exception ex)
        {
            HataMesaji = KullaniciMesaji(ex);
            ListeYukle();
            return Page();
        }
    }

    public IActionResult OnPostSil()
    {
        try
        {
            icerikBL.IcerikSil(SilinecekIcerikId);
            BasariliMesaj = "İçerik başarıyla silindi.";
            return Redirect("/Icerikler#icerik-yonetimi");
        }
        catch (Exception ex)
        {
            HataMesaji = KullaniciMesaji(ex);
            ListeYukle();
            return Page();
        }
    }

    private void ListeYukle()
    {
        Icerikler = icerikBL.IcerikListele();

        foreach (var icerik in Icerikler)
        {
            icerik.AverageRating = istatistikBL.IcerikOrtalamaPuan(icerik.ContentId);
        }
    }

    private static string KullaniciMesaji(Exception ex)
    {
        if (ex.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("constraint", StringComparison.OrdinalIgnoreCase))
        {
            return "Bu içerik başka kayıtlarla ilişkili olduğu için silinemedi. Önce izleme listesi kayıtlarını temizleyin.";
        }

        return ex.Message;
    }

    private static string PosterDosyaAdi(string baslik)
    {
        Dictionary<char, char> turkceKarakterler = new()
        {
            ['ç'] = 'c',
            ['ğ'] = 'g',
            ['ı'] = 'i',
            ['ö'] = 'o',
            ['ş'] = 's',
            ['ü'] = 'u'
        };

        StringBuilder temizBaslik = new();
        bool oncekiTire = false;

        foreach (char karakter in baslik.Trim().ToLowerInvariant())
        {
            char temizKarakter = turkceKarakterler.TryGetValue(karakter, out char asciiKarakter)
                ? asciiKarakter
                : karakter;

            if (char.IsLetterOrDigit(temizKarakter))
            {
                temizBaslik.Append(temizKarakter);
                oncekiTire = false;
            }
            else if (!oncekiTire && temizBaslik.Length > 0)
            {
                temizBaslik.Append('-');
                oncekiTire = true;
            }
        }

        return temizBaslik.ToString().Trim('-');
    }
}
