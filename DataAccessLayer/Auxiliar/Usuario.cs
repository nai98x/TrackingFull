using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public partial class Usuario
    {
        public Shared.Entities.Usuario ToEntity()
        {
            return new Shared.Entities.Usuario()
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Pass,
                Rol = this.Rol,
                Nombre = this.Nombre,
                Direccion = this.Direccion,
                Telefono = this.Telefono,
                TipoDocumento = this.TipoDocumento,
                NroDocumento = this.NroDocumento,
                CodigoExterno = this.CodigoExterno
            };
        }
    }
}
