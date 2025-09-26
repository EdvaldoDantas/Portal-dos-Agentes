namespace Portal_dos_Agentes.Server.Services.Interfaces
{
    public interface IAuthManager
    {
        Task RemoveLastLogin(int userId);
    }
}
