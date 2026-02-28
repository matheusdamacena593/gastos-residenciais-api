using GastosResidenciais.Domain.Repositories;
using GastosResidenciais.Domain.Repositories.Categorias;
using GastosResidenciais.Domain.Repositories.Pessoas;
using GastosResidenciais.Domain.Repositories.Relatorios;
using GastosResidenciais.Domain.Repositories.Transacoes;
using GastosResidenciais.Infrastructure.DataAccess;
using GastosResidenciais.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GastosResidenciais.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastruture(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);
            AddDbContext(services, configuration);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Pessoa
            services.AddScoped<IPessoaReadOnlyRepository, PessoaRepository>();
            services.AddScoped<IPessoaWriteOnlyRepository, PessoaRepository>();
            services.AddScoped<IPessoaUpdateOnlyRepository, PessoaRepository>();

            // Categoria
            services.AddScoped<ICategoriaReadOnlyRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaWriteOnlyRepository, CategoriaRepository>();

            // Transacao
            services.AddScoped<ITrancasaoReadOnlyRepository, TransacaoRepository>();
            services.AddScoped<ITransacaoWriteOnlyRepository, TransacaoRepository>();

            // Relatorio
            services.AddScoped<IRelatorioReadOnlyRepository, RelatorioRepository>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");
            
            services.AddDbContext<GastosResidenciaisDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
    }
}
