using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PricingServices.Client.Win
{
    public static class ConnectionHelper
    {
        private static string GetExceptionMsg(
            SqlConnection con, CommandDefinition commandDefinition, Exception ex)
        {
            return
                $"MESSAGE:\n{GetInnerEx(ex).Message}\n" +
                $"COMMAND:\n{GetSuccessMsg(commandDefinition)}\n" +
                $"CONNECTION:\n{con.ConnectionString}\n";
        }

        private static Exception GetInnerEx(Exception ex)
        {
            return ex.InnerException ?? ex;
        }

        private static string GetSuccessMsg(CommandDefinition cmd)
        {
            return
                $"TEXT={cmd.CommandText}\n" +
                $"PARAMS={cmd.Parameters}\n" +
                $"TIMEOUT={cmd.CommandTimeout}";
        }

        private static string GetCancelledErrorMsg(CommandDefinition cmd, TaskCanceledException ex = null)
        {
            return
                $"* Canceled Error CMD:{cmd.CommandText} " +
                $"PARAMS:{cmd.Parameters} " +
                $"TIMEOUT:{cmd.CommandTimeout} " +
                $"TOKEN:{cmd.CancellationToken.IsCancellationRequested} " +
                $"EX:{GetInnerEx(ex)} ";
        }

        private static string GetSqlErrorMsg(CommandDefinition cmd, SqlException ex = null)
        {
            return
                $"* SQL Error CMD:{cmd.CommandText} " +
                $"PARAMS:{cmd.Parameters} " +
                $"TIMEOUT:{cmd.CommandTimeout} " +
                $"TOKEN:{cmd.CancellationToken.IsCancellationRequested} " +
                $"EX:{GetInnerEx(ex)} ";
        }

        private static string GetErrorMsg(CommandDefinition cmd, Exception ex = null)
        {
            return
                $"* Error CMD:{cmd.CommandText} " +
                $"PARAMS:{cmd.Parameters} " +
                $"TIMEOUT:{cmd.CommandTimeout} " +
                $"TOKEN:{cmd.CancellationToken.IsCancellationRequested} " +
                $"EX:{GetInnerEx(ex)} ";
        }

        public static async Task<List<T>> QueryCmdAsync<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = await con.QueryAsync<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data.ToList();
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return new List<T>();
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static List<T> QueryCmd<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = con.Query<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data.ToList();
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return new List<T>();
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static async Task<T> QuerySingleCmdAsync<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = await con.QueryFirstOrDefaultAsync<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data;
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return default;
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static T QuerySingleCmd<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = con.QueryFirstOrDefault<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data;
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return default;
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static async Task<T> ExecScalarCmdAsync<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = await con.ExecuteScalarAsync<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data;
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return default;
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static T ExecScalarCmd<T>(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                var data = con.ExecuteScalar<T>(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
                return data;
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
                return default;
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static async Task ExecCmdAsync(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                await con.ExecuteAsync(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }

        public static void ExecCmd(this SqlConnection con,
            CommandDefinition commandDefinition, LogMemoryService log = null)
        {
            try
            {
                con.Execute(commandDefinition);
                log?.AddToLog(GetSuccessMsg(commandDefinition));
            }
            catch (TaskCanceledException tex)
            {
                log?.ErrorToLog(GetCancelledErrorMsg(commandDefinition, tex));
            }
            catch (Exception ex)
            {
                log?.ErrorToLog(GetErrorMsg(commandDefinition, ex));
                throw new Exception(
                    GetExceptionMsg(con, commandDefinition, ex));
            }
        }
    }

}
