namespace HatchWatch.Entities;

public class IzlemeListesi
{
    public int WatchlistId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public int ContentId { get; set; }
    public string Title { get; set; } = "";
    public string WatchStatus { get; set; } = "";
    public int? UserRating { get; set; }
    public DateTime AddedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}