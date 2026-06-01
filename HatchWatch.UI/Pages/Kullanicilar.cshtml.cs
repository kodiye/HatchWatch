using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages
{
    public class KullanicilarModel : PageModel
    {
        private readonly KullaniciBL kullaniciBL = new();

        public List<Kullanici> Kullanicilar { get; set; } = new();

        public void OnGet()
        {
            Kullanicilar = kullaniciBL.KullaniciListele();
        }
    }
}