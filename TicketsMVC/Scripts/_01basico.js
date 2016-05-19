function _AjaxGET(url, completeFunction) {
    var token = $('[name=__RequestVerificationToken]').val();
    $.ajax({
        type: "GET",
        url: url,
        dataType: "JSON",
        data: { __RequestVerificationToken: token},
        //data: "{}",
        success: function (response) {
            completeFunction(response);   
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("ERROR EN PETICIÓN AJAX");
            return null;
        }
    });
}