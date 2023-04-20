using ConsoleApp.Model.Helper;
using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Model.Service
{
    public class OracleService
    {
        public static async Task GetVersion()
        {
            using (var conn = ConnectionHelper.OracleConnection())
            {
                await conn.OpenAsync();

                var query = "SELECT * FROM v$version WHERE banner LIKE 'Oracle%'";

                var results = await conn.QueryAsync<string>(query);
                var result = results.SingleOrDefault();

                Console.WriteLine(result);
            }
        }
    }
}
