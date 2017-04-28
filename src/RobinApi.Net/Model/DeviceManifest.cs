using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class DeviceManifest
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        [DataMember(Name = "is_relay", EmitDefaultValue = false)]
        public bool IsRelay { get; set; }

        [DataMember(Name = "presence_publisher_type", EmitDefaultValue = false)]
        public string PresencePublisherType { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "device_feeds", EmitDefaultValue = false)]
        public DeviceFeed[] DeviceFeeds { get; set; }
    }
}
