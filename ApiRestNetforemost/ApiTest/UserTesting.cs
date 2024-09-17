using ApiRestNetforemost.Controllers;
using ApiRestNetforemost.DTO;
using ApiRestNetforemost.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiTest
{
    public class UserTesting
    {
        private readonly UserController _userController;
        private readonly UserServices _services;


        [Fact]
        public void LogInOk()
        {
            var result = _userController.LoginUser(new UserLoginDTO { Email = "1dba.rolando.maradiaga@gmail.com", Password = "facil1234" });
            Assert.IsType<OkObjectResult>(result);
        }
    }
}