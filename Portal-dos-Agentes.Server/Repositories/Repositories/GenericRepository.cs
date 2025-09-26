using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Context;
using Portal_dos_Agentes.Server.Repositories.Interfaces;

namespace Portal_dos_Agentes.Server.Repositories.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Entity> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
            _dbSet = context.Set<Entity>();
        }

        public async Task<Entity?> CreateAsync(Entity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await SaveAsync();
                return entity;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error creating entity: {ex.Message}");
                return null;
            }
        }

        public async Task<Entity> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _dbSet.Remove(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<List<Entity>> GetAllAsync() => 
            await _dbSet
            .AsNoTracking()
            .ToListAsync();

        public async Task<Entity?> GetByIdAsync(int id) => 
            await _dbSet.FindAsync(id);


        public async Task<Entity> UpdateAsync(Entity entity)
        {
            _dbSet.Update(entity);
            await SaveAsync();
            return entity;
        }
        public async Task<bool> ExistsAsync(int id) => 
            await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);

        private async Task SaveAsync()=>
            await context.SaveChangesAsync();

    }
}
