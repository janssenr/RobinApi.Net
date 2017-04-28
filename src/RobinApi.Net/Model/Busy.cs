using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Busy
    {
        [DataMember(Name = "from", EmitDefaultValue = false)]
        public DateTime From { get; set; }

        [DataMember(Name = "to", EmitDefaultValue = false)]
        public DateTime To { get; set; }

        [DataMember(Name = "events", EmitDefaultValue = false)]
        public Event[] Events { get; set; }
    }
}
