using ConsoleApp.Model.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model.Service
{
    public class MysqlService
    {
        public static async Task GetVerison()
        {
            using (var conn = ConnectionHelper.MysqlConnection())
            {
                await conn.OpenAsync();
                
                var query = "SELECT VERSION()";

                var results = await conn.QueryAsync<string>(query);
                var result = results.SingleOrDefault();

                Console.WriteLine(result);
            }
        }
    }
}
