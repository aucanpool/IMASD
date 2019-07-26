$(document).ready(function () {
    var urlDatable = $('#paymentsDT').data('url');
    window.dataTablePayment = bindDataTable('#paymentsDT', document, urlDatable, preparetedVMDataTable, 6, columnsDataTable, posDrawDataTable, false);
    $("#searchForm").submit(function (event) {
        event.preventDefault();  // disable link event.
        window.dataTablePayment.draw();
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
    data: 'Employee.FirstName'
},
{
    data: 'Employee.LastName'
},
{
    data: 'FrequencyofPayments'
},
{
    data: 'StarDate',

    render: function (data) {

        return ToJavaScriptDate(data);
    }
},
{
    data: 'EndDate',

    render: function (data) {

        return ToJavaScriptDate(data);
    }
},
{
    data: 'ProcessedDate',

    render: function (data) {

        return ToJavaScriptDate(data);
    }
},
{
    data: 'VoidDate',

    render: function (data) {
        
        if (data===null)
            return data;
        return ToJavaScriptDate(data);
    }
}
];

var posDrawDataTable = function () {

    $('[data-toggle="tooltip"]').tooltip();
    
}