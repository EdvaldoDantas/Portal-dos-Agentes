using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Context;
namespace Portal_dos_Agentes.Server.Repositories.Repositories
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly ApplicationDbContext _context;
        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PedidoDTO>> GetAllPedidosAsync() =>
            await _context.Pedidos
                .AsNoTracking()
                .Include(p => p.User)
                .Select(p => new PedidoDTO
                {
                    PedidoId = p.PedidoId,
                    DataPedido = p.DataPedido,
                    ValorPedido = p.ValorPedido,
                    UserName = p.User.Nome,
                    isAceite = p.isAceite
                })
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();

        public async Task<User?> GetAllByUserAsync(int userId) =>
           await _context.Users
            .AsNoTracking()
            .Include(p => p.Pedidos)
            .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task<bool> ConfirmPedidoAsync(int pedidoId)
        {
            var pedido = await _context.Pedidos.SingleOrDefaultAsync(p => p.PedidoId == pedidoId);

            if (pedido == null)
                return false;

            if (pedido.isAceite)
                return true;

            pedido.isAceite = true;
            pedido.DataAceite = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Pedido> ConfirmPedidoAsync(uint pedidoId)
        {
            var pedido = await _context.Pedidos
                .Include(u => u.User)
                .SingleOrDefaultAsync(p => p.PedidoId == pedidoId) ?? throw new ArgumentException("Pedido não existe");
            if(!pedido.isAceite)
                pedido.isAceite = true;

            return pedido;
        }
    }
}
