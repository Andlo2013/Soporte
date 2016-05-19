using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTickets;
using _Entidades;

namespace TicketsMVC.Areas.supportSI.Models
{
    public class _clsCommonQuery
    {

        // GET: _common
        public Empresa _BuscaEmpresaRUC(string RUC)
        {
            using (var db = new _Context())
            {
                db.Configuration.LazyLoadingEnabled = false;
                Empresa registro = null;
                if (RUC != null && RUC.Trim() != "")
                {
                    registro = db.Empresa.Where(e => e.EmpRuc == RUC).FirstOrDefault();
                }
                return registro;
            }
        }

        public Plan _BuscaPlan(int idPlan)
        {
            using (var db = new _Context())
            {
                db.Configuration.LazyLoadingEnabled = false;
                Plan registro = null;
                registro = db.Plan.Where(e => e.id == idPlan).FirstOrDefault();
                return registro;
            }
        }

        public Contrato _BuscaContrato(int idContrato)
        {
            using (var db = new _Context())
            {
                Contrato registro = null;
                registro = db.Contrato.Where(e => e.id == idContrato).FirstOrDefault();
                return registro;
            }
        }
    }
}