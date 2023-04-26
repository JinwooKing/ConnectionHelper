﻿using ConsoleApp2.Model.Helper;
using ConsoleApp2.Model.Utils;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ConsoleApp2.Model.Service
{
    public class OracleService
    {
        public static async Task GetVersion()
        {
            try
            {
                using (var conn = new OracleConnection(Consts.oracle))
                {
                    await conn.OpenAsync();

                    var query = "SELECT * FROM v$version WHERE banner LIKE 'Oracle%'";

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
    }
}