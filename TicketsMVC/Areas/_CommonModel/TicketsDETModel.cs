using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsMVC.Areas._CommonModel
{
    public class TicketsDETModel
    {

        public int id { get; set; }

        public int TicketID { get; set; }

        public string TicketUUID { get; set; }

        public string TeamViewer { get; set; }

        public DateTime Fecha { get; set; }

        public int SecRespta { get; set; }

        public int Minutos { get; set; }

        public string Usuario { get; set; }

        public string Mensaje { get; set; }

        public string Observacion { get; set; }

        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }


    }
}