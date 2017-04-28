using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RobinApi.Net.Wrappers
{
    [DataContract]
    public class MetaWrapper
    {
        [DataMember(Name = "status_code", EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        [DataMember(Name = "more_info", EmitDefaultValue = false)]
        public Dictionary<string, string[]> MoreInfo { get; set; }
    }
}
