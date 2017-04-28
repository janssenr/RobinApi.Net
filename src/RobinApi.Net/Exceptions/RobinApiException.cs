using System;
using RobinApi.Net.Wrappers;

namespace RobinApi.Net.Exceptions
{
    public sealed class RobinApiException : Exception
    {
        public RobinApiException(MetaWrapper meta) : base(meta.Message)
        {
            foreach (var mi in meta.MoreInfo)
            {
                Data.Add(mi.Key, mi.Value);
            }
        }
    }
}
