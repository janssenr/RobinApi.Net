using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Presence
    {
        [DataMember(Name = "location_id", EmitDefaultValue = false)]
        public int LocationId { get; set; }

        [DataMember(Name = "space_id", EmitDefaultValue = false)]
        public int? SpaceId { get; set; }

        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public int? UserId { get; set; }

        [DataMember(Name = "device_id", EmitDefaultValue = false)]
        public int? DeviceId { get; set; }

        [DataMember(Name = "last_seen_at", EmitDefaultValue = false)]
        public DateTime LastSeenAt { get; set; }

        [DataMember(Name = "arrived_at", EmitDefaultValue = false)]
        public DateTime ArrivedAt { get; set; }

        [DataMember(Name = "expired_at", EmitDefaultValue = false)]
        public DateTime ExpiredAt { get; set; }

        [DataMember(Name = "session_ttl", EmitDefaultValue = false)]
        public int SessionTtl { get; set; }

        [DataMember(Name = "session_active", EmitDefaultValue = false)]
        public bool SessionActive { get; set; }

        [DataMember(Name = "user", EmitDefaultValue = false)]
        public User User { get; set; }

        [DataMember(Name = "device", EmitDefaultValue = false)]
        public Device Device { get; set; }

        [DataMember(Name = "user_ref", EmitDefaultValue = false)]
        public string UserRef { get; set; }
    }
}
