using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Univesp.PI1.REST.DiarioEletronico.Except
{
    public class ExceptionDb
    {
        [Serializable]
        public class DbInicProcException : Exception
        {
            public DbInicProcException()
            {
            }

            public DbInicProcException(string message) : base(message)
            {
            }

            public DbInicProcException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected DbInicProcException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}