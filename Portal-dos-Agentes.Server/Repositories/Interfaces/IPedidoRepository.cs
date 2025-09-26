using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;

namespace Portal_dos_Agentes.Server.Repositories.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<Pedido>
    {
        Task<User?> GetAllByUserAsync(int userId);
        Task<List<PedidoDTO>> GetAllPedidosAsync();
        Task<bool> ConfirmPedidoAsync(int pedidoId);

        Task<Pedido> ConfirmPedidoAsync(uint pedidoId);
    }
}
