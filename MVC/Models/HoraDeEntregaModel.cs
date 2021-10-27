using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class HoraDeEntregaModel
    {
        public int Id { get; set; }

        [Display(Name = "Hora de entrega")]
        public int HoraDeEntrega { get; set; }
    }
}