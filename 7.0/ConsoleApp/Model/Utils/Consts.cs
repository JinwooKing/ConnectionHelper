using Microsoft.Extensions.Configuration;

namespace ConsoleApp2.Model.Utils
{
    public class Consts
    {
        //launchSettings.json의 값을 가져온다.
        //private static readonly string? environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        // appsettings.json 파일 로드
        private static readonly IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        //appsettings.Development.json 파일 미존재
                        //.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                        .Build();

        // ConnectionString 가져오기
        public static readonly string? local = configuration.GetConnectionString("local");
        public static readonly string? mssql = configuration.GetConnectionString("mssql");
        public static readonly string? oracle = configuration.GetConnectionString("oracle");
        public static readonly string? mariadb = configuration.GetConnectionString("mariadb");
    }
}
