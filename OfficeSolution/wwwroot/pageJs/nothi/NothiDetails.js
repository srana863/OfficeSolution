var NothiDetails = function () {
    var model = new FormData();
    var init = function () {
        initialEvent();
        getDepartmentCombo('DepartmentId', false);
        getNothiTypeCombo('NothiTypeId', 0, true);
        loadNothiDetails(0);
    };
    var initialEvent = function () {
        $(document).on('change', '#DepartmentId', function () {
            var deptId = $(this).val();
            loadNothiDetails(deptId);
        });
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var sl = $(this).data('id');
            var url = "/NothiMovement/GetNothiDetails";
            ajaxRequest(url, 'GET', { sl: sl }, true, false, function (res) {
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var sl = $(this).data('id');

            if (sl > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/NothiMovement/DeleteNothiDetails";
                        ajaxRequest(url, 'POST', { sl: sl }, true, false, function (res) {
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
        iValidation.RemoveValidation('frmNothiDetails');
    };
    var loadNothiDetails = function (deptId) {

        var url = "/NothiMovement/GetAllNothi?departmentId=" + deptId;
        $("#formDetails").load(url);

    };
    var saveData = function () {
        if (iValidation.Validate('frmNothiDetails')) {
            model = new FormData();
            model = getDataWithFile('form-element');
            model.append('IsActive', $('#IsActive').prop('checked'));
            var url = "/NothiMovement/SaveNothiDetails";
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