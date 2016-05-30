using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTickets;
using _Entidades;
using Microsoft.AspNet.Identity;
using TicketsMVC.Models;

namespace TicketsMVC.Areas.supportSI.Models
{
    public class _clsCommonQuery
    {

        public Empresa _BuscaEmpresaID(int idEmpresa)
        {
            using (var db = new _Context())
            {
                db.Configuration.LazyLoadingEnabled = false;
                Empresa registro = null;
                if (idEmpresa > 0)
                {
                    registro = db.Empresa.Where(e => e.id == idEmpresa).FirstOrDefault();
                }
                return registro;
            }
        }

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

        public Contrato _BuscaContratoEMP(int idEmpresa)
        {
            using (var db = new _Context())
            {
                Contrato registro = null;
                registro = db.Contrato
                            .Where(c => c.EmpresaId == idEmpresa)
                            .Where(c=>c.EstReg==true)
                            .Where(c=>c.fecTermina>=DateTime.Now)
                            .OrderByDescending(c=>c.id)
                            .FirstOrDefault();
                return registro;
            }
        }

        public _infoUser _BuscaUser(string userName)
        {
            _infoUser user=null;
            using (var db = new ApplicationDbContext())
            {
                ApplicationUser reg = db.Users.Where(x => x.UserName == userName).FirstOrDefault();
                if (reg != null)
                {
                    user = new _infoUser(reg.Email, reg.EmpresaID, reg.UserName, reg.Id);
                }
            }
            return user;
        }
    }
}