$(document).on("ready", _initialize);

function _initialize() {
    //var uuid = _getUrlParameter('id');
    var divURL = window.location.href.split('/');
    var uuid = divURL[divURL.length-1];
    _table(uuid);
}

function _table(uuid) {
    $('#AnswerTable').jtable({
        title: 'Respuestas tickets',
        sorting: true,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/Cliente/Soporte/answerDetails/' + uuid
        },

        //CodTicket	Categoria	Usuario	Fecha	Tecnico	Prioridad	Estado

        fields: {
            TicketID: {
                title: 'Ticket',
                width: '15%'
            },
            SecRespta: {
                title: 'Respuesta',
                width: '10%',
                display: function (data) {
                    var linkDET = data.record.SecRespta;
                    var $link = $('<a href="/Cliente/Soporte/Details/' + linkDET + '">' + linkDET + '</a>');
                    $link.click(function () { });
                    return $link;
                }
            },
            Fecha: {
                title: 'Fecha',
                width: '10%',
                type: 'date'
            },
            Usuario: {
                title: 'Usuario',
                width: '15%'
            },
            Mensaje: {
                title: 'Mensaje',
                width: '55%'
            },
            Minutos: {
                title: 'Minutos',
                width: '10%'
            },
            Detalle: {
                title: 'Det',
                width: '10%',
                display: function (data) {
                    var linkDET = data.record.id;
                    var $link = $('<label><a href="/Cliente/Soporte/AnswerShow/' + linkDET + '">Detalle</a></label>');
                    $link.click(function () { });
                    return $link;
                }
            }
        }
    });
    $('#AnswerTable').jtable('load');
}

