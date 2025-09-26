using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.DTOs
{
    public class UserSendDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public string? Endereco { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string Telefone { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
