using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Calendar
    {
        [DataMember(Name = "space_id", EmitDefaultValue = false)]
        public int SpaceId { get; set; }

        [DataMember(Name = "remote_type", EmitDefaultValue = false)]
        public string RemoteType { get;  set; }

        [DataMember(Name = "calendar_id", EmitDefaultValue = false)]
        public string CalendarId { get; set; }

        [DataMember(Name = "subscriber_id", EmitDefaultValue = false)]
        public string SubscriberId { get; set; }

        [DataMember(Name = "space_resource_id", EmitDefaultValue = false)]
        public string SpaceResourceId { get; set; }

        [DataMember(Name = "space_resource_email", EmitDefaultValue = false)]
        public string SpaceResourceEmail { get; set; }

        [DataMember(Name = "subscriber_expires_at", EmitDefaultValue = false)]
        public DateTime SubscriberExpiresAt { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }
    }
}
