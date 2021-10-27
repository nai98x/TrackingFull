using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Entities
{
    public class AgenciaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debes ingresar una dirección")]
        public string Direccion { get; set; }

        [Display(Name = "Entrega a Domicilio")]
        public bool  EntregaDomicilio{ get; set; }
}
}