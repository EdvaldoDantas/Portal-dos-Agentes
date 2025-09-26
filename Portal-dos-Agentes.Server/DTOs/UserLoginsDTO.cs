

namespace Portal_dos_Agentes.Server.DTOs
{
    public class UserLoginsDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Token { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsRevoked { get; set; }
    }
}
