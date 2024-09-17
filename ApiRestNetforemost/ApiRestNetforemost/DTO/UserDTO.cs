using ApiRestNetforemost.ModelBDNetforemost;
using ApiRestNetforemost.Services;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace ApiRestNetforemost.DTO
{
    public class UserDTO
    {
        public string IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string UserPassword { get; set; }

        public string ValidateForCreationAsync()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                return "First name is required.";

            if (string.IsNullOrWhiteSpace(LastName))
                return "Last name is required.";

            if (!IsValidEmail(Email))
                return "Invalid email format.";

            if (IsEmailExist(Email))
                return "The email address you entered already exists.";

            if (string.IsNullOrWhiteSpace(UserPassword))
                return "Password is required.";

            if (UserPassword.Length < 6)
                return "Password must be at least 6 characters long.";

            return "";
        }

        public async Task<TblUser> CreateUser(IConfiguration configuration)
        {
            try
            {
                UserServices services = new UserServices(configuration);
                var userCreated = await services.CreateUserAsync(this);
                return userCreated;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool IsEmailExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            using (NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
            {
                bool vlocEmailExist =  db.TblUsers.Where(user => user.Email.Contains(email)).Any();

                return vlocEmailExist;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return regex.IsMatch(email);
        }
    }
}
