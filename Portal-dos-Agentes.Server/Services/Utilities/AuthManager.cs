using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.Services.Interfaces;

namespace Portal_dos_Agentes.Server.Services.Utilities
{
    public class AuthManager : IAuthManager
    {
        private readonly ApplicationDbContext _context;

        public AuthManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task RemoveLastLogin(int userId)
        {
            var user = await _context.Users
                .Select(u => u.Id == userId ? u : null)
                .FirstOrDefaultAsync();
            if (user != null)
            {
                var userLogins = await _context.UserLogins.Where(u => u.User == user).ToListAsync();
                if (userLogins.Count >= 5)
                {
                    //Remove the oldest login entry
                    _context.UserLogins.Remove(userLogins.MinBy(u => u.LoginDate)!);
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}
