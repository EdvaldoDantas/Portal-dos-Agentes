using Portal_dos_Agentes.Server.DTOs;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces;

public interface IDashBoardRepository
{
    Task<(decimal total, decimal variacao)> GetTotal();
    Task<List<RankingDTO>?> GetRankingMes();
    Task<(int MesAtual, int percentual)> AgentesNovos();
    Task<int> TotalAgentes();

    Task<DashboardDados> DashboardDadosMes();

}
