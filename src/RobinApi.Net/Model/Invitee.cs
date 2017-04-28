using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Invitee
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "event_id", EmitDefaultValue = false)]
        public string EventId { get; set; }

        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public int UserId { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(Name = "display_name", EmitDefaultValue = false)]
        public string DisplayName { get; set; }

        [DataMember(Name = "response_status", EmitDefaultValue = false)]
        public string ResponseStatus { get; set; }

        [DataMember(Name = "is_organizer", EmitDefaultValue = false)]
        public bool IsOrganizer { get; set; }

        [DataMember(Name = "is_resource", EmitDefaultValue = false)]
        public bool IsResource { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }
    }
}
