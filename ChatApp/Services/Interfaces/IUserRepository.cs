using ChatApp.Models;

namespace ChatApp.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<object>> GetAllUsersAsync();
    }
}
