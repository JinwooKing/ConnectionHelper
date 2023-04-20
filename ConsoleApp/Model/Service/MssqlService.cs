using ConsoleApp.Model.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Model.Service
{
    public class MssqlService
    {
        public static async void GetVersion()
        {
            using (var conn = ConnectionHelper.MssqlConnection())
            {
                await conn.OpenAsync();

                var query = "SELECT @@VERSION";

                var results = await conn.QueryAsync<string>(query);
                var result = results.SingleOrDefault();

                Console.Write(result);
            }
        }
    }
}
