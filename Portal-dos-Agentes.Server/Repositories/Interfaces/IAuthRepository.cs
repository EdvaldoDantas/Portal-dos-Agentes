using AuthLibrary.Models;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces
{
    public interface IAuthRepository : IGenericRepository<UserLogins>
    {
        Task<UserLogins?> IsUserLoged(string RefreshToken);
        Task SaveChanges();
    }
}
