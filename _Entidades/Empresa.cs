using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _Entidades
{
    public class Empresa
    {
        public int id { get; set; }

        [Display(Name = "RUC")]
        [Required(ErrorMessage = "RUC Empresa es un campo obligatorio")]
        [StringLength(13, ErrorMessage = "El RUC debe tener 13 dígitos", MinimumLength = 13)]
        public string EmpRuc { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage = "Nombre Empresa es un campo obligatorio")]
        [StringLength(100, ErrorMessage = "Nombre Empresa debe tener máximo 100 caracteres")]
        public string EmpNom { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "Dirección Empresa es un campo obligatorio")]
        [StringLength(150, ErrorMessage = "Dirección Empresa debe tener máximo 150 caracteres")]
        public string Direccion { get; set; }


        [Display(Name = "Teléfono")]
        [StringLength(50, ErrorMessage = "Teléfono Empresa debe tener máximo 50 caracteres")]
        public string Telefono { get; set; }

        [Display(Name = "Activo")]
        [Required(ErrorMessage = "Estado Empresa es un campo obligatorio")]
        
        public bool EstReg { get; set; }
        
        

        public virtual ICollection<Contrato> Contratos { get; set; }


    }
}