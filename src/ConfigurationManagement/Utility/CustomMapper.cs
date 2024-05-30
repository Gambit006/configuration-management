using ConfigurationManagement.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManagement.Utility
{
    public static class CustomMapper
    {
        public static List<ConfigurationRecord> ConfigurationModelMapping(List<BsonDocument> documents)
        {
            var configurations = new List<ConfigurationRecord>();
            foreach (var document in documents)
            {
                var id = document["Id"].AsInt32;
                var name = document["Name"].AsString;
                var type = document["Type"].AsString;
                var value = document["Value"].ToString();
                var isActive = document["IsActive"].AsBoolean;

                configurations.Add(new ConfigurationRecord(id, name, type, ConvertType.ConvertToType(value, type), isActive));


            }
            return configurations;
        }
    }
}
