using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IMesService
    {
        Task<List<Relatorio>> GetMesesAsync();
        Task<Relatorio> CreateOrUpdateAsync(Pedido pedido);

        Task<List<Relatorio>> GetMesesByUserAsync(int id);
        Task<List<DiaDTO>> GetDatByDate(int UserId, int mes, int ano);

        Task<List<DiaDTO>> GetByMesAndDay(int UserId, int mes, int dia);

        Task<List<DiaDTO>> GetAllDaysMes(int ano, int mes);
    }
}
