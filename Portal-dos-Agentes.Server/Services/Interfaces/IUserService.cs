using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<(bool Success, string sms, User? subAgente)> AddSubAgenteAsync(SubAgentDTO model);
        Task<UserSendDTO?> GetByIdAsync(int id);
        Task<List<UserSendDTO>> GetAll(); 
        Task<(bool Success, string sms)> Delete(int id);
        Task<User?> UpdateAsync(int userId, UserDTO userDto);
    }
}
