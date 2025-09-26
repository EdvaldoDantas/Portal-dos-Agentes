using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Portal_dos_Agentes.Server.Repositories.Repositories;

public class DashBoardAgenteRepository : IDashBoardAgenteRepository
{
    private readonly ApplicationDbContext _context;
    public DashBoardAgenteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDados> DashboardDadosMes(int idAgente)
    {
        var user = await _context.Users
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Id == idAgente);

        if (user == null)
            return new DashboardDados();

        var dados = await _context.Pedidos
            .Where(p => p.DataPedido.Year == DateTime.Now.Year && p.isAceite && p.User == user)
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

    public async Task<List<RankingAgenteDTO>?> GetRankingMes(int idAgente)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(c => c.Id == idAgente)
            ?? throw new ArgumentNullException("Usuário deve estar autenticado");

        var mesAtual = DateTime.Now.Month;
        var anoAtual = DateTime.Now.Year;

        // Consulta de todos os pedidos do mês atual
        var rankingsQuery = _context.Pedidos
            .Where(p => p.DataPedido.Month == mesAtual
                     && p.DataPedido.Year == anoAtual
                     && p.isAceite)
            .GroupBy(p => new { p.User.Id, p.User.Nome })
            .Select(g => new
            {
                UserId = g.Key.Id,
                UserName = g.Key.Nome,
                Total = g.Sum(p => p.ValorPedido)
            })
            .ToList() // Transfere para memória
            .OrderByDescending(x => x.Total) // Ordena por total
            .Select((x, index) => new RankingAgenteDTO
            {
                UserId = x.UserId,
                UserName = x.UserName,
                Total = x.Total,
                Posicao = index + 1
            })
            .ToList();

        // Obter os 10 primeiros do ranking
        var top10 = rankingsQuery.Take(10).ToList();

        // Verificar se o agente está no top 10
        var dadosAgente = rankingsQuery.FirstOrDefault(r => r.UserId == idAgente);

        if (dadosAgente != null)
        {
            // Se o agente não estiver no top 10, adicione-o no final da lista
            if (!top10.Any(r => r.UserId == idAgente))
            {
                top10.Add(dadosAgente);
                top10 = top10.OrderByDescending(r => r.Posicao).ToList();
            }
        }

        return top10;
    }



    public async Task<(decimal total, decimal variacao)> GetTotal(int idAgente)
    {
        var user = await _context.Users
        .AsNoTracking()
        .SingleOrDefaultAsync(u => u.Id == idAgente);

        if (user is null)
            return (0, 0);
        
        var mesAtual = await _context.Pedidos
            .Where(p => p.DataPedido.Month == DateTime.Now.Month && p.DataPedido.Year == DateTime.Now.Year && p.User.Id == user.Id && p.isAceite)
            .SumAsync(p => p.ValorPedido);

        var mesAnterior = await _context.Pedidos
            .Where(p => p.DataPedido.Month == DateTime.Now.AddMonths(-1).Month && p.DataPedido.Year == DateTime.Now.AddMonths(-1).Year && p.User.Id == user.Id && p.isAceite)
            .SumAsync(p => p.ValorPedido);

        var percentual = mesAnterior != 0 ? (mesAtual - mesAnterior) / mesAnterior * 100 : 100;

        return (mesAtual, percentual);
    }
}
