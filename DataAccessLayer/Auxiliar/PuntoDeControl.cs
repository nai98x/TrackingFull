using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public partial class PuntoDeControl
    {
        public Shared.Entities.PuntoDeControl ToEntity()
        {
            return new Shared.Entities.PuntoDeControl()
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Posicion = this.Posicion,
                TiempoEstimado = this.TiempoEstimado,
                Trayecto = this.Trayecto.ToEntity(),
                CodigoExterno = this.CodigoExterno
                //Agencia = this.Agencia.ToEntity()
            };
        }
    }
}
