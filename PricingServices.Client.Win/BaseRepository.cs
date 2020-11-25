using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace PricingServices.Client.Win
{
    public class BaseRepository
    {
        //public static FalconConfiguration Configuration { get; set; }
        public event EventHandler<SqlMessageEventArgs> ProcessMessage;
        public string ConnectionString { get; protected set; }
        public SynchronizationContext SyncContext { get; protected set; }

        public BaseRepository(
            string connectionString, SynchronizationContext syncContext)
        {
            ConnectionString = connectionString;
            SyncContext = syncContext;
        }

        public string GetConnectionString() =>
            "data source=elsuper;initial catalog=Falcon;integrated security=True;MultipleActiveResultSets=True";

        public SqlConnection CreateConnection(
            string connectionString = null, bool open = true)
        {
            var con = new SqlConnection(
                connectionString ?? GetConnectionString());
            con.AddMessagesManagement(SyncContext, ProcessMessage);
            if (open) con.Open();
            return con;
        }

        public static CommandDefinition CreateCommand(
            string commandText, object parameters = null,
            IDbTransaction transaction = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            CommandFlags flags = CommandFlags.Buffered,
            CancellationToken cancellationToken = default)
        {
            return new CommandDefinition(
                commandText, parameters, transaction,
                commandTimeout ?? 60,
                commandType, flags, cancellationToken);
        }
    }

}
