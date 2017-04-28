using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class PrimaryEmail
    {
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(Name = "is_verified", EmitDefaultValue = false)]
        public bool IsVerified { get; set; }
    }
}
