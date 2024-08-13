using Lancamentos.Controle.Application.Infra.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lancamentos.Controle.Infra.CrossCutting.IoC.Containers
{
    public class ApplicationContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<INotificador, Notificador>();
        }
    }
}
