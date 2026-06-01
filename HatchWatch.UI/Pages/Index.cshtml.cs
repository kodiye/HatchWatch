using HatchWatch.BL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HatchWatch.UI.Pages;

public class IndexModel : PageModel
{
    private readonly IcerikBL icerikBL = new();
    private readonly IzlemeListesiBL izlemeListesiBL = new();

    public int IcerikSayisi { get; set; }
    public int IzlemeKaydiSayisi { get; set; }
    public int IzlenenSayisi { get; set; }

    public void OnGet()
    {
        var icerikler = icerikBL.IcerikListele();
        var izlemeListesi = izlemeListesiBL.IzlemeListesiListele();

        IcerikSayisi = icerikler.Count;
        IzlemeKaydiSayisi = izlemeListesi.Count;
        IzlenenSayisi = izlemeListesi.Count(kayit => kayit.WatchStatus == "İzlendi");
    }
}
