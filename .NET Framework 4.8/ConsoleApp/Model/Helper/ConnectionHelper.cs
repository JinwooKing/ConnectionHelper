using System.Text;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlConnectionStringBuilder = Microsoft.Data.SqlClient.SqlConnectionStringBuilder;
using SqlConnectionColumnEncryptionSetting = Microsoft.Data.SqlClient.SqlConnectionColumnEncryptionSetting;
using Oracle.ManagedDataAccess.Client;
using MySqlConnector;
using ConsoleApp1.Model.Service;
using System.Threading.Tasks;

namespace ConsoleApp1.Model.Helper
{
    public class ConnectionHelper
    {

        private static MssqlConnectionStringBuilder local = new MssqlConnectionStringBuilder("Local");
        private static MssqlConnectionStringBuilder mssql = new MssqlConnectionStringBuilder("Mssql");
        private static OracleConnectionStringBuilder oracle = new OracleConnectionStringBuilder("Oracle");
        private static MysqlConnectionStringBuilder Mysql = new MysqlConnectionStringBuilder("Mariadb");

        public static SqlConnection LocalConnection()
        {
            return new SqlConnection(local.ConnectionString);
        }

        public static SqlConnection MssqlConnection()
        {
            return new SqlConnection(mssql.ConnectionString);
        }

        public static OracleConnection OracleConnection()
        {
            return new OracleConnection(oracle.ConnectionString);
        }

        public static MySqlConnection MysqlConnection()
        {
            return new MySqlConnection(Mysql.ConnectionString);
        }

        private class MssqlConnectionStringBuilder : ConnectionStringBuilder
        {
            private SqlConnectionStringBuilder sqlConnectionStringBuilder;
            public MssqlConnectionStringBuilder(string dbName) : base(dbName)
            {
            }

            public string ConnectionString
            {
                get
                {
                    if (sqlConnectionStringBuilder is null)
                    {
                        sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
                        {
                            DataSource = server,
                            UserID = uid,
                            Password = pwd,
                            InitialCatalog = database,
                            Encrypt = true,
                            TrustServerCertificate = true,
                            ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled
                        };

                        return sqlConnectionStringBuilder.ConnectionString;
                    }

                    return sqlConnectionStringBuilder.ConnectionString;
                }
            }
        }

        private class OracleConnectionStringBuilder : ConnectionStringBuilder
        {
            public OracleConnectionStringBuilder(string dbName) : base(dbName)
            {
            }

            public string ConnectionString
            {
                get
                {
                    string ConnectionString = $@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {server})(PORT = {port}))) 
                                                        (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = {sid}))); User Id={uid};Password={pwd};";

                    return ConnectionString;
                }
            }
        }

        private class MysqlConnectionStringBuilder : ConnectionStringBuilder
        {
            public MysqlConnectionStringBuilder(string dbName) : base(dbName)
            {
            }

            public string ConnectionString
            {
                get
                {
                    string ConnectionString = $@"server={server};port={port};database={database};user={uid};password={pwd};charset=utf8;sslmode=none;";

                    return ConnectionString;
                }
            }
        }

        private class ConnectionStringBuilder
        {
            #region 속성
            public string server { get; private set; }
            public string database { get; private set; }
            public string uid { get; private set; }
            public string pwd { get; private set; }
            public string port { get; private set; }
            public string sid { get; private set; }
            #endregion

            public ConnectionStringBuilder(string dbName)
            {
                GetPrivateProfileString(dbName);
            }

            private void GetPrivateProfileString(string dbName)
            {
                server = IniHelper.GetPrivateProfileString(dbName, "Server");
                database = IniHelper.GetPrivateProfileString(dbName, "Database");
                uid = IniHelper.GetPrivateProfileString(dbName, "Uid");
                pwd = IniHelper.GetPrivateProfileString(dbName, "Pwd");
                port = IniHelper.GetPrivateProfileString(dbName, "Port");
                sid = IniHelper.GetPrivateProfileString(dbName, "Sid");
            }
        }

        public async static Task TestDatabaseConnection()
        {
            NlogHelper.LogWrite(ConnectionHelper.LocalConnection().ConnectionString, NlogHelper.LogType.Debug);
            NlogHelper.LogWrite(ConnectionHelper.MssqlConnection().ConnectionString, NlogHelper.LogType.Debug);
            NlogHelper.LogWrite(ConnectionHelper.OracleConnection().ConnectionString, NlogHelper.LogType.Debug);
            NlogHelper.LogWrite(ConnectionHelper.MysqlConnection().ConnectionString, NlogHelper.LogType.Debug);

            LocalService.GetVersion();
            MssqlService.GetVersion();
            OracleService.GetVersion();
            MysqlService.GetVerison();
        }
    }
}
