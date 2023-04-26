using ConsoleApp.Model.Helper;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Model.Service
{
    public class OracleService
    {
        public static async Task GetVersion()
        {
            try 
            { 
                using (var conn = ConnectionHelper.OracleConnection())
                {
                    await conn.OpenAsync();

                    var query = "SELECT * FROM v$version WHERE banner LIKE 'Oracle%'";

                    var results = await conn.QueryAsync<string>(query, commandType: CommandType.Text);
                    var result = results.SingleOrDefault();

                    NlogHelper.LogWrite(result, NlogHelper.LogType.Debug);
                }
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString());
            }
        }
    }
}
