using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Paquete
    {
        public int  Id{ get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Codigo { get; set; }
        public string CodigoExterno { get; set; }
        public bool Entregado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Trayecto Trayecto { get; set; }
        public Usuario Remitente { get; set; }
        public Usuario Destinatario { get; set; }
        public PuntoDeControl PDCActual { get; set; }
        public int HoraDeEntrega { get; set; }
    }
}
