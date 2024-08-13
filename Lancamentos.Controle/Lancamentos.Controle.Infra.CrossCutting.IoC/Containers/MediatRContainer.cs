using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lancamentos.Controle.Infra.CrossCutting.IoC.Containers
{
    public class MediatRContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains("Lancamentos.Controle.Application")).ToArray();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        }
    }
}
