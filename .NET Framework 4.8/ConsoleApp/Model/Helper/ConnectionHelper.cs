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
            public StringBuilder _Server { get; set; } = new StringBuilder();
            public StringBuilder _Database { get; set; } = new StringBuilder();
            public StringBuilder _Uid { get; set; } = new StringBuilder();
            public StringBuilder _Pwd { get; set; } = new StringBuilder();
            public StringBuilder _Port { get; set; } = new StringBuilder();
            public StringBuilder _Sid { get; set; } = new StringBuilder();
            public string server { get { return _Server.ToString(); } }
            public string database { get { return _Database.ToString(); } }
            public string uid { get { return _Uid.ToString(); } }
            public string pwd { get { return _Pwd.ToString(); } }
            public string port { get { return _Port.ToString(); } }
            public string sid { get { return _Sid.ToString(); } }
            #endregion

            public ConnectionStringBuilder(string dbName)
            {
                GetPrivateProfileString(dbName);
            }

            private void GetPrivateProfileString(string dbName)
            {
                IniHelper.GetPrivateProfileString(dbName, "Server", "", _Server, 32, IniHelper.filePath);
                IniHelper.GetPrivateProfileString(dbName, "Database", "", _Database, 32, IniHelper.filePath);
                IniHelper.GetPrivateProfileString(dbName, "Uid", "", _Uid, 32, IniHelper.filePath);
                IniHelper.GetPrivateProfileString(dbName, "Pwd", "", _Pwd, 32, IniHelper.filePath);
                IniHelper.GetPrivateProfileString(dbName, "Port", "", _Port, 32, IniHelper.filePath);
                IniHelper.GetPrivateProfileString(dbName, "Sid", "", _Sid, 32, IniHelper.filePath);
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
