using ChatApp.Hubs;
using ChatApp.Models;
using ChatApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<object>> GetAllUsersAsync()
        {
            var onlineUsers = ChatHub.GetOnlineUsers();

            return await _userManager.Users
                .Select(user => new
                {
                    user.Id,
                    user.UserName,
                    user.PhoneNumber,
                    IsOnline = onlineUsers.Contains(user.Id)
                })
                .ToListAsync();
        }
    }
}
