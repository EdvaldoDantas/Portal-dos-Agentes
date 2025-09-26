
using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.DTOs
{
    public class UserDTO
    {
        [Key]
        [Required(ErrorMessage = "Id do agente obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; } = null!;


        [Required(ErrorMessage = "Numero de telefine obrigatório")]
        public string Telefone { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required]
        public string Email { get; set; } = null!;

        [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres", MinimumLength = 6), MaxLength(100)]
        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; } = null!;

    }
}
