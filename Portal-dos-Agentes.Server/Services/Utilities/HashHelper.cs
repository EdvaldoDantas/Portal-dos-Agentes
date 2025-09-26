using BCrypt.Net;
namespace Portal_dos_Agentes.Server.Services.Utilities
{
    public static class HashHelper
    {
        public static (string hash, string senha) Hashsenha(int id)
        {
            var guid = Guid.NewGuid()
                .ToString()
                .Substring(0, 4);
            string senha = guid + "-" + id;
            string hash = BCrypt.Net.BCrypt.HashPassword(senha);
            
            return (hash, senha);
        }
        public static string Hash(string text)=>
            BCrypt.Net.BCrypt.HashPassword(text);
        public static bool VerifyHash(string hash, string text) =>
             BCrypt.Net.BCrypt.Verify(text, hash);
    }
}
