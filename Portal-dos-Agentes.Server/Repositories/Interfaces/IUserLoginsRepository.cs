using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces
{
    public interface IUserLoginsRepository : IGenericRepository<UserLogins>
    {
        Task<User?> GetUserLogins(int userId);
        Task<List<User>> GetAllUserLogin();

        Task RemoveAll();
    }
}
