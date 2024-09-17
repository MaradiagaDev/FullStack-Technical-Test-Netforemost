namespace FrontEndToDoListNetforemost.Data
{
    public class TblUser
    {
        public string IdUser { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<TblTasks> TblTasks { get; set; }
    }
}
