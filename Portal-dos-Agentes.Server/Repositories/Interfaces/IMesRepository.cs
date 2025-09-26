using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces
{
    public interface IMesRepository : IGenericRepository<Relatorio>
    {
        Task<Relatorio?> GetDadosMes(User user, int ano, int mes, int dia);
        Task<List<Relatorio>> GetMesesAsync(int id);
        Task<List<Relatorio>> GetMes(int UserId, int mes, int ano);

        Task<List<DiaDTO>> GetByMesAndDay(int ano, int mes, int dia);
        Task<List<DiaDTO>> GetAllDaysMes(int ano, int mes);
    }

}
