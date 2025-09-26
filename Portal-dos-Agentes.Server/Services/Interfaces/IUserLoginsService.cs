using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IUserLoginsService
    {
        Task<List<UserLoginsDTO>?> GetUserLogins(int userId);
        Task<List<UserLoginsDTO>> GetAllUserLogin();
        Task<List<UserLogins>> GetAll();

        Task RemoveAll();
    }
}
