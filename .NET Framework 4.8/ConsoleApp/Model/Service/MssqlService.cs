using ConsoleApp1.Model.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Service
{
    public class MssqlService
    {
        public static async Task GetVersion()
        {
            try
            {
                using (var conn = ConnectionHelper.MssqlConnection())
                {
                    await conn.OpenAsync();
                    
                    var query = "SELECT @@VERSION";

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
