using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Device
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "account_id", EmitDefaultValue = false)]
        public int AccountId { get; set; }

        [DataMember(Name = "device_manifest_id", EmitDefaultValue = false)]
        public int DeviceManifestId { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "last_reported_at", EmitDefaultValue = false)]
        public DateTime LastReportedAt { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "identifiers", EmitDefaultValue = false)]
        public Identifier[] Identifiers { get; set; }

        [DataMember(Name = "device_manifest", EmitDefaultValue = false)]
        public DeviceManifest DeviceManifest { get; set; }
    }
}
