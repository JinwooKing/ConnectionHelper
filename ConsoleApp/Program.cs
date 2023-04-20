using ConsoleApp.Model.Helper;
using ConsoleApp.Model.Service;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DoWork();
            
            Console.ReadKey();
        }

        static async void DoWork()
        {
            Console.WriteLine(ConnectionHelper.LocalConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.MssqlConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.OracleConnection().ConnectionString);
            Console.WriteLine(ConnectionHelper.MysqlConnection().ConnectionString);

            LocalService.GetVersion();
            MssqlService.GetVersion();
            //OracleService.GetVersion();
            //MysqlService.GetVerison(); 
        }
    }
}
