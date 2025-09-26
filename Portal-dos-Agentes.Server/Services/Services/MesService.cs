using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Services.Interfaces;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Services.Utilities;
namespace Portal_dos_Agentes.Server.Services.Services
{
    public class MesService : IMesService
    {
        private readonly IMesRepository repository;

        public MesService(IMesRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<DiaDTO>> GetDatByDate(int UserId, int mes, int ano)
        {
            List<DiaDTO> dias = new List<DiaDTO>();
            var relatorios = await repository.GetMes(UserId, mes, ano);
            if (relatorios is null || relatorios.Count == 0)
            {
                return dias;
            }
            relatorios.ForEach(r =>
            {
                dias.Add(new DiaDTO
                {
                    Ano = r.Ano,
                    Mes = MesHelper.MounthtoString(r.Mes),
                    Dia = r.Dia,
                    Hora = r.Hora,
                    Valor = r.Total,
                    Username = r.User.Nome
                });
            });
            return dias;
        }

        public async Task<Relatorio> CreateOrUpdateAsync(Pedido pedido)
        {
            int ano = pedido.DataPedido.Year;
            int mes = pedido.DataPedido.Month;
            int dia = pedido.DataPedido.Day;
            string hora = pedido.DataPedido.ToString("HH:mm:ss");
            var cliente = pedido.User ?? throw new ArgumentNullException("user não pode ser nulo aqui");
            var dadosMes = await repository.GetDadosMes(cliente, ano, mes, dia);

            if (dadosMes is null)
            {
                dadosMes = new Relatorio
                {
                    User = cliente,
                    Ano = ano,
                    Mes = mes,
                    Dia = dia,
                    Hora = hora,
                    Total = pedido.ValorPedido
                };
                await repository.CreateAsync(dadosMes);
            }
            else
            {
                dadosMes.Total += pedido.ValorPedido;
                await repository.UpdateAsync(dadosMes);
            }

            return dadosMes;
        }

        public async Task<List<Relatorio>> GetMesesAsync() =>
            await repository.GetAllAsync();

        public async Task<List<Relatorio>> GetMesesByUserAsync(int id) =>
            await repository.GetMesesAsync(id);

        public Task<List<DiaDTO>> GetByMesAndDay(int ano, int mes, int dia)
        {
            return repository.GetByMesAndDay(ano, mes, dia);
        }
        public async Task<List<DiaDTO>> GetAllDaysMes(int ano, int mes) =>
            await repository.GetAllDaysMes(ano, mes);
    }
}
