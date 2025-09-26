using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Services.Interfaces;

namespace Portal_dos_Agentes.Server.Services.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository repository;
        private readonly IEmailSender emailSender;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMesService mesService;

        public PedidoService(IPedidoRepository repository, IEmailSender emailSender, IGenericRepository<User> userRepository, IMesService mesService)
        {
            this.repository = repository;
            this.emailSender = emailSender;
            _userRepository = userRepository;
            this.mesService = mesService;
        }

        public async Task<Relatorio> ConfirmPedidoAsync(uint pedidoId)
        {
            var pedido = await repository.ConfirmPedidoAsync(pedidoId);
            var dadosMes = await mesService.CreateOrUpdateAsync(pedido);
            return dadosMes;
        }

        public async Task<(bool success, string sms, PedidoDTO? pedido)> FazerPedidoAsync(int userId, decimal preco)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is not null)
            {
                if(preco < 0)
                {
                    return (false, "o valor deve ser positivo", null);
                }
                var query = new Pedido
                {
                    DataPedido = DateTime.UtcNow,
                    ValorPedido = preco,
                    User = user,

                };
                var email = new EmailDTO
                {
                    Nome = "Cota Sílva",
                    Subject = "Pedido de saldo",
                    Message = $"{user.Nome} Fez um pedido de saldo de {preco} \n por favor, atende-o o mais rápido possível",
                    To = "edvaldojoao520@gmail.com"
                };
                var create = repository.CreateAsync(query);
                var send = emailSender.SendEmailAsync(email);
                await Task.WhenAll(create, send);
                var completed2 = await send;
                if (!completed2.success)
                {
                    return (false, "Erro ao enviar o email", null);
                }
                var completed = await create;
                return (true, "A sua solicitação foi feita", new PedidoDTO
                {
                    UserName = user.Nome,
                    PedidoId = completed.PedidoId,
                    DataPedido = completed.DataPedido,
                    ValorPedido = completed.ValorPedido
                });
            }
            return (false, "usuário não encontrado", null);
        }

        public async Task<List<PedidoDTO>> GetAllAsync()=>
            await repository.GetAllPedidosAsync();
        public async Task<List<PedidoDTO>?> GetAllByUserAsync(int userId) 
        {
            var user = await repository.GetAllByUserAsync(userId);
            return user?.Pedidos.Select(p => new PedidoDTO
                {
                    UserName = p.User.Nome,
                    PedidoId = p.PedidoId,
                    DataPedido = p.DataPedido,
                    ValorPedido = p.ValorPedido,
                    isAceite = p.isAceite
                })
                .ToList();
        }
            
    }
}
