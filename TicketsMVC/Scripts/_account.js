$(document).on("ready", _initialize);

function _initialize() {
    $("#RUC").on("change", _BuscaEmpresa);
}


function _BuscaEmpresa() {
    var ruc = $("#RUC").val();
    $("#Empresa").val("");
    if ($.trim(ruc)) {
        _cq_FindEMPRUC(ruc, _empresaComplete);
    }
}

function _empresaComplete(resultado) {
    if (resultado != null) {
        if (resultado.pro_isComplete) {
            $("#Empresa").val(resultado.pro_data.EmpNom);
        }
    }
}