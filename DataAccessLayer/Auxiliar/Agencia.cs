using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public partial class Agencia
    {
        public Shared.Entities.Agencia ToEntity()
        {
            return new Shared.Entities.Agencia()
            {
                Id = this.Id,
                Nombre = this.Nombre,
                Direccion = this.Direccion,
                EntregaDomicilio = this.EntregaDomicilio,
                CodigoExterno = this.CodigoExterno
            };
        }
    }
}
