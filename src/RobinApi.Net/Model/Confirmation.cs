using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Confirmation
    {
        [DataMember(Name = "event_id", EmitDefaultValue = false)]
        public int EventId { get; set; }

        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public int? UserId { get; set; }

        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public int? DeviceId { get; set; }

        [DataMember(Name = "confirmed_at", EmitDefaultValue = false)]
        public DateTime ConfirmedAt { get; set; }
    }
}
