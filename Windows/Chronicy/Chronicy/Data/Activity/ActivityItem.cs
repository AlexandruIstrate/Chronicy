using System;

namespace Chronicy.Data.Activity
{
    public class ActivityItem
    {
        public int ID { get; set; }

        public string Message { get; set; }
        public string Component { get; set; }
        public DateTime Date { get; set; }
    }
}
