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
        private string _applicationName;

        public ConfigurationRecordsUtilities(string connectionString, string applicationName)
        {
            _connectionString = connectionString;
            _applicationName = applicationName;
        }

        public List<ConfigurationRecord> GetConfigurationRecords()
        {
            //mongodb connection
            var client = new MongoClient(_connectionString);

            //created filter tp get active records of appName 
            var filter = Builders<BsonDocument>.Filter.Eq("ApplicationName", _applicationName);


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


        public static object ConvertToType(string value, string type)
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
        public static string ConvertFromType(object value)
        {
            if (value is string)
            {
                return value.ToString();
            }
            else if (value is int)
            {
                return value.ToString();
            }
            else if (value is bool)
            {
                // Boole değerini 0 veya 1 olarak döndür
                return ((bool)value) ? "1" : "0";
            }
            else if (value is double)
            {
                return value.ToString();
            }
            else
            {
                throw new Exception("Unknown type");
            }
        }

        public void UpdateRecord(ConfigurationRecord record)
        {
            var client = new MongoClient(_connectionString);
            var collection = client.GetDatabase("ConfigurationManagementDB").GetCollection<BsonDocument>("ConfigurationRecords");

            var filter = Builders<BsonDocument>.Filter.Eq("Id", record.Id);
            var update = Builders<BsonDocument>.Update
                .Set("Name", record.Name)
                .Set("Type", record.Type)
                .Set("Value", ConvertFromType(record.Value))
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
                { "Value", ConvertFromType(record.Value) },
                { "IsActive", record.IsActive },
                {"ApplicationName", _applicationName },
            };

            collection.InsertOne(document);
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
