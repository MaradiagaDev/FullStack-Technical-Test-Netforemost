using ApiRestNetforemost.DTO;
using ApiRestNetforemost.ModelBDNetforemost;

namespace ApiRestNetforemost.Services
{
    public interface IUser
    {
        Task<UserCorrectAnswer> AuthenticateAsync(UserLoginDTO dtoUser);
        Task<TblUser> CreateUserAsync(UserDTO user);
        Task<TblUser> UpdateUserAsync(TblUser user);
        
    }
}
