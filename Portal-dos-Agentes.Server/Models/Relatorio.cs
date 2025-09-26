using System.ComponentModel.DataAnnotations;

namespace Portal_dos_Agentes.Server.Models;

public class Relatorio
{
    [Key]
    public int MesId { get; set; }
    public int Ano {  get; set; }
    public int Mes { get; set; }
    public int Dia { get; set; }
    public string Hora { get; set; }
    public decimal Total { get; set; }
    [Required]
    public User User { get; set; }
}