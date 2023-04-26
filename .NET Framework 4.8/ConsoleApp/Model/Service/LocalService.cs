using ConsoleApp1.Model.Helper;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Service
{
    public class LocalService
    {

        public static async Task GetVersion()
        {
            try
            {
                using (var conn = ConnectionHelper.LocalConnection())
                {
                    await conn.OpenAsync();

                    var query = "SELECT @@VERSION";

                    var results = await conn.QueryAsync<string>(query, commandType: CommandType.Text);
                    var result = results.SingleOrDefault();

                    NlogHelper.LogWrite(result, NlogHelper.LogType.Debug);
                }
            }
            catch(Exception e)
            {
                NlogHelper.LogWrite(e.ToString());
            }
        }
    }
}
