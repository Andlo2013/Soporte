using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace _Entidades
{
    public class Ticket
    {

        public int id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El código de ticket es obligatorio")]
        [StringLength(13,ErrorMessage ="Longitud de código de ticket incorrecta")]
        //El código de ticket se forma por fecha + secuencial de 4 dígitos yyyyMMdd-0001
        public string Codigo { get; set; }


        [Display(Name = "Contrato")]
        [Required(ErrorMessage = "El tipo de contrato es obligatorio")]
        public int ContratoId { get; set; }

        [Display(Name = "User")]
        [Required(ErrorMessage = "El código de usuario es obligatorio")]
        public string AspNetUsersId { get; set; }

        [Display(Name = "FechaINI")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        public DateTime fechaINI { get; set; }

        [Display(Name = "Tecnico")]
        [Required(ErrorMessage = "El campo TÉCNICO es obligatorio")]
        public int TecnicoId { get; set; }

        [Display(Name = "Fecha FIN")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fechaFIN { get; set; }

        [Display(Name = "Prioridad")]
        [Required(ErrorMessage = "El campo PRIORIDAD es obligatorio")]
        public int PrioridadId { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo Estado de ticket es obligatorio")]
        public int TicketEstadoId { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "La categoría de ticket es obligatoria")]
        public int TicketCategoriaId { get; set; }

        [Display(Name ="Nro. Respuestas")]
        [Required(ErrorMessage ="El número de detalles es obligatorio")]
        public int NumDetalle { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage = "El estado del registro es obligatoria")]
        public bool EstReg { get; set; }

        //Relaciones
        public virtual TicketsCategoria TicketCategoria {get;set;}

        public virtual Contrato Contrato { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual ICollection<TicketsDetalle> TicketDetalle { get; set; }


    }
}