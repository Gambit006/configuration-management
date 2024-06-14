using ConfigurationManagement.Models;
using ConfigurationManagement.Utility;
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
            return CustomMapper.ConfigurationModelMapping(documents);
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

        public void UpdateRecord(ConfigurationRecord record)
        {
            var client = new MongoClient(_connectionString);
            var collection = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", record.Id);
            var update = Builders<BsonDocument>.Update
                .Set("Name", record.Name)
                .Set("Type", record.Type)
                .Set("Value", ConvertType.ConvertFromType(record.Value))
                .Set("IsActive", record.IsActive);

            collection.UpdateOne(filter, update);
        }

        public void DeleteRecord(int id)
        {
            var client = new MongoClient(_connectionString);
            var collection = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }

        public void InsertRecord(ConfigurationRecord record)
        {
            var client = new MongoClient(_connectionString);
            var collection = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords");

            var document = new BsonDocument
            {
                { "Id", record.Id },
                { "Name", record.Name },
                { "Type", record.Type },
                { "Value", ConvertType.ConvertFromType(record.Value) },
                { "IsActive", record.IsActive },
                {"ApplicationName", _applicationName },
            };

            collection.InsertOne(document);
        }

    }
}
