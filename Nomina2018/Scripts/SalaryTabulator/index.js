$(document).ready(function () {
    var urlDatatable = $("#salaryTabulatorsDT").data("url");
    window.dataTableSalaryTabulator = bindDataTable("#salaryTabulatorsDT", document, urlDatatable, preparetedVMDataTable, 8, columnsDataTable, posDrawDataTable, true);
    eventsForButtonCreate();
});
// Funcion para preparar el VMDataTable
var preparetedVMDataTable = function (dataTablesInput) {

    return JSON.stringify(dataTablesInput);
};
var columnsDataTable = [

{
    data: 'Job.Name'
},
{
    data: 'Key'
},
{
    data: 'TabulatorLevel',
    render: function (data) {
        return TabulatorSalary[data];
    }
},
{
    data: 'Hourlywages'
},
{
    data: 'AnnualHolidayBonus'
},
{
    data: 'AnnualBonusDays'
},
{
    data: 'AnnualVacationDays'
},
{
    data: 'Active',
    render: function (data, type, row, meta) {
        var urlActiveSalaryTabulator = $('#urlActiveSalaryTabulator').val();
        var buttonDelete = '';
        if (data) {
            buttonDelete = '<a id="desactiveSalaryTabulator" class="desactiveButtonDataTable btn btn-round btn-success btn-xs" href="' + urlActiveSalaryTabulator + '/'
					+ !data
					+ '/'
					+ row.Id
					+ '">Activado</a>';

        } else {
            buttonDelete = '<a id="activeSalaryTabulator" class="activeButtonDataTable btn btn-round btn-danger btn-xs" href="' + urlActiveSalaryTabulator + '/'
				+ !data
				+ '/'
				+ row.Id
					+ '">Desactivado</a>';

        }
        return buttonDelete;
    }
},
{
    data: 'Id',
    render: function (data, type, row, meta) {
        var urlEditSalaryTabulator = $('#urlEditSalaryTabulator').val();
        var urlDetailsSalaryTabulator = $('#urlDetailsSalaryTabulator').val();
        var buttonEdit = '<button  data-toggle="tooltip" data-placement="left" title="Editar" aria-label="Left Align"  class="buttonEdit btn btn-default btn-sm btn-size-table" data-url="' + urlEditSalaryTabulator + '/'
				+ data
				+ '"><i class="fa fa-pencil fa-lg"></i></button>';
        var buttonDetails = '<button data-toggle="tooltip" data-placement="left" title="Ver detalles" aria-label="Left Align"  class="buttonDetail btn btn-default btn-sm btn-size-table" data-url="' + urlDetailsSalaryTabulator + '/'
				+ data
				+ '"><i class="fa fa-info-circle fa-lg"></i></button>';
        return '<div class="contentButtonsListDataTable">'
				+ buttonEdit
                + buttonDetails
				+ '</div>';
    }
}
];

var posDrawDataTable = function () {
    //Tooltip
    $('[data-toggle="tooltip"]').tooltip();
    eventsForButtonCrud();

    //// Delete DocumentType
    $("a#desactiveSalaryTabulator, a#activeSalaryTabulator").on('click', function (event) {
        event.preventDefault();  // disable link event.
        debugger
        var link = $(this);
        var url = link.attr('href');
        var titleSwal = '¿Estas seguro de ';
        var myconfirmButtonText = '';
        if (link.attr('id') == 'activeSalaryTabulator') {
            titleSwal += 'activar el tabuladr salarial?';
            myconfirmButtonText = 'Activar!';
        } else {
            titleSwal += 'desactivar el tabulador salarial?';
            myconfirmButtonText += 'Desactivar!';
        }
        $('.text-modal').text(titleSwal);
        $('.text-active').text(myconfirmButtonText);
        var inputUrl = $('<input/>');
        inputUrl.prop("type", "hidden");
        inputUrl.prop("id", "urlActive");
        inputUrl.val(url);

        var modalcontent = $('#deleteModal').html();

        $("#load").html(modalcontent).append(inputUrl);

        $('#flipFlop').modal('show');// Open edit
        deletSumitForm();
    });
    // Ends Delete DocumentType
}

//Events of cruds buttons
function eventsForButtonCrud() {
    $("button.buttonEdit,button.buttonDetail").on('click', function (event) {
        event.preventDefault();  // disable link event.
        var button = $(this);
        var url = button.data('url');

        $.ajax({
            type: 'get',
            url: url,
            contentType: "text/html",
            success: function (response) {
                $("#load").html(response);
                $('#flipFlop').modal('show');// Open edit
                postSubmitEdit();
            },
            error: function (data) {
                alert("Ocurrio un problema al intentar realizar su transacción");

            }
        });
    });
}
//ENDS Events of cruds buttons

// Events of button create
function eventsForButtonCreate() {
    $("button.buttonNew").on('click', function (event) {
        event.preventDefault();  // disable link event.

        var button = $(this);
        var url = button.data('url');

        $.ajax({
            type: 'get',
            url: url,
            contentType: "text/html",
            success: function (response) {
                $("#load").html(response);
                $('#flipFlop').modal('show');// Open edit
                postSubmitEdit();
            },
            error: function (data) {
                alert("Ocurrio un problema al intentar realizar su transacción");

            }
        });
    });
}
//Ends Events of button create


//Edit Or Create SalaryTabulator
function postSubmitEdit() {

    $('#editForm, #createForm')
    .submit(
            function (event) {
                debugger
                event.preventDefault();
                var form = $(this);
                var url = form.attr('action');

                var data = form.serialize();
                form.find('.field-validation-valid').text('');
                $('#spinner').show();
                form.find('button').prop('disabled', true).css("cursor", "not-allowed");
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: data,
                    async: true,
                    success: function (response) {
                        $('#flipFlop').modal('hide');
                        window.dataTableSalaryTabulator.draw();
                        if (form.id == "createForm") {
                            confirm("Tabulador salarial creado con éxito");
                        } else {
                            confirm("Tabulador salarial editado con éxito");
                        }

                    },
                    error: function (response) {

                        if (response.status === 400) {
                            var errors = JSON.parse(response.responseText);
                            for (var i = 0; i < errors.length; i++) {
                                $('span[data-valmsg-for="' + errors[i].key + '"]').text(errors[i].errors[0]);
                            }

                        }
                    }
                }).always(function () {
                    $('#spinner').hide();
                    form.find('button').prop('disabled', false).css("cursor", "pointer");
                });
            });

}
//ends Edit or create function

//Submit
var deletSumitForm = function () {
    $("#load .activeButton").on('click', function (e) {
        e.preventDefault();

        var url = $("#urlActive").val();
        $.ajax({
            type: 'Delete',
            url: url,
            success: function (salaryTabulatorDTO) {

                if (salaryTabulatorDTO.Active)
                    alert('Se activo con éxito el tabulador salarial.');
                else
                    alert('Se desactivo con éxito el tabulador salarial');

                window.dataTableSalaryTabulator.draw();
                $('#flipFlop').modal('hide');
            },
            error: function (data) {

                alert("Ocurrio un error con el tabulador salarial");

            }
        });
    });

};
//Ends Submit