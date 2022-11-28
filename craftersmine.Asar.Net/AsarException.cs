using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace craftersmine.Asar.Net
{
    [Serializable]
    public class AsarException : Exception
    {
        public AsarException()
        {
        }

        public AsarException(string message) : base(message)
        {
        }

        public AsarException(string message, Exception inner) : base(message, inner)
        {
        }

        protected AsarException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
