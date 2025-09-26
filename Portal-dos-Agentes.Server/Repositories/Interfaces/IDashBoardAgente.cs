using Portal_dos_Agentes.Server.DTOs;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces;

public interface IDashBoardAgenteRepository
{
    Task<(decimal total, decimal variacao)> GetTotal(int idAgente);
    Task<List<RankingAgenteDTO>?> GetRankingMes(int idAgente);
    Task<DashboardDados> DashboardDadosMes(int idAgente);
}
