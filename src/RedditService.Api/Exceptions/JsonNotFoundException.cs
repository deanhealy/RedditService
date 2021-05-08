using System;
using System.Runtime.Serialization;

namespace RedditService.Api.Exceptions
{
    [Serializable]
    public class JsonNotFoundException : Exception
    {
        public JsonNotFoundException()
        {
        }

        protected JsonNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
