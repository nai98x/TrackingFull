using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class UsuarioNoExistenteException : SystemException
    {
        public UsuarioNoExistenteException()
            : base("El usuario no está registrado en el sistema")
        {
        }
    }
}
