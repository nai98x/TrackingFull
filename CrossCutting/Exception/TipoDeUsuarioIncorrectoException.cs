using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class TipoDeUsuarioIncorrectoException : SystemException
    {
        public TipoDeUsuarioIncorrectoException()
            : base("El tipo de usuario que solicita no existe")
        {
        }
    }
}
