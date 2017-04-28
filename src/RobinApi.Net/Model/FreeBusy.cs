using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class FreeBusy
    {
        [DataMember(Name = "has_presence", EmitDefaultValue = false)]
        public bool HasPresence { get; set; }

        [DataMember(Name = "space", EmitDefaultValue = false)]
        public Space Space { get; set; }

        [DataMember(Name = "user", EmitDefaultValue = false)]
        public User User { get; set; }

        [DataMember(Name = "busy", EmitDefaultValue = false)]
        public Busy[] Busy { get; set; }
    }
}
