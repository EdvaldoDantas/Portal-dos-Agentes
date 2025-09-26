using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Id do agente obrigatório")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        public string Nome { get; set; } = null!;

        public string? Endereco { get; set; }

        public DateOnly? DataNascimento { get; set; }

        [Required(ErrorMessage = "Numero de telefine obrigatório")]
        public string Telefone { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email inválido")]
        [Required]
        public string Email { get; set; } = null!;

        [StringLength(100, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres", MinimumLength = 6), MaxLength(100)]
        [Required(ErrorMessage = "Senha obrigatória")]
        public string Senha { get; set; } = null!;

        public string Role { get; set; } = null!;

        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        public ICollection<UserLogins> UserLogins { get; set; } = new List<UserLogins>();

        public ICollection<Relatorio> DadosMeses = new List<Relatorio>();

        public bool IsEmailConfirmed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
