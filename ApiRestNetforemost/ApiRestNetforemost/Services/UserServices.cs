using ApiRestNetforemost.DTO;
using ApiRestNetforemost.ModelBDNetforemost;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiRestNetforemost.Services
{
    public class UserServices
    {
        public IConfiguration _configuration;

        public UserServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<TblUser> CreateUserAsync(UserDTO user)
        {
            try
            {
                using (NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    byte[] salt = PasswordHelper.GenerateSalt();
                    string hashedPassword = PasswordHelper.HashPassword(user.UserPassword, salt);

                    TblUser newUser = new TblUser() 
                    { 
                        IdUser = Guid.NewGuid().ToString(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Telephone = user.Telephone,
                        UserPassword = hashedPassword,
                        CreatedAt = DateTime.UtcNow
                    };

                    db.Add(newUser);
                    db.SaveChanges();

                    return newUser;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public  UserCorrectAnswer AuthenticateAsync(UserLoginDTO dtoUser)
        {
            try
            {
                using (NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    var user = db.TblUsers
                        .Where(u => u.Email == dtoUser.Email)
                        .FirstOrDefault();

                    if (user == null || !PasswordHelper.VerifyPassword(dtoUser.Password, user.UserPassword))
                    {
                        return null;
                    }

                    var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.IdUser.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.UtcNow.AddDays(2),
                        signingCredentials: signingCredentials
                    );

                    var userCorrectAnswer = new UserCorrectAnswer { user = user, token = new JwtSecurityTokenHandler().WriteToken(token) };

                    return userCorrectAnswer;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Task<TblUser> UpdateUserAsync(TblUser user)
        {
            throw new NotImplementedException();
        }
    }
}
