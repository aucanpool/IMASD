$(document).ready(function () {
    var urlDatable = $('#employeesDT').data('url');
    window.dataTableEmployee = bindDataTable('#employeesDT', document, urlDatable, preparetedVMDataTable, 10, columnsDataTable, posDrawDataTable, false);
    eventsForButtonCreate();
    $("#searchForm").submit(function (event) {
        event.preventDefault();  // disable link event.
        window.dataTableEmployee.draw();
    });

});

// Funcion para preparar el VMDataTable
var preparetedVMDataTable = function (dataTablesInput) {
    var form = $("#searchForm");
    
    var data = form.serializeArray();
    var filters = {};
    $.each(data, function (index, item) {
        filters[item.name] = item.value;
    });
    var VMDataTable = { dataTablesInput: dataTablesInput };
    VMDataTable.filters = filters;
    return JSON.stringify(VMDataTable);
};

var columnsDataTable = [

{
    data: 'Departament.Name'
},
{
    data: 'SalaryTabulator.Key'
},
{
    data: 'JobNumber'
},
{
    data: 'FirstName'
},
{
    data: 'LastName'
},
{
    data: 'Address'
},
{
    data: 'Telefone'
},
{
    data: 'Gender'
},
{
    data: 'HireDate',
    
    render: function (data) {
        
        return ToJavaScriptDate(data);
    }
},

{
    data: 'Active',
    render: function (data, type, row, meta) {
        var urlActiveEmployee = $('#urlActiveEmployee').val();
        var buttonDelete = '';
        if (data) {
            buttonDelete = '<a id="desactiveEmployee" class="desactiveButtonDataTable btn btn-round btn-success btn-xs" href="'+urlActiveEmployee+'/'
					+ !data
					+ '/'
					+ row.Id
					+ '">Activado</a>';

        } else {
            buttonDelete = '<a id="activeEmployee" class="activeButtonDataTable btn btn-round btn-danger btn-xs" href="' + urlActiveEmployee + '/'
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
        var urlEditEmployee=$('#urlEditEmployee').val();
        var urlDetailsEmployee=$('#urlDetailsEmployee').val();
        var buttonEdit = '<button  data-toggle="tooltip" data-placement="left" title="Editar" aria-label="Left Align"  class="buttonEdit btn btn-default btn-sm btn-size-table" data-url="' + urlEditEmployee + '/'
				+ data
				+ '"><i class="fa fa-pencil fa-lg"></i></button>';
        var buttonDetails = '<button data-toggle="tooltip" data-placement="left" title="Ver detalles" aria-label="Left Align"  class="buttonDetail btn btn-default btn-sm btn-size-table" data-url="' + urlDetailsEmployee + '/'
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
    $("a#desactiveEmployee, a#activeEmployee").on('click', function (event) {
        event.preventDefault();  // disable link event.
        debugger
        var link = $(this);
        var url = link.attr('href');
        var titleSwal = '¿Estas seguro de ';
        var myconfirmButtonText = '';
        if (link.attr('id') == 'activeEmployee') {
            titleSwal += 'activar al empleado?';
            myconfirmButtonText = 'Activar!';
        } else {
            titleSwal += 'desactivar al empleado?';
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
                inputsValidInput();
            },
            error: function (data) {
                alert("Ocurrio un problema al intentar realizar su transacción");

            }
        });
    });
}
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
                inputsValidInput();
            },
            error: function (data) {
                alert("Ocurrio un problema al intentar realizar su transacción");

            }
        });
    });
}

//Edit Or Create Employee
function postSubmitEdit() {

    $('#editForm, #createForm')
    .submit(
            function (event) {

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
                        window.dataTableEmployee.draw();
                        
                        if (form[0].id == "createForm") {
                            confirm("Empleado creado con éxito");
                        } else {
                            confirm("Empleado editado con éxito");
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
var deletSumitForm = function () {
    $("#load .activeButton").on('click',function(e){
        e.preventDefault();
        
        var url = $("#urlActive").val();
        $.ajax({
            type: 'Delete',
            url: url,
            success: function (employeeDTO) {
                
                if (employeeDTO.Active)
                    alert('Se activo con éxito el empleado.');
                else
                    alert('Se desactivo con éxito el empleado');

                window.dataTableEmployee.draw();
                $('#flipFlop').modal('hide');
            },
            error: function (data) {
                
                alert("Ocurrio un error con el empleado");

            }
        });
    });
    
};
var inputsValidInput = function () {

    $("#JobNumber").on("input", function (evt) {
        var self = $(this);
        self.val(self.val().replace(/[^0-9]/g, ''));
        if ((evt.which != 46 || self.val().indexOf('.') != -1) && (evt.which < 48 || evt.which > 57)) {
            evt.preventDefault();
        }
    });
    $('.date').datepicker({
        format: 'dd/mm/yyyy',
        calendarWeeks: true,
        todayHighlight:true,
        language:'es'
    }).on('changeDate', function (e) {
        $(this).datepicker('hide');
    });
};