using assetsmentCelsia.Models;
namespace assetsmentCelsia.Services.Interfaces
{
    public interface IUserLoginRepository
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
