using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.Entities
{
    public class SistemaExternoModel
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debes ingresar un Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "el Email no es válido.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }
    }
}