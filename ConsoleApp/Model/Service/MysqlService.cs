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
        public static async void GetVerison()
        {
            using (var conn = ConnectionHelper.MssqlConnection())
            {
                await conn.OpenAsync();
                
                var query = "SELECT VERSION()";

                var results = await conn.QueryAsync<string>(query);
                var reulst = results.SingleOrDefault();

                Console.WriteLine(results);
            }
        }
    }
}
