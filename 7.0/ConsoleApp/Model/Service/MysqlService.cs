using ConsoleApp2.Model.Domain;
using ConsoleApp2.Model.Helper;
using ConsoleApp2.Model.Utils;
using Dapper;
using DapperExtensions;
using MySqlConnector;
using System.Data;

namespace ConsoleApp2.Model.Service
{
    public class MysqlService
    {
        public static async Task GetVerison()
        {
            try
            {
                using (var conn = new MySqlConnection(Consts.mariadb))
                {
                    await conn.OpenAsync();

                    var query = "SELECT VERSION()";

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

        public static async Task Insert(Person person)
        {
            try
            {
                using (var conn = new MySqlConnection(Consts.mssql))
                {
                    await conn.InsertAsync(person);
                }
            }
            catch (Exception e)
            {
                NlogHelper.LogWrite(e.ToString());
            }
        }
    }
}
