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
using System.Data.SqlClient;
using TicketsMVC.Clases;
using System.Data.Entity.Infrastructure;
using TicketsMVC.Areas._CommonModel;
using TicketsMVC.SentenciasSQL;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class si_TicketController : Controller
    {
        private _SQLServer objSQLServer = new _SQLServer();
        private _Context db = new _Context();
        private string m_errors = "";
        private string m_origin = "SUPPORT";

        // GET: supportSI/si_Tickets
        public ActionResult Index()
        {
            ViewBag.cmbEstado = db.Combo
                               .Where(x => x.Relacion == "ticket_estado")
                               .OrderBy(x => x.Valor).ToList();
            ViewBag.cmbPrioridad = db.Combo
                           .Where(x => x.Relacion == "ticket_prioridad")
                           .OrderBy(x => x.Valor).ToList();

            return View();
        }

        // JSON => Detalle de tickets
        [HttpPost]
        public JsonResult DetailsTicket(int ticketnumero = 0, string ticketempresa="",
                                        int ticketprioridad = 0,int ticketestado = 0,
                                        int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                            new string[] { "@startIndex", "@perPage", "@ticketNumero","@ticketEmpresa", "@ticketPrioridad", "@ticketEstado" },
                            new object[] { jtStartIndex, jtPageSize,ticketnumero,ticketempresa,ticketprioridad,ticketestado });
                DbRawSqlQuery<TicketsModel> data = db.Database.SqlQuery<TicketsModel>
                                                            (_SQLSupport.RecuperaTickets, parametros);
                return Json(new { Result = "OK", Records = data.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        //Métodos para las respuestas
        #region Answer

        // GET => Guarda respuesta
        public ActionResult Answer(string id)
        {
            return View();
        }

        // POST => Guarda respuesta
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult answerJSON(string id,  string Mensaje, string observacion,int Minutos)
        {
            try
            {
                m_errors = "";
                if (_Validar("123", Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3","@whoSend" },
                        new object[] {User.Identity.Name,id,"Tecnico",Minutos,Mensaje,
                                observacion,"","","",m_origin});
                    DbRawSqlQuery<TicketsDETModel> data = db.Database.SqlQuery<TicketsDETModel>
                                                           (_SQLCliente.GuardaAnswer, parametros);

                    List<TicketsDETModel> record = data.ToList();
                    if (record != null && record.Count == 1)
                    {
                        return Json(new { Result = "OK", Record = record[0] }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Result = "ERROR", m_errors });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // POST => Guarda respuesta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Answer(string id, string Minutos, string Mensaje, string observacion,
                                    HttpPostedFileBase File1, HttpPostedFileBase File2, HttpPostedFileBase File3)
        {
            try
            {
                m_errors = "";
                
                string s_File1 = File1 != null && File1.FileName.Trim() != "" ? File1.FileName : "";
                string s_File2 = File2 != null && File2.FileName.Trim() != "" ? File2.FileName : "";
                string s_File3 = File3 != null && File3.FileName.Trim() != "" ? File3.FileName : "";
                if (_Validar(Minutos, Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3" },
                        new object[] {User.Identity.Name,id,"Soporte",Minutos,Mensaje,
                                observacion,s_File1,s_File2,s_File3});
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
        public JsonResult answerDetails(string id, int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                SqlParameter[] parametros = clsUtilidades._ParamsSQL(new string[] { "@UUID","@startIndex", "@perPage","@whoAsked" },
                                                                    new object[] { id, jtStartIndex, jtPageSize, m_origin });
                DbRawSqlQuery<TicketsDETModel> data = db.Database.SqlQuery<TicketsDETModel>
                                                            (_SQLCliente.RecuperaTicketsDET, parametros);
                return Json(new { Result = "OK", Records = data.ToList() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET => Detalle respuesta
        public ActionResult AnswerShow(int id)
        {
            TicketsDetalle ticketsDetalle = db.TicketDetalle.Find(id);
            if (ticketsDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ticketsDetalle);
        }

        #endregion




        // GET: supportSI/si_Tickets/Details/5
        public ActionResult Details(int id)
        {
            Ticket ticket = db.Ticket.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: supportSI/si_Tickets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id != null && id.Trim() != "")
            {
                Ticket objTicket = _retrieveTicket(id);
                if (objTicket != null)
                {
                    ViewBag.cmbPrioridadId = new SelectList(db.Combo.Where(x => x.Relacion == "ticket_prioridad"), "Valor", "Descripcion", objTicket.cmbPrioridadId);
                    ViewBag.cmbEstadoId = new SelectList(db.Combo.Where(x => x.Relacion == "ticket_estado"), "Valor", "Descripcion", objTicket.cmbPrioridadId);
                    ViewBag.TecnicoId = new SelectList(db.Tecnico, "id", "nombreTecnico", objTicket.TecnicoId);
                    ViewBag.TicketCategoriaId = new SelectList(db.TicketCategoria, "id", "Categoria", objTicket.TicketCategoriaId);
                    return View(objTicket);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: supportSI/si_Tickets/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket ticket,string id)
        {
            Ticket objTicket = _retrieveTicket(id);
            objTicket.TecnicoId = ticket.TecnicoId;
            objTicket.cmbEstadoId = ticket.cmbEstadoId;
            objTicket.cmbPrioridadId = ticket.cmbPrioridadId;
            objTicket.TicketCategoriaId = ticket.TicketCategoriaId;
            db.Entry(objTicket).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        #region MétodosComplementarios

        private Ticket _retrieveTicket(string UUID)
        {
            List<Ticket> lsTicket = db.Ticket.Where(x => x.UUID == UUID).Take(1).ToList();
            if (lsTicket != null && lsTicket.Count==1)
            {
                return lsTicket[0];
            }
            return null;
        }

        private bool _Validar(string Minutos, string Mensaje)
        {
            bool isValid = true;
            if (Minutos.Trim() == "")
            {
                m_errors = "El campo 'Minutos' es obligatorio";
                isValid = false;
            }
            else if (Convert.ToInt32(Minutos) <= 0)
            {
                m_errors = "El campo 'Minutos' debe ser mayor a cero";
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
