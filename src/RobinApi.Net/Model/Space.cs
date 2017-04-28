using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Space
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "location_id", EmitDefaultValue = false)]
        public int LocationId { get; set; }

        [DataMember(Name = "level_id", EmitDefaultValue = false)]
        public int? LevelId { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "image", EmitDefaultValue = false)]
        public string Image { get; set; }

        [DataMember(Name = "discovery_radius", EmitDefaultValue = false)]
        public double DiscoveryRadius { get; set; }

        [DataMember(Name = "capacity", EmitDefaultValue = false)]
        public int? Capacity { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "last_presence_at", EmitDefaultValue = false)]
        public string LastPresenceAt { get; set; }

        [DataMember(Name = "is_disabled", EmitDefaultValue = false)]
        public bool IsDisabled { get; set; }

        [DataMember(Name = "is_dibsed", EmitDefaultValue = false)]
        public bool IsDibsed { get; set; }

        [DataMember(Name = "is_accessible", EmitDefaultValue = false)]
        public bool IsAccessible { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public Location Location { get; set; }

        [DataMember(Name = "calendar", EmitDefaultValue = false)]
        public Calendar Calendar { get; set; }
    }
}
