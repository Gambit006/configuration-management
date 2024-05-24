using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationManagement
{
    public class ConfigurationRecordsUtilities
    {
        private string _connectionString;

        public ConfigurationRecordsUtilities(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<ConfigurationModel> GetConfigurationRecords(string applicationName)
        {
            //mongodb connection
            var client = new MongoClient(_connectionString);

            //created filter tp get active records of appName 
            var filter = Builders<BsonDocument>.Filter.Eq("ApplicationName", applicationName) & Builders<BsonDocument>.Filter.Eq("IsActive", true);


            //get records from db
            var documents = client.GetDatabase("ConfigruationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords").Find(filter).ToList();
            documents = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords").Find(_ => true).ToList();

            return ConfigurationModelMapping(documents);
        }

        private List<ConfigurationModel> ConfigurationModelMapping(List<BsonDocument> documents)
        {
            var configurations = new List<ConfigurationModel>();
            foreach (var document in documents)
            {
                var name = document["Name"].AsString;
                var type = document["Type"].AsString;
                var value = document["Value"].ToString();

                configurations.Add(new ConfigurationModel(name, ConvertToType(value, type)));
            }
            return configurations;
        }

        private object ConvertToType(string value, string type)
        {
            switch (type.ToLower())
            {
                case "string":
                    return value.ToString();
                case "int":
                    return Convert.ToInt32(value);
                case "bool":
                    return Convert.ToBoolean(Convert.ToInt32(value));
                case "double":
                    return Convert.ToDouble(value);
                default:
                    throw new Exception("Unknown type");
            }
        }
    }

    public class ConfigurationModel
    {
        public ConfigurationModel(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }
}
