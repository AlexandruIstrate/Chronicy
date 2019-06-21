using System;
using System.Runtime.Serialization;

namespace Chronicy.Tracking
{
    [DataContract]
    public class TrackingData
    {
        [DataMember]
        public DateTime DateDispatched { get; }

        public TrackingData()
        {
            DateDispatched = DateTime.Now;
        }
    }
}
