using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _Entidades
{
    public class Tecnico
    {
        public int id { get; set; }
        public string nombreTecnico { get; set; }

        public bool EstReg { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}