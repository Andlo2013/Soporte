using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsMVC.Areas.Cliente.Models
{
    public class cliTicketsDETModel
    {

        public int id { get; set; }

        public int TicketID { get; set; }

        public string TicketUUID { get; set; }

        public DateTime Fecha { get; set; }

        public int SecRespta { get; set; }

        public int Minutos { get; set; }

        public string Usuario { get; set; }

        public string Mensaje { get; set; }


    }
}