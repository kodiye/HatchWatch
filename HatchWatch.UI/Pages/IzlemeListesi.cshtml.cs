using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages;

public class IzlemeListesiModel : PageModel
{
    private readonly IzlemeListesiBL izlemeListesiBL = new();
    private readonly KullaniciBL kullaniciBL = new();
    private readonly IcerikBL icerikBL = new();
    private readonly IstatistikBL istatistikBL = new();

    public List<IzlemeListesi> IzlemeListesi { get; set; } = new();
    public List<Kullanici> Kullanicilar { get; set; } = new();
    public List<Icerik> Icerikler { get; set; } = new();

    [BindProperty]
    public IzlemeListesi YeniKayit { get; set; } = new();

    [BindProperty]
    public IzlemeListesi GuncellenecekKayit { get; set; } = new();

    [BindProperty]
    public int SilinecekKayitId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int SecilenKullaniciId { get; set; }

    public int? IzlenenIcerikSayisi { get; set; }

    [TempData]
    public string? BasariliMesaj { get; set; }

    public string? HataMesaji { get; set; }

    public void OnGet()
    {
        ListeYukle();
    }

    public IActionResult OnPostEkle()
    {
        try
        {
            izlemeListesiBL.IzlemeListesiEkle(YeniKayit);
            BasariliMesaj = "İzleme listesine kayıt eklendi.";
            return RedirectToPage("/IzlemeListesi", new { SecilenKullaniciId = YeniKayit.UserId });
        }
        catch (Exception ex)
        {
            SecilenKullaniciId = YeniKayit.UserId;
            HataMesaji = KullaniciMesaji(ex);
            ListeYukle();
            return Page();
        }
    }

    public IActionResult OnPostGuncelle()
    {
        try
        {
            izlemeListesiBL.IzlemeListesiGuncelle(GuncellenecekKayit);
            BasariliMesaj = "İzleme listesi kaydı güncellendi.";
            return RedirectToPage("/IzlemeListesi", new { SecilenKullaniciId });
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
            izlemeListesiBL.IzlemeListesiSil(SilinecekKayitId);
            BasariliMesaj = "İzleme listesi kaydı silindi.";
            return RedirectToPage("/IzlemeListesi", new { SecilenKullaniciId });
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
        Kullanicilar = kullaniciBL.KullaniciListele();
        Icerikler = icerikBL.IcerikListele();

        if (SecilenKullaniciId == 0 && Kullanicilar.Count > 0)
        {
            SecilenKullaniciId = Kullanicilar[0].UserId;
        }

        List<IzlemeListesi> tumListe = izlemeListesiBL.IzlemeListesiListele();
        IzlemeListesi = SecilenKullaniciId > 0
            ? tumListe.Where(kayit => kayit.UserId == SecilenKullaniciId).ToList()
            : tumListe;

        if (SecilenKullaniciId > 0)
        {
            IzlenenIcerikSayisi = istatistikBL.KullaniciIzlenenSayisi(SecilenKullaniciId);
        }
    }

    private static string KullaniciMesaji(Exception ex)
    {
        if (ex.Message.Contains("İzlendi olarak işaretlenen", StringComparison.OrdinalIgnoreCase))
        {
            return ex.Message;
        }

        if (ex.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("constraint", StringComparison.OrdinalIgnoreCase))
        {
            return "Bu kayıt ilişkili veriler nedeniyle işlenemedi. Kullanıcı ve içerik seçimlerini kontrol edin.";
        }

        return ex.Message;
    }
}
