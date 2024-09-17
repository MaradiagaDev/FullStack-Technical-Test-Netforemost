using ApiRestNetforemost.ModelBDNetforemost;
using ApiRestNetforemost.Services;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace ApiRestNetforemost.DTO
{
    public class UserLoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string ValidateLogin()
        {
            if (string.IsNullOrWhiteSpace(Email))
                return "Email is required.";

            if (string.IsNullOrWhiteSpace(Password))
                return "Password is required.";

            return string.Empty;
        }

        public async Task<UserCorrectAnswer> Login(IConfiguration configuration)
        {
            try
            {
                UserServices services = new UserServices(configuration);
                var userCorrect = services.AuthenticateAsync(this);

                return userCorrect;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }


    public class UserCorrectAnswer
    {
        public TblUser user { get; set; }
        public string token { get; set; }
    }
}
