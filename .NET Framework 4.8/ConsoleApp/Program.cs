using ConsoleApp1.Model.Helper;
using ConsoleApp1.Model.Service;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region DB 접속정보 및 접속 테스트
            ConnectionHelper.TestDatabaseConnection();
            #endregion
            
            Console.ReadKey();
        }
    }
}
