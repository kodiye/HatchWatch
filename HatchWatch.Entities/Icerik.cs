namespace HatchWatch.Entities;

public class Icerik
{
    public int ContentId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string ContentType { get; set; } = "";
    public int? ReleaseYear { get; set; }
    public int? DurationMinutes { get; set; }
    public int? AgeLimit { get; set; }
    public decimal AverageRating { get; set; }
    public DateTime CreatedAt { get; set; }
}