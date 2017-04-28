using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class UserAccess
    {
        [DataMember(Name = "management_level", EmitDefaultValue = false)]
        public int ManagementLevel { get; set; }

        [DataMember(Name = "is_owner", EmitDefaultValue = false)]
        public bool IsOwner { get; set; }
    }
}
