namespace FrontEndToDoListNetforemost.Data
{
    public class PrioritiesDTO
    {
        public bool success { get; set; }
        public string messages { get; set; }
        public List<TblPriority> result { get; set; }
    }
}
