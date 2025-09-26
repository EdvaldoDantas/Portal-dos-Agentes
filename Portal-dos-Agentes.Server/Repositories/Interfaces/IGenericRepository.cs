namespace Portal_dos_Agentes.Server.Repositories.Interfaces
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> CreateAsync(Entity entity);
        Task<Entity> UpdateAsync(Entity entity);
        Task<Entity> DeleteAsync(int id);
        Task<Entity?> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task<bool> ExistsAsync(int id);
    }
}
