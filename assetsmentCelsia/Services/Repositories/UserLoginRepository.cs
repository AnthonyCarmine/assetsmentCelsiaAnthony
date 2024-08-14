using Microsoft.EntityFrameworkCore;
using assetsmentCelsia.Models;
using assetsmentCelsia.Data;
using assetsmentCelsia.Services.Interfaces;
using System.Threading.Tasks;

namespace assetsmentCelsia.Services.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly BaseContext _context;

        public UserLoginRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                throw new Exception("El correo electrónico no está registrado.");

            if (user.Password != password)
                throw new Exception("La contraseña es incorrecta.");

            var isAdmin = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == user.Id && ur.RoleId == 1);

            if (!isAdmin)
                throw new Exception("El usuario no tiene permisos de administrador.");

            return user;
        }
    }
}


