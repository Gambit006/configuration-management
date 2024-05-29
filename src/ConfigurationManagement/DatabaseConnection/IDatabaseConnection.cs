using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.DatabaseConnection
{
    public interface IDatabaseConnection
    {
        public void Open();
        public Task OpenAsync();

        public List<ConfigurationRecord> GetConfigurationRecords();
    }
}
