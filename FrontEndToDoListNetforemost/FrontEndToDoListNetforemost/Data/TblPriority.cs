namespace FrontEndToDoListNetforemost.Data
{
    public class TblPriority
    {
        public int IdPriority { get; set; }
        public string? NamePriority { get; set; }
        public virtual ICollection<TblTasks> TblTasks { get; set; }
    }
}
