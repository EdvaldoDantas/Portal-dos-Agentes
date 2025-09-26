using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.DTOs
{
    public class SubAgentDTO
    {
        [Key]
        [Required(ErrorMessage = "Id do agente obrigatório")]
        public int AgenteId { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "Numero de telefine obrigatório")]
        public string Telefone { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Endereço obrigatório")]
        public string Endereco { get; set; } = null!;
    }
}
