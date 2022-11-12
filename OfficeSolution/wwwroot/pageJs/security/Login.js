var Login = function () {
    var init = function () {
        initialEvent();
        //loadModules();
    };
    var initialEvent = function () {
        $('#btnSave').click(saveData);
       // $('#btnClear').click(resetForm);
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmModules');
    };


    var saveData = function () {

        var UserName = $("#UserName");
        var password = $("#password");

        $.ajax({
            type: "POST",
            url: "/Home/Login",
            data: {"UserName": "' + UserName.val() + '", "password": "' + password.val() + '" },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                var row = $("#tblCustomers tr:last-child");
                if ($("#tblCustomers tr:last-child span").eq(0).html() != "&nbsp;") {
                    row = row.clone();
                }
                AppendRow(row, r.CustomerId, r.Name, r.Country);
                txtName.val("");
                txtCountry.val("");
            }
        });
    };

    return {
        init: init
    };
}();