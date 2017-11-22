using System.Runtime.Serialization;

namespace RobinApi.Net.Model
{
	[DataContract]
	public class UserStatus
	{
		[DataMember(Name = "status", EmitDefaultValue = false)]
		public string Status { get; set; }

		[DataMember(Name = "device", EmitDefaultValue = false)]
		public Device Device { get; set; }
	}

}
