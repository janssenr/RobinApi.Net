using System;
using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
    [DataContract]
    public class Event
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int Id { get; set; }

        [DataMember(Name = "organizer_id", EmitDefaultValue = false)]
        public int? OrganizerId { get; set; }

        [DataMember(Name = "organizer_email", EmitDefaultValue = false)]
        public string OrganizerEmail { get; set; }

        [DataMember(Name = "creator_id", EmitDefaultValue = false)]
        public int? CreatorId { get; set; }

        [DataMember(Name = "creator_email", EmitDefaultValue = false)]
        public string CreatorEmail { get; set; }

        [DataMember(Name = "space_id", EmitDefaultValue = false)]
        public int SpaceId { get; set; }

        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        [DataMember(Name = "location", EmitDefaultValue = false)]
        public string Location { get; set; }

        [DataMember(Name = "remote_event_id", EmitDefaultValue = false)]
        public string RemoteEventId { get; set; }

        [DataMember(Name = "remote_type", EmitDefaultValue = false)]
        public string RemoteType { get; set; }

        [DataMember(Name = "creation_type", EmitDefaultValue = false)]
        public string CreationType { get; set; }

        [DataMember(Name = "uid", EmitDefaultValue = false)]
        public string Uid { get; set; }

        [DataMember(Name = "started_at", EmitDefaultValue = false)]
        [Obsolete("Use Start")]
        public DateTime StartedAt { get; set; }

        [DataMember(Name = "ended_at", EmitDefaultValue = false)]
        [Obsolete("Use End")]
        public DateTime EndedAt { get; set; }

        [DataMember(Name = "start", EmitDefaultValue = false)]
        public DateTimeZone Start { get; set; }

        [DataMember(Name = "end", EmitDefaultValue = false)]
        public DateTimeZone End { get; set; }

        [DataMember(Name = "series_id", EmitDefaultValue = false)]
        public string SeriesId { get; set; }

        [DataMember(Name = "recurrence_id", EmitDefaultValue = false)]
        public string RecurrenceId { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        [DataMember(Name = "recurrence", EmitDefaultValue = false)]
        public string[] Recurrence { get; set; }

        [DataMember(Name = "is_all_day", EmitDefaultValue = false)]
        public bool IsAllDay { get; set; }

        [DataMember(Name = "visibility", EmitDefaultValue = false)]
        public string Visibility { get; set; }

        [DataMember(Name = "invitees", EmitDefaultValue = false)]
        public Invitee[] Invitees { get; set; }

        [DataMember(Name = "confirmation", EmitDefaultValue = false)]
        public Confirmation Confirmation { get; set; }

        [DataMember(Name = "space", EmitDefaultValue = false)]
        public Space Space { get; set; }
    }
}
