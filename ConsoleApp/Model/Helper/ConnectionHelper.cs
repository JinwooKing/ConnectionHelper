using System.Text;
using SqlConnection = Microsoft.Data.SqlClient.SqlConnection;
using SqlConnectionStringBuilder = Microsoft.Data.SqlClient.SqlConnectionStringBuilder;
using SqlConnectionColumnEncryptionSetting = Microsoft.Data.SqlClient.SqlConnectionColumnEncryptionSetting;
using Oracle.ManagedDataAccess.Client;
using MySqlConnector;

namespace ConsoleApp.Model.Helper
{
    public class ConnectionHelper
    {
        private static MssqlConnectionStringBuilder local = new MssqlConnectionStringBuilder("Local");
        private static MssqlConnectionStringBuilder mssql = new MssqlConnectionStringBuilder("Mssql");
        private static OracleConnectionStringBuilder oracle = new OracleConnectionStringBuilder("Oracle");
        private static MysqlConnectionStringBuilder Mysql = new MysqlConnectionStringBuilder("Mariadb");

        private static SqlConnection localConnection;
        private static SqlConnection mssqlConnection;
        private static OracleConnection oracleConnection;
        private static MySqlConnection mysqlConnection;
        //Mysql 연결 추가

        public static SqlConnection LocalConnection()
        {
            if(localConnection is null)
                localConnection = new SqlConnection(local.ConnectionString);

            return localConnection;
        }

        public static SqlConnection MssqlConnection()
        {
            if (mssqlConnection is null)
                mssqlConnection = new SqlConnection(mssql.ConnectionString);

            return mssqlConnection;
        }

        public static OracleConnection OracleConnection()
        {
            if(oracleConnection is null)
                oracleConnection = new OracleConnection(oracle.ConnectionString);
            
            return oracleConnection;
        }

        public static MySqlConnection MysqlConnection()
        {
            if(mysqlConnection is null)
                mysqlConnection = new MySqlConnection(Mysql.ConnectionString);
            
            return mysqlConnection;
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
                    if(sqlConnectionStringBuilder is null)
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
                                                        (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe))); User Id={uid};Password={pwd};";

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
                    string ConnectionString = $@"server={server};user={uid};password={pwd};database={database}";

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
            public string server { get {  return _Server.ToString(); } }
            public string database { get { return _Database.ToString(); } }
            public string uid { get { return _Uid.ToString(); } }
            public string pwd { get { return _Pwd.ToString(); } }
            public string  port { get { return _Port.ToString(); } }
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
            }
        }

        #region SAMPLE
        /*
        public ConnectionHelper()
        {
            //OracleConnectionTest();
            //SqlServerConnectionTest();
        }

        /// <summary>
        /// SQL Server 데이터베이스
        /// </summary>
        public static void SqlServerConnectionTest()
        {
            //0. 접속정보
            string SERVER = "000.000.000.000";
            string UID = "<로그인>";
            string PWD = "<암호>";
            string DATABASE = "<데이터베이스 명>";

            //1. SqlConnectionStringBuilder 사용
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = SERVER,
                UserID = UID,
                Password = PWD,
                InitialCatalog = DATABASE,
                Encrypt = true,
                TrustServerCertificate = true,
                ColumnEncryptionSetting = SqlConnectionColumnEncryptionSetting.Enabled
            };

            //1-2. ConnectionString 사용
            //string ConnectionString = $"Data Source={SERVER};Initial Catalog={DATABASE};User ID={UID};Password={PWD}";
            //string ConnectionString = $"server={SERVER};database={DATABASE};uid={UID};pwd={PWD}";

            //2. 데이터베이스 연결 및 버전 조회
            using (SqlConnection conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                string sql = "SELECT @@VERSION";

                conn.Open();

                string rtnVal = conn.Query<string>(sql).First();
                Console.Write(rtnVal);
            }
        }
        /// <summary>
        /// Oracle 데이터베이스
        /// </summary>
        public static void OracleConnectionTest()
        {
            //0. 접속정보
            string SERVER = "000.000.000.000";
            string PORT = "<포트>";
            string UID = "<로그인>";
            string PWD = "<암호>";

            //1. ConnectionString 사용
            string ConnectionString = $@"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {SERVER})(PORT = {PORT}))) 
                                                        (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = xe))); User Id={UID};Password={PWD};";
            //"Data Source={DataSource};User Id={UserId};Password={Password};";

            //2. 데이터베이스 연결 및 버전 조회
            OracleConnection conn = new OracleConnection(ConnectionString);
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            
            cmd.CommandText = "SELECT SYSDATE FROM DUAL";
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine(reader[0]);
            }
        }
        */
        #endregion
    }
}
