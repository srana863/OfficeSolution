var NothiType = function () {
    var model = new FormData();
    var init = function () {
        initialEvent();
        getDepartmentCombo('DepartmentId', false);
        loadNothiType(0);
    };
    var initialEvent = function () {
        $(document).on('change', '#DepartmentId', function () {
            var deptId = $(this).val();
            loadNothiType(deptId);
        });
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var nothiTypeId = $(this).data('id');
            var url = "/NothiMovement/GetNothiType";
            ajaxRequest(url, 'GET', { nothiTypeId: nothiTypeId }, true, false, function (res) {
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var nothiTypeId = $(this).data('id');

            if (nothiTypeId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/NothiMovement/DeleteNothiType";
                        ajaxRequest(url, 'POST', { nothiTypeId: nothiTypeId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                $('#DepartmentId').trigger('change');
                            }
                        });
                    },
                    cancel: function () {
                        return false;
                    },
                    confirmButton: "Yes",
                    cancelButton: "No"
                });

            } else {
                showNotification(3, "Select Nothi Type!!");
            }
        });

    };

    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmNothiType');
    };
    var loadNothiType = function (deptId) {

        var url = "/NothiMovement/GetAllNothiType?departmentId=" + deptId;
        $("#formDetails").load(url);

    };
    var saveData = function () {
        if (iValidation.Validate('frmNothiType')) {
            model = new FormData();
            model = getDataWithFile('form-element');
            model.append('IsActive', $('#IsActive').prop('checked'));
            var url = "/NothiMovement/SaveNothiType";
            requestWithFile(url, 'POST', model, false, true, false, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    $('#DepartmentId').trigger('change');
                    resetForm();
                }
            });

        }
    };

    return {
        init: init
    };
}();