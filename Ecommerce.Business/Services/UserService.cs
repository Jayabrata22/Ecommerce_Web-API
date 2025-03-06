using Ecommerce.Business.Interfaces.Service;
using Ecommerce.DataAccess.Repository;
using Ecommerce.Models;
using Ecommerce.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IuserRepository repository;
        private readonly IConfiguration _configuration;
        private readonly ImailService emailService;

        public UserService(IuserRepository repository, IConfiguration _configuration, ImailService emailService)
        {
            this.repository = repository;
            this._configuration = _configuration;
            this.emailService = emailService;
        }

        
        //Register
        public async Task<string> RegisterUserAsync(UserDTO userDto)
        {
            var exsits = repository.GetUserByEmailAsync(userDto.Email);
            if (exsits != null)
            {
                DateTime CreatedAtt = DateTime.Now;
                var user = new User
                {
                    Email = userDto.Email,
                    FullName = userDto.FullName,
                    PasswordHash = userDto.Pssword,
                    Role = "Buyer",
                    CreatedAt = CreatedAtt
                };
                await repository.RegisterUserAsync(user);
                return "User Registered Successfully";
            }
            else
            {
                return "User Already Exists";
            }
        }

        //Login
        public async Task<string> LoginUserAsync(LogInDTO logindto)
        {
            bool IsvalidUser = await repository.verifyUserAync(logindto.Email, logindto.Password);
            return IsvalidUser ? "Login Successful" : "Invalid Login Details";
        }

        //using JWT Token
        //public async Task<string> LoginUserAsync(LogInDTO logindto)
        //{
        //    var user = await repository.GetUserByEmailAsync(logindto.Email);
        //    if (user == null || user.PasswordHash != logindto.Password)
        //    {
        //        return null;
        //    }

        //    return GenerateJwtToken(user);
        //}
        //private string GenerateJwtToken(User user)
        //{
        //    var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim("UserId", user.Id.ToString()),
        //            new Claim("Role", "User") // Add roles if needed
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
        //        Issuer = _configuration["Jwt:Issuer"],
        //        Audience = _configuration["Jwt:Audience"],
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var token = GenerateResetToken();
            bool success = await repository.SetPasswordResetTokenAsync(email, token);
            if (!success) return "Email not found!";

            var resetLink = $"https://localhost:5002/Account/ResetPassword?token={token}";
            string emailBody = $"Click <a href='{resetLink}'>here</a> to reset your password.";
            await emailService.SendEmailAsync(email, "Reset Password", emailBody);

            return "Password reset link sent to your email.";
        }

        public async Task<string> ResetPasswordAsync(string token, string newPassword)
        {
            var email = await repository.GetUserEmailByResetTokenAsync(token);
            if (string.IsNullOrEmpty(email)) return "Invalid or expired token.";

            bool success = await repository.ResetPasswordAsync(email, newPassword);
            return success ? "Password reset successfully!" : "Error resetting password.";
        }

        private string GenerateResetToken()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] tokenBytes = new byte[32];
            rng.GetBytes(tokenBytes);
            return Convert.ToBase64String(tokenBytes);
        }
    }
}

