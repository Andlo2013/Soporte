$(document).on("ready", _initialize);

function _initialize() {
    _table();
}

function _table() {
    $('#SoporteTable').jtable({
        title: 'Tickets ingresados',
        sorting: false,
        paging: true, //Enable paging
        pageSize: 15,
        actions: {
            listAction: '/Cliente/soporte/DetailsTicket'
        },

        //CodTicket	Categoria	Usuario	Fecha	Tecnico	Prioridad	Estado

        fields: {

           

            id: {
                title: 'Ticket',
                width: '7%'
            },

            Fecha: {
                title: 'Fecha',
                width: '10%',
                type: 'date'
            },

            Pregunta: {
                title: 'Pregunta',
                width: '35%',
                sorting: false
            },

            Prioridad: {
                title: 'Prioridad',
                width: '12%'
            },

            Estado: {
                title: 'Estado',
                width: '8%'
            },

            Categoria: {
                title: 'Categoría',
                width: '15%'
            },

            Tiempo: {
                title: 'Tiempo',
                width: '5%'
            },
            //CHILD TABLE 
            Detalle: {
                title: 'Ver',
                width: '4%',
                sorting: false,
                display: function (studentData) {
                    //Create an image that will be used to open child table
                    //var $img = $('<img class="icon" src="/images/detail.png" title="Ver respuestas"/>');
                    var $img = $('<label class="btn btn-info" title="Ver respuestas">Ver</label>');
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
                                            width: '50%'
                                        },
                                        File1: {
                                            title: 'File 1',
                                            width: '10%'
                                        },
                                        File2: {
                                            title: 'File 2',
                                            width: '10%'
                                        },
                                        File3: {
                                            title: 'File 3',
                                            width: '10%'
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

            Responder: {
                title: 'Pre',
                width: '4%',
                sorting: false,
                display: function (studentData) {
                    //Create an image that will be used to open child table
                    var $img = $('<a class="btn btn-success" href="/Cliente/Soporte/Answer/' + studentData.record.UUID + '">SMS</a>');
                    //Return image to show on the person row
                    return $img;
                }
            }
        }
    });
    $('#SoporteTable').jtable('load');
}