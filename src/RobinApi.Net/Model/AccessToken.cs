using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class AccessToken
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string Token { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "ip", EmitDefaultValue = false)]
        public string Ip { get; set; }

        [DataMember(Name = "agent", EmitDefaultValue = false)]
        public string Agent { get; set; }

        [DataMember(Name = "scopes", EmitDefaultValue = false)]
        public string[] Scopes { get; set; }

        [DataMember(Name = "last_accessed_at", EmitDefaultValue = false)]
        public DateTime LastAccessedAt { get; set; }

        [DataMember(Name = "expire_at", EmitDefaultValue = false)]
        public DateTime? ExpireAt { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }
    }
}
