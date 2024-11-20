using ChatApp.Models;

namespace ChatApp.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);

        Task<AuthModel> LoginAsync(LoginModel model);
    }
}
