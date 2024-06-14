using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace ConfigurationManagement.Models
{
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
        [BsonId(IdGenerator = typeof(BsonObjectIdGenerator))]
        [NonSerialized]
        public BsonObjectId _id;
        [NonSerialized]
        public string ApplicationName;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool IsActive { get; set; }



    }

}
