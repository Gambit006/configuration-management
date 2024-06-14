using ConfigurationManagement;
using ConfigurationManagement.DatabaseConnection;
using ConfigurationManagement.DatabaseConnection.MongoDbConnection;
using ConfigurationManagement.DatabaseConnection.SqlServerConnection;
using ConfigurationManagement.Models;


var mongoConnectionString = "mongodb://localhost:27017";
var mongoDatabaseName = "ConfigurationManagementDB";
var mongoContext = new MongoDbContext(mongoConnectionString, mongoDatabaseName);
IRepository<ConfigurationRecord> configurationRecordRepository = new MongoDbRepository<ConfigurationRecord>(mongoContext, "ConfigurationRecords");

Console.WriteLine(configurationRecordRepository.GetAllAsync().Result.FirstOrDefault().Value.ToString());


var sqlConnectionString = "Server=DESKTOP-BC7AGD6\\SQLEXPRESS;Database=ConfigurationManagementDB;Trusted_Connection=True;";
var sqlContext = new SqlServerDbContext(sqlConnectionString);
IRepository<ConfigurationRecord> configurationRecordRepositorySql = new SqlServerBaseRepository<ConfigurationRecord>(sqlContext, "ConfigurationRecords", new[] { "Id", "Name", "Type", "Value", "IsActive", "ApplicationName" });
Console.WriteLine(configurationRecordRepositorySql.GetAllAsync().Result.FirstOrDefault().Value.ToString());


//TODO: this code part might need to write again and agian, this decleration will be easy -> var mongoContext = DatabaseFactSory.CreateDbContext(DatabaseType.MongoDB, configuration) as MongoDbContext;
//TODO: Various db will be able to run correctly at the same time for different records  

//ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "mongodb://localhost:27017", 10000);



//int a = 0;
//while (true)
//{
//    Console.WriteLine(a.ToString() + " - " + configurationReader.GetValue<string>("SiteName"));
//    Thread.Sleep(1000); // 1000 milisaniye = 1 saniye
//    a++;
//}




