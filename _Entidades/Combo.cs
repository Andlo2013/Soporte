using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Entidades
{
    public class Combo
    {

        public int id { get; set; }

        [StringLength(50, ErrorMessage = "Relación máximo 50 caracteres")]
        public string Relacion { get; set; }

        public int Valor { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Descripción máximo 50 caracteres")]
        public string Descripcion { get; set; }

    }
}
