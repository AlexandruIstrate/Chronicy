using Chronicy.Data;
using System.Collections.Generic;

namespace Chronicy.Tracking
{
    public class TrackingDataBuilder
    {
        public string Name { private get;  set; }
        public string Comment { private get; set; }

        public List<CustomField> Fields { private get; set; }
        public List<Tag> Tags { private get; set; }

        public TrackingData Create()
        {
            return new TrackingData(Name, Comment, Fields, Tags);
        }
    }
}
