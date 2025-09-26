using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.DTOs
{
    public class PedidoDTO
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        public DateTime DataPedido { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal ValorPedido { get; set; }

        public bool isAceite { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
