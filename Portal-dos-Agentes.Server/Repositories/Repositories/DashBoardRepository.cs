using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Context;
using Microsoft.Data.Sqlite;
namespace Portal_dos_Agentes.Server.Repositories.Repositories;

public class DashBoardRepository : IDashBoardRepository
{
    private readonly ApplicationDbContext _context;
    public DashBoardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(decimal total, decimal variacao)> GetTotal()
    {
        //Total do mês atual
        var totalMesAtual = await _context.Pedidos
                    .Where(p => p.DataPedido.Month == DateTime.Now.Month && p.DataPedido.Year == DateTime.Now.Year && p.isAceite)
                    .SumAsync(p => p.ValorPedido);

        //Total do mês anterior
        var totalMesAnterior = await _context.Pedidos
                    .Where(p => p.DataPedido.Month == DateTime.Now.AddMonths(-1).Month && p.DataPedido.Year == DateTime.Now.AddMonths(-1).Year && p.isAceite)
                    .SumAsync(p => p.ValorPedido);

        var variacao = totalMesAnterior != 0 ? (totalMesAtual - totalMesAnterior) / totalMesAnterior * 100 : 100;

        return (totalMesAtual, variacao);
    }
    public async Task<List<RankingDTO>?> GetRankingMes()
    {
        try
        {

            return await _context.Pedidos
                .Where(p => p.DataPedido.Month == DateTime.Now.Month && p.DataPedido.Year == DateTime.Now.Year && p.isAceite)
                .GroupBy(p => p.User.Id)
                .Select(g => new RankingDTO
                {
                    UserId = g.Key,
                    UserName = g.Select(p => p.User.Nome).FirstOrDefault()!,
                    Total = g.Sum(p => p.ValorPedido)
                })
                .OrderByDescending(r => r.Total)
                .ToListAsync();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<int> TotalAgentes() => await _context.Users
                                    .Where(u => u.Role != "adm")
                                    .CountAsync();

    public async Task<(int MesAtual, int percentual)> AgentesNovos()
    {
        var mesAtual = await _context.Users
            .Where(u => u.CreatedAt.Month == DateTime.Now.Month && u.CreatedAt.Year == DateTime.Now.Year && u.Role != "adm")
            .CountAsync();

        var mesAnterior = await _context.Users
            .Where(u => u.CreatedAt.Month == DateTime.Now.AddMonths(-1).Month && u.CreatedAt.Year == DateTime.Now.AddMonths(-1).Year && u.Role != "adm")
            .CountAsync();

        var percentual = mesAnterior != 0 ? (mesAtual - mesAnterior) / mesAnterior * 100 : 100;

        return (mesAtual, percentual);
    }

    //Retornando os os totais de cada mês do ano atual 
    public async Task<DashboardDados> DashboardDadosMes()
    {
        var dados = await _context.Pedidos
            .Where(p => p.DataPedido.Year == DateTime.Now.Year && p.isAceite)
            .GroupBy(p => p.DataPedido.Month)
            .Select(g => new { Mes = g.Key, Total = g.Sum(p => p.ValorPedido) })
            .ToListAsync();

        var resultado = new DashboardDados
        {
            Janeiro = dados.FirstOrDefault(d => d.Mes == 1)?.Total ?? 0,
            Fevereiro = dados.FirstOrDefault(d => d.Mes == 2)?.Total ?? 0,
            Marco = dados.FirstOrDefault(d => d.Mes == 3)?.Total ?? 0,
            Abril = dados.FirstOrDefault(d => d.Mes == 4)?.Total ?? 0,
            Maio = dados.FirstOrDefault(d => d.Mes == 5)?.Total ?? 0,
            Junho = dados.FirstOrDefault(d => d.Mes == 6)?.Total ?? 0,
            Julho = dados.FirstOrDefault(d => d.Mes == 7)?.Total ?? 0,
            Agosto = dados.FirstOrDefault(d => d.Mes == 8)?.Total ?? 0,
            Setembro = dados.FirstOrDefault(d => d.Mes == 9)?.Total ?? 0,
            Outubro = dados.FirstOrDefault(d => d.Mes == 10)?.Total ?? 0,
            Novembro = dados.FirstOrDefault(d => d.Mes == 11)?.Total ?? 0,
            Dezembro = dados.FirstOrDefault(d => d.Mes == 12)?.Total ?? 0,
        };

        return resultado;
    }
}
