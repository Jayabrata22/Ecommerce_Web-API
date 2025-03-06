using Ecommerce.Business.Interfaces.Service;
using Ecommerce.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);

            if (result == "User Registered Successfully")
                return Ok(new { message = result });

            return BadRequest(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDTO logindto)
        {
            var result = await _userService.LoginUserAsync(logindto);
            if (result == "Login Successful")
                return Ok(new { message = result });
            return BadRequest(new { message = result });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            var result = await _userService.ForgotPasswordAsync(email);
            return Ok(new { message = result });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO dto)
        {
            var result = await _userService.ResetPasswordAsync(dto.ResetToken, dto.NewPassword);
            return Ok(new { message = result });
        }
    }
}
