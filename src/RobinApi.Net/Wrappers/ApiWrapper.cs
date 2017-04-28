using System.Runtime.Serialization;

namespace RobinApi.Net.Wrappers
{
    [DataContract]
    public class ApiWrapper<T>
    {
        [DataMember(Name = "meta", EmitDefaultValue = false)]
        public MetaWrapper Meta { get; set; }

        [DataMember(Name = "data", EmitDefaultValue = false)]
        public T Data { get; set; }

        [DataMember(Name = "paging", EmitDefaultValue = false)]
        public PagingWrapper Paging { get; set; }

    }
}
