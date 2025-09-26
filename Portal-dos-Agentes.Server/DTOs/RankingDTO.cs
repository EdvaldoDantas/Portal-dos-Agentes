using System;

namespace Portal_dos_Agentes.Server.DTOs;

public class RankingDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public decimal Total { get; set; }
}
