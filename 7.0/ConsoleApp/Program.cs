using ConsoleApp2.Model.Domain;
using ConsoleApp2.Model.Service;

// 1. 조회
//MssqlService.GetVersion();
//MysqlService.GetVerison();
//OracleService.GetVersion();

// 2. 입력
var p = new Person() { Name = "JINWOO", Age = 30 };

//DapperExtensions을 사용한 EntityFrameWork MSSQL만 가능
MssqlService.Insert(p);

//MysqlService.Insert(p);
//OracleService.Insert(p);



Console.ReadKey();