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
        private List<ConfigurationModel> records;

        //variables for timer
        private Timer timer;
        private DateTime lastRefreshTimerIntervalInMs = DateTime.MinValue;



        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            this.applicationName = applicationName;
            this.connectionString = connectionString;
            this.refreshTimerIntervalInMs = refreshTimerIntervalInMs;

            ConfigurationRecordsUtilities connection = new ConfigurationRecordsUtilities(connectionString);
            records = connection.GetConfigurationRecords(applicationName);

            RefreshConfigruation();
        }

        private void RefreshConfigruation()
        {
            timer = new Timer(GetRecordFromDb, null, 0, refreshTimerIntervalInMs);

        }

        private void GetRecordFromDb(object state)
        {
            ConfigurationRecordsUtilities connection = new ConfigurationRecordsUtilities(connectionString);
            records = connection.GetConfigurationRecords(applicationName);
        }


        public T? GetValue<T>(string key)
        {
            var record = records.Where(r => r.Name == key).FirstOrDefault();

            if (record != null)
                return (T?)record.Value;
            else
                throw new Exception("Cannot found value!");
        }
    }
}
