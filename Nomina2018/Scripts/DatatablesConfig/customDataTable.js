var bindDataTable = function (idElement, document, urlServerSide,
		preparetedVMDataTableFunction, numUnorderedColumn, arrayColumns,
		posDrawDataTableFunction, searching) {
    var search = false;
    if (searching !== undefined) {
        search = searching;
    }
    var progressbar = '<div class="progress">'
  + '<div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">'
    '<span class="">Processing...</span>'
  +'</div></div>';
    return $(idElement)
			.DataTable(
					{
					    "processing": true,
					    "searching": search,
					    autoWidth: false,
					    lengthMenu: [[5, 10, 20, 30, 50],
								[5, 10, 20, 30, 50]],
					    "language": {
					        "sProcessing": progressbar,
					        "sLengthMenu": "Mostrar _MENU_ registros",
					        "sZeroRecords": "No se encontraron resultados",
					        "sEmptyTable": "Ningún dato disponible en esta tabla",
					        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
					        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
					        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
					        "sInfoPostFix": "",
					        "sSearch": "Buscar:",
					        "sUrl": "",
					        "sInfoThousands": ",",
					        "sLoadingRecords": "Cargando...",
					        "oPaginate": {
					            "sFirst": "Primero",
					            "sLast": "Último",
					            "sNext": "<i class='fa fa-forward' aria-hidden='true'></i>",
					            "sPrevious": "<i class='fa fa-backward' aria-hidden='true'></i>"
					        },
					        "oAria": {
					            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
					            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
					        }
					    },
					    ajax: {
					        contentType: 'application/json',
					        url: urlServerSide,
					        type: 'POST',
					        data: preparetedVMDataTableFunction,
					        error: function (data) {
                                alert(data.statusText);
					        }
					    },
					    serverSide: true,
					    //Esta opción deshabilitada el ordenamiento de la columna acciones
					    columnDefs: [{
					        "orderable": false,
					        "targets": numUnorderedColumn
					    }],
					    columns: arrayColumns,
					    drawCallback: posDrawDataTableFunction
					});
}