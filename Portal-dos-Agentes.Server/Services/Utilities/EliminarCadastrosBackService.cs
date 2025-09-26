using Microsoft.EntityFrameworkCore;
using Portal_dos_Agentes.Server.Context;

namespace Portal_dos_Agentes.Server.Services.Utilities
{
    public class EliminarCadastrosBackService(ILogger<EliminarCadastrosBackService> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) {

                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var context = scope
                            .ServiceProvider
                            .GetRequiredService<ApplicationDbContext>();

                        var user = await context.Users
                            .Where(u => u.IsEmailConfirmed == false)
                            .ToListAsync(stoppingToken);
                        context.RemoveRange(user);
                        var result = await context.SaveChangesAsync(stoppingToken);
                        if (result > 0)
                        {
                            logger.LogWarning("Foram Eliminadas {result} contas depois da limpeza", DateTimeOffset.Now);
                        }
                    }

                    await Task.Delay(TimeSpan.FromDays(5), stoppingToken);
                    logger.LogInformation("Background service is stopping at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
