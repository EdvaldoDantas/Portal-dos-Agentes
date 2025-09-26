namespace Portal_dos_Agentes.Server.Models;

public class UserLogins
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime LoginDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsRevoked { get; set; }
    public User User { get; set; }
}