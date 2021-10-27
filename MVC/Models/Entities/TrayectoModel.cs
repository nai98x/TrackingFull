using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models.Entities
{
    public class TrayectoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debes ingresar un nombre")]
        public string Nombre { get; set; }

        public Agencia Origen { get; set; }

        public Agencia Destino { get; set; }
    }
}