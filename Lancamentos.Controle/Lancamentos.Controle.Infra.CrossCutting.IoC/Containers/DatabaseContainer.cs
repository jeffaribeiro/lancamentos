using Lancamentos.Controle.Infra.Data.Session;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Data.SqlClient;

namespace Lancamentos.Controle.Infra.CrossCutting.IoC.Containers
{
    public class DatabaseContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbConnection>(provider =>
            {
                return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<DbSession>();
        }
    }
}
