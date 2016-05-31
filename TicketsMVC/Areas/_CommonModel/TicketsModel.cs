using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsMVC.Clases;

namespace TicketsMVC.Areas._CommonModel
{
    public class TicketsModel
    {
        
        public int id { get; set; }

        public DateTime Fecha { get; set; }
       
        public int Sec { get; set; }

        public string Categoria { get; set; }

        public string Usuario { get; set; }

        public string Tecnico { get; set; }

        public string Pregunta { get; set; }

        public string Prioridad { get; set; }

        public int Tiempo { get; set; }

        public int PrioridadID { get; set; }

        public int EstadoID { get; set; }

        public string Estado { get; set; }

        public string UUID { get; set; }

        //Solo Renán
        public string Empresa { get; set; }

        public string tipoPlan { get; set; }

    }
}