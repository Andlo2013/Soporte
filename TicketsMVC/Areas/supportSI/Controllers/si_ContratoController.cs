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
using TicketsMVC.Areas.supportSI.Models;

namespace TicketsMVC.Areas.supportSI.Controllers
{
    public class si_ContratoController : Controller
    {
        private _Context db = new _Context();
        private _clsCommonQuery objComun = new _clsCommonQuery();
        private string m_errors = "";
        // GET: si_Contrato
        public ActionResult Index()
        {
            //int a=user.EmpresaID;
            var contrato = db.Contrato.Include(c => c.Empresa).Include(c => c.Plan)
                            .OrderByDescending(x=>x.id);
            return View(contrato.ToList());
        }

        // GET: si_Contrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // GET: si_Contrato/Create
        public ActionResult Create()
        {
            //ViewBag.EmpresaId = new SelectList(db.Empresa, "id", "EmpRuc");
            ViewBag.PlanId = new SelectList(db.Plan, "id", "Descripcion");
            return View();
        }

        // POST: si_Contrato/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Empresa,Plan")]Contrato contrato, string RUC)
        {
            try {
                m_errors = "";
                _RetrieveData(contrato, RUC, true);
                //configuraciones propias de un CONTRATO NUEVO
                contrato.EstReg = true;
                if (ModelState.IsValid && _Validate(contrato))
                {
                    db.Contrato.Add(contrato);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                m_errors = "Error al guardar el registro: " + ex.Message;
            }
            ViewBag.errores = m_errors;
            ViewBag.PlanId = new SelectList(db.Plan, "id", "Descripcion", contrato.PlanId);
            return View(contrato);
        }

        // GET: si_Contrato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.Empresa, "id", "EmpRuc", contrato.EmpresaId);
            ViewBag.PlanId = new SelectList(db.Plan, "id", "Descripcion", contrato.PlanId);
            return View(contrato);
        }

        // POST: si_Contrato/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Empresa,Plan")]Contrato contrato, string RUC)
        {
            try {
                m_errors = "";
                _RetrieveData(contrato, RUC, false);
                if (ModelState.IsValid && _Validate(contrato))
                {
                    db.Entry(contrato).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                m_errors = "Error al editar " + ex.Message;
            }

            ViewBag.errores = m_errors;
            ViewBag.PlanId = new SelectList(db.Plan, "id", "Descripcion", contrato.PlanId);
            return View(contrato);
        }

        // GET: si_Contrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = db.Contrato.Find(id);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: si_Contrato/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //////Contrato contrato = db.Contrato.Find(id);
            //////db.Contrato.Remove(contrato);
            //////db.SaveChanges();
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


        private void _RetrieveData(Contrato contrato, string RUC,bool isNew)
        {
            //El usuario ingresa el RUC y recupero la empresa 
            Empresa infoEmpresa = objComun._BuscaEmpresaRUC(RUC);
            //Esto para asegurarme que los valores del plan sean los configurados en la base de datos
            Plan infoPlan = objComun._BuscaPlan(contrato.PlanId);
            Contrato infoContrato = !isNew?objComun._BuscaContrato(contrato.id):null;
            int oldPlan = infoContrato != null ? infoContrato.PlanId : 0;
            if (infoEmpresa != null && infoPlan != null)
            {
                contrato.EmpresaId = infoEmpresa.id;
            }
            
            if (infoPlan != null && (isNew || (!isNew && oldPlan!=contrato.PlanId)))
            {
                contrato.PlanId = infoPlan.id;
                contrato.MinPlan = infoPlan.Minutos;
            }

        }

        private bool _Validate(Contrato contrato)
        {
            if (contrato.fecInicia > contrato.fecTermina)
            {
                DateTime aux = contrato.fecInicia;
                contrato.fecInicia = contrato.fecTermina;
                contrato.fecTermina = aux;
            }
            bool isValid = true;
           
            if (contrato.EmpresaId <= 0)
            {
                m_errors = "No se ha definido una Empresa";
                isValid = false;
            }
            if (contrato.PlanId <= 0)
            {
                m_errors = "No se ha definido el tipo de Plan";
                isValid = false;
            }
            if (contrato.fecTermina.Subtract(contrato.fecInicia).TotalDays < 30)
            {
                m_errors = "El periodo de vigencia del contrato debe ser al menos de 30 días";
                isValid = false;
            }
            return isValid;
        }

    }
}
