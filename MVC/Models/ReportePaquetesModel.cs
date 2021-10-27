using Shared.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ReportePaquetesModel
    {
        [Display(Name = "Fecha Desde")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; } = DateTime.Now.AddMonths(-1);

        [Display(Name = "Fecha Hasta")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)] 
        public DateTime FechaHasta { get; set; } = DateTime.Now;
        public string Estado { get; set; }
        public Usuario Remitente { get; set; }
        public Usuario Destinatario { get; set; }
    }
}