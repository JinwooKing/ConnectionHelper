using ConsoleApp.Model.Domain;
using ConsoleApp.Model.Helper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Model.Service
{
    public class LocalService
    {

        public static async Task GetVersion()
        { 
            using (var conn = ConnectionHelper.LocalConnection())
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
