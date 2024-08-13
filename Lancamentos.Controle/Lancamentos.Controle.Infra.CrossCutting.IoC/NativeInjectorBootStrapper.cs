using Lancamentos.Controle.Infra.CrossCutting.IoC.Containers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lancamentos.Controle.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            DatabaseContainer.RegisterServices(services, configuration);
            RepositoriesContainer.RegisterServices(services, configuration);
            MediatRContainer.RegisterServices(services, configuration);
            ApplicationContainer.RegisterServices(services, configuration);
        }
    }
}
