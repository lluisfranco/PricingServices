using System;
using System.Data.SqlClient;

namespace PricingServices.Client.Win
{
    public static class SqlConnectionExtension
    {
        public static SqlConnection AddMessagesManagement(this SqlConnection con,
            System.Threading.SynchronizationContext SyncContext, EventHandler<SqlMessageEventArgs> InfoMessage)
        {
            if (SyncContext == null) return con;
            if (con.FireInfoMessageEventOnUserErrors) return con;
            con.FireInfoMessageEventOnUserErrors = true;
            con.InfoMessage += (s, e) =>
                SyncContext.Post(_ => InfoMessage?.Invoke(
                s, new SqlMessageEventArgs
                {
                    Message = e.Message,
                    Progress = e.Errors[0]?.State ?? 0
                }), null);
            return con;
        }
    }

}
