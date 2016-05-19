using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataTickets;
using _Entidades;
using TicketsMVC.Areas.supportSI.Models;

namespace TicketsMVC.Controllers
{
    public class ajaxController : Controller
    {
        private Models.ResponseModel rm = null;
        private _Context db = new _Context();
        private _clsCommonQuery objComun = new _clsCommonQuery();
        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult _BuscaEMP(string RUC)
        {
            rm = new Models.ResponseModel();
            Empresa registro = objComun._BuscaEmpresaRUC(RUC);
            if (registro!=null)
            {
                rm.pro_isComplete = true;
                rm.pro_data = registro;
            }
            return Json(rm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _BuscaPlan(int idPlan)
        {
            rm = new Models.ResponseModel();
            Plan registro = null;
            registro = objComun._BuscaPlan(idPlan);
            if (registro != null)
            {
                rm.pro_isComplete = true;
                rm.pro_data = registro;
            }
            return Json(rm, JsonRequestBehavior.AllowGet);
        }
    }
}