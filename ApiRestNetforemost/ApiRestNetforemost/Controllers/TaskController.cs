using ApiRestNetforemost.DTO;
using ApiRestNetforemost.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiRestNetforemost.Controllers
{
    [ApiController]
    [Route("Tasks/api")]
    public class TaskController : Controller
    {
        private TaskServices taskServices = new TaskServices();

        [HttpPost("GetAllTask")]
        public IActionResult GetAllTask(GetTaskDto dto)
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

                if (dto.pageNumber < 1)
                {
                    return BadRequest("The page number must be greater than zero..");
                }

                if (dto.pageSize < 1)
                {
                    return BadRequest("The page size must be greater than zero.");
                }

                var response = taskServices.GetAllTask(dto.userID, dto.pageNumber, dto.pageSize);

                return Ok(new
                {
                    success = true,
                    message = "Action completed",
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

        [HttpPost("CreateTask")]
        public IActionResult CreateTask(TaskDTO dto)
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

                string responseValidations = dto.ValidateTask();

                if (responseValidations != string.Empty)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = responseValidations,
                        result = string.Empty
                    });
                }

                var task = taskServices.CreateTask(dto);

                return Ok(new
                {
                    success = true,
                    message = "Task created successfully.",
                    result = task
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

        [HttpPatch("DeleteTask")]
        public IActionResult DeleteTask(string IdTask)
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

                if (string.IsNullOrEmpty(IdTask))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "IdTask is required.",
                        result = string.Empty
                    });
                }

                var task = taskServices.DeleteTask(IdTask);

                return Ok(new
                {
                    success = true,
                    message = "Task deleted successfully.",
                    result = task
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

        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask(TaskDTO dto)
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

                if (string.IsNullOrEmpty(dto.IdTask))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "IdTask is required.",
                        result = string.Empty
                    });
                }

                var task = taskServices.UpdateTask(dto);

                return Ok(new
                {
                    success = true,
                    message = "Task updated successfully.",
                    result = task
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
