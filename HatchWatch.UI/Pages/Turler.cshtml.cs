using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages;

public class TurlerModel : PageModel
{
    private readonly TurBL turBL = new();

    public List<Tur> Turler { get; set; } = new();

    [BindProperty]
    public Tur YeniTur { get; set; } = new();

    [BindProperty]
    public Tur GuncellenecekTur { get; set; } = new();

    [BindProperty]
    public int SilinecekTurId { get; set; }

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
            turBL.TurEkle(YeniTur);
            BasariliMesaj = "Tür başarıyla eklendi.";
            return RedirectToPage("/Turler");
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
            turBL.TurGuncelle(GuncellenecekTur);
            BasariliMesaj = "Tür başarıyla güncellendi.";
            return RedirectToPage("/Turler");
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
            turBL.TurSil(SilinecekTurId);
            BasariliMesaj = "Tür başarıyla silindi.";
            return RedirectToPage("/Turler");
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
        Turler = turBL.TurListele();
    }

    private static string KullaniciMesaji(Exception ex)
    {
        if (ex.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("constraint", StringComparison.OrdinalIgnoreCase))
        {
            return "Bu tür içeriklerle ilişkili olduğu için silinemedi.";
        }

        return ex.Message;
    }
}
