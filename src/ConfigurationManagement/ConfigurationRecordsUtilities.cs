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

        public List<ConfigurationRecord> GetConfigurationRecords(string applicationName)
        {
            //mongodb connection
            var client = new MongoClient(_connectionString);

            //created filter tp get active records of appName 
            var filter = Builders<BsonDocument>.Filter.Eq("ApplicationName", applicationName);


            //get records from db
            var documents = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords").Find(filter).ToList();

            return ConfigurationModelMapping(documents);
        }

        private List<ConfigurationRecord> ConfigurationModelMapping(List<BsonDocument> documents)
        {
            var configurations = new List<ConfigurationRecord>();
            foreach (var document in documents)
            {
                var id = document["Id"].AsInt32;
                var name = document["Name"].AsString;
                var type = document["Type"].AsString;
                var value = document["Value"].ToString();
                var isActive = document["IsActive"].AsBoolean;

                configurations.Add(new ConfigurationRecord(id, name, type, ConvertToType(value, type), isActive));
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


    public class ConfigurationRecord
    {
        public ConfigurationRecord(int id, string name, string type, object value, bool isActive)
        {
            Id = id;
            Name = name;
            Type = type;
            Value = value;
            IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool IsActive { get; set; }
    }
}
