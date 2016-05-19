using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _Entidades
{
    public class Plan
    {
        public int id { get; set; }

        [Display(Name = "Plan")]
        [Required(ErrorMessage = "Descripción Plan es un campo obligatorio")]
        [StringLength(50, ErrorMessage = "Descripción Plan debe tener máximo 50 caracteres")]
        public string Descripcion { get; set; }

        [Display(Name = "Minutos")]
        [Required(ErrorMessage ="Minutos Plan es un campo obligatorio")]
        [Range(60,120000,ErrorMessage ="Minutos Plan entre 60 y 120000")]
        public int Minutos { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage ="Estado Plan es un campo obligatorio")]
        public bool EstReg { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }


    }
}