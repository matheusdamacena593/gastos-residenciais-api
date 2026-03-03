using GastosResidenciais.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GastosResidenciais.Infrastructure.HostedServices
{
    public class DbStartupCheckHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DbStartupCheckHostedService> _logger;

        public DbStartupCheckHostedService(
            IServiceProvider serviceProvider,
            ILogger<DbStartupCheckHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            const int maxRetries = 10;
            var delay = TimeSpan.FromSeconds(2);

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<GastosResidenciaisDbContext>();

                try
                {
                    _logger.LogInformation(
                        "Verificando banco e aplicando migrations... (tentativa {Attempt}/{Max})",
                        attempt,
                        maxRetries
                    );

                    await db.Database.MigrateAsync(cancellationToken);

                    var ok = await db.Database.CanConnectAsync(cancellationToken);
                    if (!ok)
                        throw new Exception("Não foi possível conectar no banco de dados.");

                    _logger.LogInformation("Banco OK (migrations aplicadas e conexão validada).");
                    return;
                }
                catch (Exception ex) when (attempt < maxRetries)
                {
                    _logger.LogWarning(ex, "Banco ainda indisponível. Tentando novamente em {Delay}...", delay);
                    await Task.Delay(delay, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, "Banco indisponível. Encerrando aplicação.");
                    throw;
                }
            }
        }
        
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
