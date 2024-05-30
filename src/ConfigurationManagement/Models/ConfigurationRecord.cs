using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public bool IsActive { get; set; }
    }
}
