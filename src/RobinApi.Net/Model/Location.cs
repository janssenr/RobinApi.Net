using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "account_id", EmitDefaultValue = false)]
        public int AccountId { get; set; }

        [DataMember(Name = "campus_id", EmitDefaultValue = false)]
        public int? CampusId { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "address", EmitDefaultValue = false)]
        public string Address { get; set; }

        [DataMember(Name = "image", EmitDefaultValue = false)]
        public string Image { get; set; }

        [DataMember(Name = "latitude", EmitDefaultValue = false)]
        public float? Latitude { get; set; }

        [DataMember(Name = "longitude", EmitDefaultValue = false)]
        public float? Longitude { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime UpdatedAt { get; set; }
    }
}
