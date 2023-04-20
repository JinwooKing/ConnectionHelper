using ConsoleApp.Model.Helper;
using ConsoleApp.Model.Service;
using System;
namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ConnectionHelper.LocalConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.MssqlConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.OracleConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.MysqlConnection().ConnectionString);

            LocalService.GetVersion();
            MssqlService.GetVersion();
            //OracleService.GetVersion();
            //MysqlService.GetVerison(); 

            Console.ReadKey();
        }
    }
}
