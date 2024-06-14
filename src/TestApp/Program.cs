using ConfigurationManagement;
using ConfigurationManagement.DatabaseConnection;
using ConfigurationManagement.Models;


var mongoConnectionString = "mongodb://localhost:27017";
var mongoDatabaseName = "ConfigurationManagementDB";
var mongoContext = new MongoDbContext(mongoConnectionString, mongoDatabaseName);
IRepository<ConfigurationRecord> configurationRecordRepository = new MongoDbRepository<ConfigurationRecord>(mongoContext, "ConfigurationRecords");

Console.WriteLine(configurationRecordRepository.GetAllAsync().Result.FirstOrDefault().Value.ToString());
//TODO: type parse error must fix 


//ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "mongodb://localhost:27017", 10000);



//int a = 0;
//while (true)
//{
//    Console.WriteLine(a.ToString() + " - " + configurationReader.GetValue<string>("SiteName"));
//    Thread.Sleep(1000); // 1000 milisaniye = 1 saniye
//    a++;
//}




