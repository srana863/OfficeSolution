$(document)
    .ajaxStart(function () { showGlobalLoader(); })
    .ajaxStop(function () { hideGlobalLoader(); });

$('.decimal').on('keyup', function (e) {
    var ex = /^[0-9]+\.?[0-9]*$/;
    if (ex.test(this.value) == false) {
        this.value = this.value.substring(0, this.value.length - 1);
    }

});
$('.number').on('keyup', function (e) {
    var ex = /^[0-9]*$/;
    if (ex.test(this.value) == false) {
        this.value = this.value.substring(0, this.value.length - 1);
    }
});

$('.datepicker-not-allow-futuredate').prop('readonly', true);
$('.datepicker-not-allow-futuredate').datepicker({
    format: 'mm/dd/yyyy',
    autoclose: true,
    todayBtn: 'linked',
    todayHightlight: true,
    endDate: "current"
});

$('.datepicker-allow-futuredate').prop('readonly', true);
$('.datepicker-allow-futuredate').datepicker({
    format: 'mm/dd/yyyy',
    autoclose: true,
    todayBtn: 'linked',
    todayHightlight: true
});


$('.readonly-input').prop('readonly', true);
$('.disabled-input').prop('disabled', true);

$(function () {
    $(".select2").select2();
     $("[data-mask]").inputmask();
    //$('.datepicker').prop('readonly', true);
    $('.datepicker').datepicker({
        format: 'mm/dd/yyyy',
        autoclose: true,
        todayBtn: 'linked',
        todayHightlight: true
    });
    var today = new Date();
    today.setDate(today.getDate());
});


function searchEmployee(controlId,nextCallFunction) {
    $('#SearchEmployeeName').modal("show");
    loadSearchEmployee(controlId, nextCallFunction);
}

function loadSearchEmployee(controlId, nextCallFunction) {
    var url = '/Common/EmployeeSearch';
    $('#Div-EmployeeList').empty().load(url);
    $('#SearchEmployeeName #nextId').val(controlId);
    $('#SearchEmployeeName #nextMethod').val(nextCallFunction);
}
