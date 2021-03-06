using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AdministradorModel
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debes ingresar un Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "el Email no es válido.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debes ingresar una contraseña")]
        public string Password { get; set; }
    }
}