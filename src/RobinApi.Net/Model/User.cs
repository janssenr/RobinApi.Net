using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class User
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "slug", EmitDefaultValue = false)]
        public string Slug { get; set; }

        [DataMember(Name = "avatar", EmitDefaultValue = false)]
        public string Avatar { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "primary_email", EmitDefaultValue = false)]
        public PrimaryEmail PrimaryEmail { get; set; }

        [DataMember(Name = "user_status", EmitDefaultValue = false)]
        public UserStatus UserStatus { get; set; }

        [DataMember(Name = "is_pending", EmitDefaultValue = false)]
        public bool? IsPending { get; set; }

        [DataMember(Name = "user_access", EmitDefaultValue = false)]
        public UserAccess UserAccess { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }
    }
}
