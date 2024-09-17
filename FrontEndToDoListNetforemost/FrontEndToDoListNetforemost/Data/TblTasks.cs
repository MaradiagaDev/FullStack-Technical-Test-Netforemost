namespace FrontEndToDoListNetforemost.Data
{
    public class TblTasks
    {
        public string IdTask { get; set; } = null!;
        public string? IdUser { get; set; }
        public string Title { get; set; } = null!;
        public string? DescriptionTask { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Finished { get; set; }
        public bool Deleted { get; set; }
        public string? Tags { get; set; }
        public int? IdPriority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual TblPriority? IdPriorityNavigation { get; set; }
        public virtual TblUser? IdUserNavigation { get; set; }
    }
}
