namespace FrontEndToDoListNetforemost.Data
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserCorrectAnswer
    {
        public TblUser user { get; set; }
        public string token { get; set; }
    }

    public class RequestGeneric<T>
    {
        public bool success { get; set; }
        public string messages { get; set; }
        public T result { get; set; }
     }
}
