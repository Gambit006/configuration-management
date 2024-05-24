using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement
{
    public class ConfigurationReader
    {
        public string applicationName;
        public string connectionString;
        public int refreshTimerIntervalInMs;

        public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {
            this.applicationName = applicationName;
            this.connectionString = connectionString;
            this.refreshTimerIntervalInMs = refreshTimerIntervalInMs;
        }


        public bool GetValue<T>(string key)
        {
            return true;
        }
    }
}
