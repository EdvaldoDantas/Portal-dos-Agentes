using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;

namespace Portal_dos_Agentes.Server.Repositories.Repositories
{
    public class AuthRepository : GenericRepository<UserLogins>, IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserLogins?> IsUserLoged(string RefreshToken)
            => await _context.UserLogins
            .Include(ul => ul.User)
            .SingleOrDefaultAsync(ul => (ul.Token == RefreshToken) && (!ul.IsRevoked) && (ul.ExpirationDate > DateTime.UtcNow));
        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}
