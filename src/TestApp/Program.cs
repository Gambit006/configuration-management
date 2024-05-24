using ConfigurationManagement;




ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "mongodb://localhost:27017", 10000);



int a = 0;
while (true)
{
    Console.WriteLine(a.ToString() + " - " + configurationReader.GetValue<string>("SiteName"));
    Thread.Sleep(1000); // 1000 milisaniye = 1 saniye
    a++;
}




