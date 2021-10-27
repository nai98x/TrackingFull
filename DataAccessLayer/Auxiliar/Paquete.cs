using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;

namespace DataAccessLayer.Model
{
    public partial class Paquete
    {
        public Shared.Entities.Paquete ToEntity()
        {
            return new Shared.Entities.Paquete()
            {
                Id = this.Id,
                Descripcion = this.Descripcion,
                FechaEntrega = (DateTime)this.FechaEntrega,
                Codigo = this.Codigo,
                CodigoExterno = this.CodigoExterno,
                Entregado = (bool)this.Entregado,
                FechaIngreso = (DateTime)this.FechaIngreso,
                Trayecto = this.Trayecto.ToEntity(),
                Remitente = this.Remitente.ToEntity(),
                Destinatario = this.Destinatario.ToEntity(),
                PDCActual = this.PDC_Actual.ToEntity()
            };
        }
    }
}
