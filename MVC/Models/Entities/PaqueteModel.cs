using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.Entities
{
    public class PaqueteModel
    {
        public int Id { get; set; }

        public string Estado { get; set; }

        [Required(ErrorMessage = "Debes ingresar una descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debes ingresar una fecha de entrega")]
        [Display(Name = "Fecha de entrega")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [Display(Name = "Codigo de verificación")]
        public string Codigo { get; set; }

        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "Debes elegir un trayecto")]
        public Trayecto Trayecto { get; set; }

        [Required(ErrorMessage = "Debes elegir un remitente")]
        public Usuario Remitente { get; set; }

        [Required(ErrorMessage = "Debes elegir un destinatario")]
        public Usuario Destinatario { get; set; }

        public bool PuedeCambiarHoraDeEntrega { get; set; }

        public string HoraDeEntrega { get; set; }
    }
}