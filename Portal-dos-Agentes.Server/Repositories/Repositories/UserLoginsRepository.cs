using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;

namespace Portal_dos_Agentes.Server.Repositories.Repositories
{
    public class UserLoginsRepository : GenericRepository<UserLogins>, IUserLoginsRepository
    {
        private readonly ApplicationDbContext _context;
        public UserLoginsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUserLogin() => await _context.Users
            .AsNoTracking()
            .Include(ul => ul.UserLogins)
            .ToListAsync();

        public async Task<User?> GetUserLogins(int userId) => await _context.Users
            .AsNoTracking()
            .Include(ul => ul.UserLogins)
            .Where(ul => ul.Id == userId)
            .SingleOrDefaultAsync();

        public async Task RemoveAll()
        {
            var userLogins = await _context.UserLogins.ToListAsync();
            if (userLogins.Any())
            {
                _context.UserLogins.RemoveRange(userLogins);
                await _context.SaveChangesAsync();
            }
        }
    }
}
