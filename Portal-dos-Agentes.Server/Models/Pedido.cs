using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        [Required]

        public DateTime DataPedido { get; set; } = DateTime.UtcNow;
        
        [Required]
        public decimal ValorPedido { get; set; }

        public bool isAceite { get; set; } = false;
        public DateTime DataAceite { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
    }
}
