using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class SpaceState
    {
        [DataMember(Name = "availability", EmitDefaultValue = false)]
        public string Availability { get; set; }

        [DataMember(Name = "present", EmitDefaultValue = false)]
        public int Present { get; set; }

        [DataMember(Name = "next_busy_change", EmitDefaultValue = false)]
        public DateTime? NextBusyChange { get; set; }
    }
}
