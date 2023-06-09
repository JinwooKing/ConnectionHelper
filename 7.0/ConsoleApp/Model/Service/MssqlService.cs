﻿using ConsoleApp2.Model.Domain;
using ConsoleApp2.Model.Helper;
using ConsoleApp2.Model.Utils;
using Dapper;
using DapperExtensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ConsoleApp2.Model.Service
{
    public class MssqlService
    {

        public static async Task GetVersion()
        {
            try
            {
                using (var conn = new SqlConnection(Consts.mssql))
                {
                    await conn.OpenAsync();

                    var query = "SELECT @@VERSION";

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
                using (var conn = new SqlConnection(Consts.mssql))
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
