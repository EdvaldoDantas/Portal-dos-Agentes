using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.DTOs
{
    public class EmailDTO
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Required]
        public string Subject { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string To { get; set; } = null!;
    }
}
