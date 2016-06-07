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
using System.Data.Entity.Infrastructure;
using TicketsMVC.Clases;
using TicketsMVC.SentenciasSQL;
using System.Web.Routing;
using TicketsMVC.Areas._CommonModel;
using System.Threading.Tasks;
using System.IO;

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
                DbRawSqlQuery<TicketsModel> data = db.Database.SqlQuery<TicketsModel>
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
                string s_File1 = File1 != null && File1.FileName.Trim() != "" ? File1.FileName : "";
                string s_File2 = File2 != null && File2.FileName.Trim() != "" ? File2.FileName : "";
                string s_File3 = File3 != null && File3.FileName.Trim() != "" ? File3.FileName : "";
                if (_Validar(teamViewer, Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3" },
                        new object[] {User.Identity.Name,id,teamViewer,0,Mensaje,
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

        // POST => Guarda respuesta
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult answerJSON(string id, string teamViewer, string Mensaje, string observacion)
        {
            try
            {
                m_errors = "";
                if (_Validar(teamViewer, Mensaje))
                {
                    SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@userName","@TicketUUID","@TeamViewer","@Minutos","@Mensaje",
                                    "@Observaciones","@Archivo1","@Archivo2","@Archivo3" },
                        new object[] {User.Identity.Name,id,teamViewer,0,Mensaje,
                                observacion,"","",""});
                    DbRawSqlQuery<TicketsDETModel> data = db.Database.SqlQuery<TicketsDETModel>
                                                           (_SQLCliente.GuardaAnswer, parametros);

                    List<TicketsDETModel> record = data.ToList();
                    if(record!=null && record.Count == 1)
                    {
                        return Json(new { Result = "OK", Record = record[0] }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { Result = "ERROR", m_errors});
            }
            catch(Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // JSON => Detalle de respuestas
        public JsonResult answerDetails(string id,int jtStartIndex = 0, int jtPageSize = 0)
        {
            try
            {
                SqlParameter[] parametros = clsUtilidades._ParamsSQL(new string[] { "@UUID","@startIndex", "@perPage" },
                                                                    new object[] { id,jtStartIndex,jtPageSize });
                DbRawSqlQuery<TicketsDETModel> data = db.Database.SqlQuery<TicketsDETModel>
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
            TicketsDetalle ticketsDetalle = db.TicketDetalle.Find(id);
            if (ticketsDetalle == null)
            {
                return HttpNotFound();
            }
            return View(ticketsDetalle);
        }

        [HttpPost]
        public async Task<JsonResult> UploadFileN()
        {
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // get a stream
                        var stream = fileContent.InputStream;
                        // and optionally write the file to disk
                        var fileName = fileContent.FileName;
                        var path = Path.Combine(Server.MapPath("~/Upload"), fileName);
                        FileInfo f = new FileInfo(path);
                        using (var fileStream = f.Create())
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json("File uploaded successfully");
        }

        //[HttpPost]
        public ActionResult UploadFile(string id,string img)
        {
            HttpPostedFileBase myFile = Request.Files["files[]"];
            bool isUploaded = false;
            string message = "File upload failed";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Uploads/"+id);
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
                        SqlParameter[] parametros = clsUtilidades._ParamsSQL(
                        new string[] {"@id","@imageNumber","@FileName"},
                        new object[] {id,img,myFile.FileName});
                        db.Database.ExecuteSqlCommand(_SQLCliente.UploadImage, parametros);
                        isUploaded = true;
                        message = "File uploaded successfully!";
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }
            return Json(new { isUploaded = isUploaded, message = message }, "text/html");
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
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
