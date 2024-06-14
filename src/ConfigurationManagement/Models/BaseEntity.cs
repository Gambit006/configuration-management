using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConfigurationManagement.Models
{
    public class BaseEntity 
    {
        private Guid _idfield;

        [DataMember]
        public Guid Id
        {
            get { return _idfield; }
            set { _idfield = value; }
        }

        public Guid _id
        {
            get { return Id; }
            set { Id = value; }
        }
    }
}
