using ConfigurationManagement.DatabaseConnection;
using ConfigurationManagement.DatabaseConnection.MongoDbConnection;
using ConfigurationManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement
{
    public class ConfigurationReader
    {
        //variables that set from program
        public string applicationName;
        public string connectionString;
        public int refreshTimerIntervalInMs;

        // list of config records
        private List<ConfigurationRecord> records;

        //variables for timer
        private Timer timer;



        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            this.applicationName = applicationName;
            this.connectionString = connectionString;
            this.refreshTimerIntervalInMs = refreshTimerIntervalInMs;

            IDatabaseConnection connection = new MongoDbConnection(connectionString, applicationName);
            connection.Open();
            records = connection.GetConfigurationRecords();

            RefreshConfigruation();
        }

        private void RefreshConfigruation()
        {
            timer = new Timer(GetRecordFromDb, null, 0, refreshTimerIntervalInMs);

        }

        private void GetRecordFromDb(object state)
        {
            IDatabaseConnection connection = new MongoDbConnection(connectionString, applicationName);
            connection.Open();
            var refreshRecords = connection.GetConfigurationRecords();
            if (refreshRecords.Count != 0)
                records = refreshRecords;
        }


        public T? GetValue<T>(string key)
        {
            var record = records.Where(r => r.Name == key && r.IsActive == true).FirstOrDefault();

            if (record != null)
                return (T?)record.Value;
            else
                throw new Exception("Cannot found value!");
        }
    }
}
