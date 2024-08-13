using Lancamentos.Controle.Application.Infra.Repositories;
using Lancamentos.Controle.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lancamentos.Controle.Infra.CrossCutting.IoC.Containers
{
    public class RepositoriesContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }
    }
}
