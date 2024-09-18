namespace FrontEndToDoListNetforemost.Data
{
    public class GetTaskDto
    {
        public string userID { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

    public class GetTaskResultDto
    {
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int pageSize { get; set; }
        public int TotalPages { get; set; }
        public List<TblTasks> Data { get; set; }
    }
}
