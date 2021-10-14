using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
