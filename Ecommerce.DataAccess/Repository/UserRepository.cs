using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DataAccess.Repository
{
    public class UserRepository : IuserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

        //Get EMail
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            
        }

        public async  Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
           await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> updatePasswordAsync(string email, string newPassword)
        {
            var user = await GetUserByEmailAsync(email);
            if (user == null) return false;

            user.PasswordHash = (newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
        

        public async Task<bool> verifyUserAync(string email, string password)
        {
            var user =await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }

        public async Task<bool> SetPasswordResetTokenAsync(string email, string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.ResetToken = token;
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetUserEmailByResetTokenAsync(string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.UtcNow);
            return user?.Email;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            user.PasswordHash = (newPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
