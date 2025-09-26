using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<(bool success, string sms, PedidoDTO? pedido)> FazerPedidoAsync(int userId, decimal preco);
        Task<List<PedidoDTO>> GetAllAsync();
        Task<List<PedidoDTO>?> GetAllByUserAsync(int userId);

        Task<Relatorio> ConfirmPedidoAsync(uint pedidoId);
    }
}
