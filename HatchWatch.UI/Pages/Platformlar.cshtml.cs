using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages;

public class PlatformlarModel : PageModel
{
    private readonly PlatformBL platformBL = new();

    public List<Platform> Platformlar { get; set; } = new();

    [BindProperty]
    public Platform YeniPlatform { get; set; } = new();

    [BindProperty]
    public Platform GuncellenecekPlatform { get; set; } = new();

    [BindProperty]
    public int SilinecekPlatformId { get; set; }

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
            platformBL.PlatformEkle(YeniPlatform);
            BasariliMesaj = "Platform başarıyla eklendi.";
            return RedirectToPage("/Platformlar");
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
            platformBL.PlatformGuncelle(GuncellenecekPlatform);
            BasariliMesaj = "Platform başarıyla güncellendi.";
            return RedirectToPage("/Platformlar");
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
            platformBL.PlatformSil(SilinecekPlatformId);
            BasariliMesaj = "Platform başarıyla silindi.";
            return RedirectToPage("/Platformlar");
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
        Platformlar = platformBL.PlatformListele();
    }

    private static string KullaniciMesaji(Exception ex)
    {
        if (ex.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase) ||
            ex.Message.Contains("constraint", StringComparison.OrdinalIgnoreCase))
        {
            return "Bu platform içeriklerle ilişkili olduğu için silinemedi.";
        }

        return ex.Message;
    }
}
