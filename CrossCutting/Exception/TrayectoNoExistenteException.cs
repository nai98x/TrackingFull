using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class TrayectoNoExistenteException : SystemException
    {
        public TrayectoNoExistenteException()
            : base("El trayecto no está registrado en el sistema")
        {
        }
    }
}