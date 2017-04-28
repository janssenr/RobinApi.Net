using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class DateTimeZone
    {
        [DataMember(Name = "date_time", EmitDefaultValue = false)]
        public DateTime DateTime { get; set; }

        [DataMember(Name = "time_zone", EmitDefaultValue = false)]
        public string TimeZone { get; set; }
    }
}
