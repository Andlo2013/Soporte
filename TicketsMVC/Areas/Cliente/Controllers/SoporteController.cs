using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataTickets;
using _Entidades;
using TicketsMVC.Areas.Cliente.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using TicketsMVC.Clases;
using TicketsMVC.SentenciasSQL;
using System.Web.Routing;

namespace TicketsMVC.Areas.Cliente.Controllers
{
    public class SoporteController : Controller
    {
        
        private _SQLServer objSQLServer = new _SQLServer();
        private _Context db = new _Context();
        private string m_errors = "";

        //Métodos para los tickets
        #region Tickets

        // GET: Index ticket
        public ActionResult Index()
        {
            try {
                _infoContrato();
                return View();
            }
            catch
            {
                return null;
            }
        }

        // GET => Guarda ticket
        public ActionResult Create()
        {
            return View();
        }

        // POST => Guarda ticket
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string teamViewer, string Mensaje, string observacion,
                                    HttpPostedFileBase File1, HttpPostedFileBase File2, HttpPostedFileBase File3)
        {
            try
            {
                m_errors = "";
                if (_Validar(teamViewer, Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TeamViewer","@Mensaje","@Observaciones",
                                    "@Archivo1","@Archivo2","@Archivo3" },
                        new object[] {User.Identity.Name,teamViewer,Mensaje,
                                observacion,File1.FileName,File2.FileName,File3.FileName});
                    db.Database.ExecuteSqlCommand(_SQLCliente.GuardaTickets, parametros);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                m_errors = "Error al intentar guarda los datos";
            }
            ViewBag.errores = m_errors;
            return View();
        }

        // GET => Detalle de tickets(Reemplazado por jtable)
        public ActionResult Details(string id)
        {
            ViewBag.TicketUUID = id;
            return View();
        }

        // JSON => Detalle de tickets
        public JsonResult DetailsTicket(int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                //jtStartIndex
                SqlParameter[] parametros = clsUtilidades._ParamsSQL(new string[] { "@userName", "@startIndex","@perPage" },
                                                                    new object[] { User.Identity.Name, jtStartIndex, jtPageSize });
                DbRawSqlQuery<cliTicketsModel> data = db.Database.SqlQuery<cliTicketsModel>
                                                            (_SQLCliente.RecuperaTickets, parametros);
                return Json(new { Result = "OK", Records = data.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        #endregion

        //Métodos para las respuestas
        #region Answer

        // GET => Guarda respuesta
        public ActionResult Answer(string id)
        {
            return View();
        }

        // POST => Guarda respuesta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(string id, string teamViewer, string Mensaje, string observacion,
                                    HttpPostedFileBase File1, HttpPostedFileBase File2, HttpPostedFileBase File3)
        {
            try
            {
                m_errors = "";
                if (_Validar(teamViewer, Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TicketUUID","@TeamViewer","@Mensaje","@Observaciones",
                                    "@Archivo1","@Archivo2","@Archivo3" },
                        new object[] {User.Identity.Name,id,teamViewer,Mensaje,
                                observacion,File1.FileName,File2.FileName,File3.FileName});
                    db.Database.ExecuteSqlCommand(_SQLCliente.GuardaAnswer, parametros);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                m_errors = "Error al intentar guarda los datos";
            }
            ViewBag.errores = m_errors;
            return View();
        }

        // JSON => Detalle de respuestas
        public JsonResult answerDetails(string id)
        {
            try
            {
                SqlParameter[] parametros = clsUtilidades._ParamsSQL(new string[] { "@UUID" },
                                                                    new object[] { id });
                DbRawSqlQuery<cliTicketsDETModel> data = db.Database.SqlQuery<cliTicketsDETModel>
                                                            (_SQLCliente.RecuperaTicketsDET, parametros);
                return Json(new { Result = "OK", Records = data.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET => Detalle respuesta
        public ActionResult AnswerShow(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketsDetalle ticketsDetalle = db.TicketDetalle.Find(id);
            if (ticketsDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ticketsDetalle);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MétodosComplementarios

        private void _infoContrato()
        {
            DataTable dt_infoContrato=objSQLServer._CargaDataTable("ticket_infoPlan",
                        new string[] { "@userName" }, new object[] { User.Identity.Name });
            ViewBag.Empresa = dt_infoContrato.Rows[0]["Empresa"].ToString().Trim();
            ViewBag.Plan = dt_infoContrato.Rows[0]["Plan"].ToString().Trim();
            ViewBag.Inicia = dt_infoContrato.Rows[0]["Inicia"].ToString().Trim();
            ViewBag.Termina = dt_infoContrato.Rows[0]["Termina"].ToString().Trim();
            ViewBag.Minutos = dt_infoContrato.Rows[0]["Minutos"].ToString().Trim();
            ViewBag.MinutosUTI = dt_infoContrato.Rows[0]["MinutosUTI"].ToString().Trim();
        }

        private bool _Validar(string teamViewer,string Mensaje)
        {
            bool isValid = true;
            if (teamViewer.Trim() == "")
            {
                m_errors = "El campo ID TeamViewer es obligatorio";
                isValid = false;
            }
            else if (Mensaje.Trim() == "")
            {
                m_errors = "El campo mensaje es obligatorio";
                isValid = false;
            }
            return isValid;
        }

        #endregion

    }
}
