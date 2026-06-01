using HatchWatch.BL;
using HatchWatch.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages;

public class IstatistiklerModel : PageModel
{
    private readonly IstatistikBL istatistikBL = new();
    private readonly KullaniciBL kullaniciBL = new();
    private readonly IcerikBL icerikBL = new();

    public List<Kullanici> Kullanicilar { get; set; } = new();
    public List<Icerik> Icerikler { get; set; } = new();

    [BindProperty]
    public int SecilenKullaniciId { get; set; }

    [BindProperty]
    public int SecilenIcerikId { get; set; }

    public int? IzlenenIcerikSayisi { get; set; }
    public decimal? OrtalamaPuan { get; set; }

    public void OnGet()
    {
        ListeYukle();
    }

    public void OnPost()
    {
        ListeYukle();

        if (SecilenKullaniciId > 0)
        {
            IzlenenIcerikSayisi = istatistikBL.KullaniciIzlenenSayisi(SecilenKullaniciId);
        }

        if (SecilenIcerikId > 0)
        {
            OrtalamaPuan = istatistikBL.IcerikOrtalamaPuan(SecilenIcerikId);
        }
    }

    private void ListeYukle()
    {
        Kullanicilar = kullaniciBL.KullaniciListele();
        Icerikler = icerikBL.IcerikListele();
    }
}