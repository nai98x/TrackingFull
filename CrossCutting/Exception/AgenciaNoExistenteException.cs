using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class AgenciaNoExistenteException : SystemException
    {
        public AgenciaNoExistenteException()
            : base("La agencia no está registrada en el sistema")
        {
        }
    }
}
