using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class PuntoDeControl
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        public int TiempoEstimado { get; set; }
        public Agencia Agencia { get; set; }
        public Trayecto Trayecto { get; set; }
        public string CodigoExterno { get; set; }
    }
}
