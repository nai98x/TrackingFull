using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class PaqueteNoExistenteException : SystemException
    {
        public PaqueteNoExistenteException()
            : base("El paquete no está registrado en el sistema")
        {
        }
    }
}