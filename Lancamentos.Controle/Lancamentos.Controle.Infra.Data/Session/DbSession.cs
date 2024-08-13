using System.Data.Common;

namespace Lancamentos.Controle.Infra.Data.Session
{
    public class DbSession : IDisposable
    {
        public DbConnection Connection { get; }

        public DbTransaction Transaction { get; set; }

        public DbSession(DbConnection connection)
        {
            Connection = connection;
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
