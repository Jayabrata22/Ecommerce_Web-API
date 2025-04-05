using Business.Interfaces;
using Business.Services;
using Ecommerce.Models.DTO;
using Ecommerce.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IAuthInterfaceService _authServiceRepository;
        private readonly UserManager<User> _userManager;

        public UserAuthenticationController(IAuthInterfaceService authServiceRepository, UserManager<User> userManager)
        {
            _authServiceRepository = authServiceRepository;
            _userManager = userManager;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _authServiceRepository.RegisterAsync(model);
            return Ok(new { message = "Registration successful", user = result.Email });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var token = await _authServiceRepository.LoginAsync(model);
            if (token == null)
                return Unauthorized("Invalid credentials");

            // Optional: Store in Session/Cookie
            HttpContext.Session.SetString("JWToken", token); // Session
            Response.Cookies.Append("JWToken", token); // Cookie    

            return Ok(new { token });
        }

        [HttpPost("admin-update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordupdateDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("User not found.");

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                return BadRequest(removeResult.Errors);

            var addResult = await _userManager.AddPasswordAsync(user, dto.NewPassword);
            if (!addResult.Succeeded)
                return BadRequest(addResult.Errors);
            else
            {
                await _authServiceRepository.UpdatePassword(user, dto.NewPassword);
            }

                return Ok("Password updated successfully.");
        }


        [HttpPost("confirm-contacts")]
        public async Task<IActionResult> ConfirmContacts([FromBody] ConfirmUserContactDto dto)
        {
            var result = await _authServiceRepository.ConfirmEmailAndPhoneAsync(dto);
            if (result)
                return Ok("Contact information confirmed.");

            return BadRequest("Invalid email or phone number.");
        }

    }
}
