using ApiRestNetforemost.DTO;
using ApiRestNetforemost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRestNetforemost.Controllers
{
    [ApiController]
    [Route("Priority/api")]
    public class PriorityController : Controller
    {
        private PriorityServices _services = new PriorityServices();

        [HttpGet("GetAllPriorities")]
        public IActionResult GetAllTask()
        {
            try
            {
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

                var response = _services.GetTblPriorities();

                return Ok(new
                {
                    success = true,
                    message = "",
                    result = response
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
