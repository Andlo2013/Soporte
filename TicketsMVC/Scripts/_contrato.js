$(document).on("ready", _initialize);

function _initialize() {
    _formato();
    $("#fecInicia").datepicker();
    $("#fecTermina").datepicker();
    $("#Empresa_EmpRuc").on("change", _BuscaEmpresa);
    $("#PlanId").on("change", _BuscaPlan);
}

function _formato() {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '&#x3c;Ant',
        nextText: 'Sig&#x3e;',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio',
		'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun',
		'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Mi&eacute;rcoles', 'Jueves', 'Viernes', 'S&aacute;bado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mi&eacute;', 'Juv', 'Vie', 'S&aacute;b'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'S&aacute;'],
        weekHeader: 'Sm',
        dateFormat: 'yy/mm/dd',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        changeYear: true,
        yearRange:'-5:+5',
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
}

function _BuscaEmpresa() {
    var ruc = $("#Empresa_EmpRuc").val();
    if ($.trim(ruc)) {
        $("#Empresa_EmpNom").val("");
        _cq_FindEMPRUC(ruc, _empresaComplete);
    }
}

function _empresaComplete(resultado) {
    if (resultado != null) {
        if (resultado.pro_isComplete) {
            $("#Empresa_EmpNom").val(resultado.pro_data.EmpNom);
        }
    }
}

function _BuscaPlan() {
    var idPlan = $("#PlanId").val();
    $("#MinPlan").val(0);
    if (idPlan != null) {
        _cq_FindPlanID(idPlan, _planComplete);
    }
}

function _planComplete(resultado) {
    if (resultado != null) {
        if (resultado.pro_isComplete) {
            $("#MinPlan").val(resultado.pro_data.Minutos)
        }
    }
}

