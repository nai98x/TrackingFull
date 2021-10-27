using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debes ingresar un Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "El Email no es válido.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes ingresar una dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Debes ingresar un teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Tipo Documento")]
        public TiposDocumento TipoDocumento { get; set; }

        [Required(ErrorMessage = "Debes ingresar un número de documento")]
        [Display(Name = "Número Documento")]
        public string NroDocumento { get; set; }
    }
}