using ConsoleApp.Model.Helper;
using Dapper;
using System;
using System.Linq;

namespace ConsoleApp.Model.Service
{
    public class LocalService
    {

        public static async void GetVersion()
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
