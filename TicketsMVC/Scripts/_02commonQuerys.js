function _cq_FindEMPRUC(rucEmpresa,_successFunction) {
    if ($.trim(rucEmpresa)) {
        _AjaxGET("/ajax/_BuscaEMP?RUC=" + rucEmpresa, _successFunction);
    }
}

function _cq_FindPlanID(idPlan, _successFunction) {
    if (idPlan != null) {
        _AjaxGET("/ajax/_BuscaPlan?idPlan=" + idPlan, _successFunction);
    }
}