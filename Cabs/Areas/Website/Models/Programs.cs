namespace Cabs.Areas.Website.Models
{
    public enum ProgramStatus
    {
        UP_COMING = 1,

        COMING = 2,

        CLOSE = 3,
    }

    public class Programs
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public string? Image { get; set; }

        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual List<ImageModel>? Images { get; set; }
    }
}
