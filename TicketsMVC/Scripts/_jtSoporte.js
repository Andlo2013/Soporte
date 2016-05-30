$(document).on("ready", _initialize);

function _initialize() {
    _table();
}

function _table() {
    $('#SoporteTable').jtable({
        title: 'Tickets ingresados',
        sorting: true,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/Cliente/soporte/DetailsTicket'
        },

        //CodTicket	Categoria	Usuario	Fecha	Tecnico	Prioridad	Estado

        fields: {

            //CHILD TABLE 
            Detalle: {
                title: 'Ver',
                width: '5%',
                sorting: false,
                display: function (studentData) {
                    //Create an image that will be used to open child table
                    var $img = $('<img class="icon" src="/images/detail.png" title="Ver respuestas"/>');
                    //Open child table when user clicks the image
                    $img.click(function () {
                        $('#SoporteTable').jtable('openChildTable',
                                $img.closest('tr'),
                                {
                                    title: 'Respuestas del ticket',
                                    actions: {
                                        listAction: '/Cliente/Soporte/answerDetails/' + studentData.record.UUID
                                    },
                                    fields: {
                                        SecRespta: {
                                            title: 'Nro',
                                            width: '5%',
                                            display: function (data) {
                                                var linkDET = data.record.SecRespta;
                                                var identifier = data.record.id;
                                                var $link = $('<a href="/Cliente/Soporte/AnswerShow/' + identifier + '">' + linkDET + '</a>');
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
                                            width: '60%'
                                        },
                                        Minutos: {
                                            title: 'Minutos',
                                            width: '10%'
                                        }
                                    }
                                }, function (data) { //opened handler
                                    data.childTable.jtable('load');
                                });
                    });
                    //Return image to show on the person row
                    return $img;
                }
            },

            id: {
                title: 'Ticket',
                width: '10%'
            },

            Categoria: {
                title: 'Categoría',
                width: '15%'
            },

            Usuario: {
                title: 'Usuario',
                width: '23%'
            },

            Fecha: {
                title: 'Fecha',
                width: '10%',
                type: 'date'
            },

            Tecnico: {
                title: 'Técnico',
                width: '20%'
            },

            Prioridad: {
                title: 'Prioridad',
                width: '12%'
            },

            Estado: {
                title: 'Estado',
                width: '10%'
            },

            Responder: {
                title: 'Pre',
                width: '5%',
                sorting: false,
                display: function (studentData) {
                    //Create an image that will be used to open child table
                    var $img = $('<a href="/Cliente/Soporte/Answer/'+studentData.record.UUID+'"><img class="icon" src="/images/pregunta.png" title="Preguntar"/></a>');
                    //Return image to show on the person row
                    return $img;
                }
            }

        }
    });
    $('#SoporteTable').jtable('load');
}