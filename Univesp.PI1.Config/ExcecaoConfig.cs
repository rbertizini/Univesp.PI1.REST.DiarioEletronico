using System;
using System.Runtime.Serialization;

namespace Univesp.PI1.Config
{
    public class ExcecaoConfig
    {
        [Serializable]
        public class ParamNaoLocalizadoException : Exception
        {
            public ParamNaoLocalizadoException()
            {
            }

            public ParamNaoLocalizadoException(string message) : base(message)
            {
            }

            public ParamNaoLocalizadoException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected ParamNaoLocalizadoException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
