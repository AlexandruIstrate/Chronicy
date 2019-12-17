using Chronicy.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chronicy.Tracking
{
    [DataContract]
    public class TrackingData
    {
        [DataMember]
        public string Name { get; protected set; }

        [DataMember]
        public string Comment { get; protected set; }

        [DataMember]
        public DateTime Date { get; protected set; }

        [DataMember]
        public List<CustomField> Fields { get; protected set; }

        [DataMember]
        public List<Tag> Tags { get; protected set; }

        public TrackingData(string name, string comment, List<CustomField> fields, List<Tag> tags)
        {
            Name = name;
            Comment = comment;
            Date = DateTime.Now;
            Fields = fields;
            Tags = tags;
        }
    }
}
