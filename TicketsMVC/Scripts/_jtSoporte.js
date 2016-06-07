$(document).on("ready", _initializeJTSoporte);
//mensajes en español
var spanishMessages = {
    serverCommunicationError: 'A ocurrido un error al intentar comunicarse con el servidor.',
    loadingMessage: 'Recuperando registros...',
    noDataAvailable: 'No se encontraron registros!',
    addNewRecord: 'Nuevo Registro',
    editRecord: 'Edit Record',
    areYouSure: 'Are you sure?',
    deleteConfirmation: 'This record will be deleted. Are you sure?',
    save: 'Guardar',
    saving: 'Guardando',
    cancel: 'Cancelar',
    deleteText: 'Eliminar',
    deleting: 'Eliminando',
    error: 'Error',
    close: 'Cerrar',
    cannotLoadOptionsFor: 'No se puede cargar las opciones para el campo {0}',
    pagingInfo: 'Showing {0}-{1} of {2}',
    pageSizeChangeLabel: 'Row count',
    gotoPageLabel: 'Ir a página',
    canNotDeletedRecords: 'No se puede eliminar {0} de {1} registros!',
    deleteProggress: 'Eliminando {0} de {1} registros, processando...'
};

//Pone en español jtable
$.extend(true, $.hik.jtable.prototype.options.messages, spanishMessages);

//Inicializa este script
function _initializeJTSoporte() {
    _masterTable();
}

//Crea la tabla principal
function _masterTable() {
    $('#SoporteTable').jtable({
        messages: spanishMessages,
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
                messages:{addNewRecord: 'Nuevo Ticket'},
                display:_detailTable
            }
        }
    });
    $('#SoporteTable').jtable('load');
}

//Crea la tabla detalle
function _detailTable (mainRecord) {
    //Create an image that will be used to open child table
    //var $img = $('<img class="icon" src="/images/detail.png" title="Ver respuestas"/>');
    var $img = $('<label id="' + mainRecord.record.UUID + '"class="btn btn-info" title="Ver respuestas">Ver</label>');
    //Open child table when user clicks the image
    $img.click(function () {
        $('#SoporteTable').jtable('openChildTable',
                $img.closest('tr'),
                {
                    title: 'Respuestas del ticket',
                    paging: true, //Enable paging
                    pageSize: 10,
                    actions: {
                        listAction: '/Cliente/Soporte/answerDetails/' + mainRecord.record.UUID,
                        createAction: '/Cliente/Soporte/answerJSON/' + mainRecord.record.UUID,
                    },
                    fields: {
                        SecRespta: {
                            title: 'Nro',
                            width: '5%',
                            create: false,
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
                            type: 'date',
                            create: false
                        },
                        Usuario: {
                            title: 'Usuario',
                            width: '15%',
                            create: false
                        },
                        Mensaje: {
                            title: 'Mensaje',
                            width: '50%'
                        },
                        TeamViewer: {
                            title: 'TeamViewer',
                            width: '0%',
                            create: true,
                            visibility:'hidden'
                        },
                        Observacion: {
                            title: 'Observacion',
                            width: '0%',
                            create: true,
                            visibility: 'hidden'
                        },
                        File1: {
                            title: 'File 1',
                            width: '10%',
                            create: false,
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 1);
                                return $link;
                            }
                        },
                        File2: {
                            title: 'File 2',
                            width: '10%',
                            create: false,
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 2);
                                return $link;
                            }
                        },
                        File3: {
                            title: 'File 3',
                            width: '10%',
                            create: false,
                            display: function (data) {
                                var $link = _linkUpload(data.record, mainRecord.record.UUID, 3);
                                return $link;
                            }
                        },
                        Minutos: {
                            title: 'Minutos',
                            width: '10%',
                            create: false
                        }
                    }
                }, function (data) { //opened handler
                    data.childTable.jtable('load');
                });
    });
    //Return image to show on the person row
    return $img;
}

//Crea el link del 'FILEUPLOAD' para cada registro en cada columna.
function _linkUpload(record,ticketUUID,numeroImg) {
   
    //setencia base para crear la interfaz de fileupload en tiempo de ejecución
    var upload = '<div>' +
        '<span class="btn btn-info fileinput-button">' +
        '<i class="glyphicon glyphicon-plus"></i>' +
        '<span id="nombre">Adjuntar</span>' +
        '<input id="fileupload" data-ticket="ticket0" data-numeroImg="img0" data-name="name0" type="file" class="fileUpload" name="files[]" multiple="">' +
        '</span></br>' +
        '<div id="progress" class="barra">' +
        '<div class="progress-bar progress-bar-primary"></div>' +
        '</div>';
    //recuperamos propiedades para no utilizar nombres muy extensos
    var $link;
    var identifier = record.id;
    var fileName = "";
    switch (numeroImg) {
        case 1:
            fileName = record.File1;
            break;
        case 2:
            fileName = record.File2;
            break;
        case 3:
            fileName = record.File3;
            break;
    }
    
    //si tiene un nombre de archivo crea un link de descarga
    if (fileName != null && fileName != "File") {
        $link = $('<a href="/Uploads/' + identifier + '/' + fileName + '" download>' + fileName + '</a>');
    }
        //si no tiene un link de archivo crea el fileuploader identifcando cada controlador por su nombre
    else {
        $link = upload.replace("progress", "progress" + identifier+numeroImg);
        $link = $link.replace("fileupload", "fileupload" + identifier + numeroImg);
        $link = $link.replace("name0", identifier);
        $link = $link.replace("ticket0", ticketUUID);
        $link = $link.replace("img0", numeroImg);
    }
    return $link;
}