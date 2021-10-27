using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Shared.Entities;

namespace MVC.Models
{
    public class NuevoClienteModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debes ingresar un Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "El Email no es válido.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debes ingresar una confirmación de contraseña")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes ingresar una dirección")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Debes ingresar un teléfono")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Display(Name = "Tipo de Documento")]
        public TiposDocumento TipoDocumento { get; set; }

        [Required(ErrorMessage = "Debes ingresar un número de documento")]
        [Display(Name = "Número de Documento")]
        public string NroDocumento { get; set; }
    }
}