using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Identifier
    {
        [DataMember(Name = "urn", EmitDefaultValue = false)]
        public string Urn { get; set; }

        [DataMember(Name = "interface", EmitDefaultValue = false)]
        public string Interface { get; set; }

        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }
    }
}
