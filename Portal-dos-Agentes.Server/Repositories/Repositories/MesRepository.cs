using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Services.Utilities;
using Portal_dos_Agentes.Server.Context;
namespace Portal_dos_Agentes.Server.Repositories.Repositories
{
    public class MesRepository : GenericRepository<Relatorio>, IMesRepository
    {
        private readonly ApplicationDbContext _context;
        public MesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Relatorio?> GetDadosMes(User user, int ano, int mes, int dia)
        {
            try
            {
                var resultado = await _context.Relatorios
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.User.Id == user.Id
                                && d.Ano == ano
                                && d.Mes == mes
                                && d.Dia == dia);

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);

                if (ex.InnerException != null)
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);

                throw; // Lança novamente se quiser que o erro continue sendo propagado
            }

        }

        public async Task<List<Relatorio>> GetMesesAsync(int id)=>
             await _context.Relatorios
                .AsNoTracking()
                .Include(d => d.User)
                .Where(d => d.User.Id == id)
                .OrderBy(d => d.Ano)
                .ThenBy(d => d.Hora)
                .ToListAsync();
                
        public async Task<List<Relatorio>> GetMes(int UserId, int mes, int ano)=>
             await _context.Relatorios
                .AsNoTracking()
                .Include(d => d.User)
                .Where(d => d.User.Id == UserId && d.Mes == mes && d.Ano == ano)
                .OrderBy(d => d.Dia)
                .ThenBy(d => d.Hora)
                .ToListAsync();

        public Task<List<DiaDTO>> GetByMesAndDay(int ano, int mes, int dia) =>
            _context.Relatorios
            .AsNoTracking()
            .Where(d => d.Ano == ano && d.Mes == mes && d.Dia == dia)
            .Select(d => new DiaDTO
            {
                Ano = d.Ano,
                Mes = MesHelper.MounthtoString(d.Mes),
                Dia = d.Dia,
                Hora = d.Hora,
                Valor = d.Total,
                Username = d.User.Nome
            })
            .OrderBy(d => d.Dia)
            .ThenBy(d => d.Hora)
            .ToListAsync();

        public Task<List<DiaDTO>> GetAllDaysMes(int ano, int mes) =>
            _context.Relatorios
            .AsNoTracking()
            .Where(d => d.Ano == ano && d.Mes == mes)
            .Select(d => new DiaDTO
            {
                Ano = d.Ano,
                Mes = MesHelper.MounthtoString(d.Mes),
                Dia = d.Dia,
                Hora = d.Hora,
                Valor = d.Total,
                Username = d.User.Nome
            })
            .OrderBy(d => d.Dia)
            .ThenBy(d => d.Hora)
            .ToListAsync();
    }
}
