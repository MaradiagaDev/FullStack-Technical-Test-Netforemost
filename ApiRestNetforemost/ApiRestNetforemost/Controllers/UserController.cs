using ApiRestNetforemost.DTO;
using ApiRestNetforemost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRestNetforemost.Controllers
{
    [ApiController]
    [Route("Users/api")]
    public class UserController : Controller
    {
        public IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO loginDto)
        {
            try
            {
                string validation = loginDto.ValidateLogin();
                if (validation != string.Empty)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = validation,
                        result = string.Empty
                    });
                }

                var result = await loginDto.Login(_configuration);

                if (result != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Access granted.",
                        result = result
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid username or password. Please try again.",
                        result = string.Empty
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An unexpected error occurred. Error: {ex.Message}",
                    result = string.Empty
                });
            }
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDTO dto)
        {
            try
            {
                string validation = dto.ValidateForCreationAsync();
                if (validation != string.Empty)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = validation,
                        result = string.Empty
                    });
                }

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                bool token = Jwt.ValidateToken(identity);

                if (!token)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid token.",
                        result = string.Empty
                    });
                }

                var result = await dto.CreateUser(_configuration);

                return Ok(new
                {
                    success = true,
                    message = "User created successfully.",
                    result = result
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"An unexpected error occurred. Error: {ex.Message}",
                    result = string.Empty
                });
            }
        }
    }
}
