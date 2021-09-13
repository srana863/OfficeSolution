var Department = function () {
    var init = function () {
        initialEvent();
        loadDepartment();
    };
    var initialEvent = function () {
    };

    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmDepartment');
    };
    var loadDepartment = function () {
        var url = "/Dashboard/GetAll";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmDepartment')) {
            var Department = getDataWithFile('form-element');
            var url = "/Department/Save";
            requestWithFile(url, 'POST', Department, false, true, false, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadDepartment();
                }
            });

        }
    };

    return {
        init: init
    };
}();