using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.DatabaseConnection
{
    public class MongoDbConnection : IDatabaseConnection
    {
        private string _connectionString;
        private string _applicationName;
        private List<BsonDocument>? documents;

        public MongoDbConnection(string connectionString, string applicationName)
        {
            _connectionString = connectionString;
            _applicationName = applicationName;
        }

        public List<ConfigurationRecord> GetConfigurationRecords()
        {
            return  ConfigurationModelMapping(documents);
        }

        public void Open()
        {
            //mongodb connection
            var client = new MongoClient(_connectionString);

            //created filter tp get active records of appName 
            var filter = Builders<BsonDocument>.Filter.Eq("ApplicationName", _applicationName);

            //get records from db
            documents = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords").Find(filter).ToList();
        }

        public Task OpenAsync()
        {
            throw new NotImplementedException();
        }

    }
}
