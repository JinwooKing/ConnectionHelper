using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static ConsoleApp1.Model.Helper.ConnectionHelper;

namespace ConsoleApp1.Model.Helper
{
    public class IniHelper
    {
        //private static string currentPath = Directory.GetCurrentDirectory();
        private static string currentPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string filePath = Path.Combine(currentPath, "ConfigEx.ini");

        static IniHelper()
        { // GetPrivateProfileString("카테고리", "Key값", "기본값", "저장할 변수", "불러올 경로");
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
                WritePrivateProfileString("SAMPLE", "TEST", "1", filePath);
            }
        }

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
    }
}
