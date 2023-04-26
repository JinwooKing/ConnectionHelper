using Microsoft.Extensions.Configuration;

namespace ConsoleApp2.Model.Utils
{
    public class Consts
    {
        // appsettings.json 파일 로드
        private static IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();

        // ConnectionString 가져오기
        public static string? local = configuration.GetConnectionString("local");
        public static string? mssql = configuration.GetConnectionString("mssql");
        public static string? oracle = configuration.GetConnectionString("oracle");
        public static string? mariadb = configuration.GetConnectionString("mariadb");
    }
}
