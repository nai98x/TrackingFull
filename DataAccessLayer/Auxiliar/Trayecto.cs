using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public partial class Trayecto
    {
        public Shared.Entities.Trayecto ToEntity()
        {
            return new Shared.Entities.Trayecto()
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Destino = this.Destino.ToEntity(),
                Origen = this.Origen.ToEntity(),
                CodigoExterno = this.CodigoExterno
            };
        }
    }
}