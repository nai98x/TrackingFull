using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Trayecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Agencia Origen { get; set; }
        public Agencia Destino { get; set; }
        public string CodigoExterno { get; set; }
    }
}
