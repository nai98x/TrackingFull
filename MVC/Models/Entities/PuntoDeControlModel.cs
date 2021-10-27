using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.Entities
{
    public class PuntoDeControlModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }
        public int Posicion { get; set; }

        [Required(ErrorMessage = "Debes ingresar el tiempo estimado")]
        [Display(Name = "Tiempo Estimado")]
        public int TiempoEstimado { get; set; }
        public Agencia Agencia { get; set; }
        public Trayecto Trayecto { get; set; }
    }
}