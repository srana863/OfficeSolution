var NothiMovement = function () {
    var ckeditorRemarks = "";
    var init = function () {
        initialEvent();
        getNothiCombo('NothiId',0, true);
        getAllDepartmentCombo('SendToDepartmentId', true);
        CKEDITOR.replace('CommentsWhileSending');
        ckeditorRemarks = CKEDITOR.instances['CommentsWhileSending'];
        loadNothiMovement(0);
        financialAmountEnableDisable();
        nothiIdEnableDisable();
    };
    var initialEvent = function () {
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('change','#IsFinancial',financialAmountEnableDisable);
        $(document).on('click', '.btnEdit', function () {
            var nothiMovementId = $(this).data('id');
            var url = "/NothiMovement/GetNothiMovement";
            ajaxRequest(url, 'GET', { nothiMovementId: nothiMovementId }, true, false, function (res) {
                setFormData(res, null);
                ckeditorRemarks.setData(res.CommentsWhileSending);
                nothiIdEnableDisable();
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var nothiMovementId = $(this).data('id');
            $.confirm({
                title: 'Delete Message',
                text: 'Do you want to delete this?',
                confirm: function () {
                    var url = "/NothiMovement/DeleteNothiMovement";
                    ajaxRequest(url, 'POST', { nothiMovementId: nothiMovementId }, true, false, function (res) {
                        showNotification(res.MessageType, res.Message);
                        if (res.MessageType == '1' || res.MessageType == 'Success') {
                        }
                    });
                },
                cancel: function () {
                    return false;
                },
                confirmButton: "Yes",
                cancelButton: "No"
            });
        });

    };
    var nothiIdEnableDisable = function () {
        if ($('#NothiMovementId').val()>0) {
            $('#NothiId').prop('disabled', true);
        } else {
            $('#NothiId').prop('disabled', false);
        }
    };
    var financialAmountEnableDisable = function () {
        if ($('#IsFinancial').is(':checked')) {
            $('#FinancialAmount').prop('disabled', false);
        } else {
            $('#FinancialAmount').prop('disabled', true);
        }
    };

    var resetForm = function () {
        clearFormField();
        ckeditorRemarks.setData("");
        nothiIdEnableDisable();
        iValidation.RemoveValidation('frmNothiMovement');
    };
    var loadNothiMovement = function (deptId) {
        var url = "/NothiMovement/GetAllNothiMovement?departmentId=" + deptId;
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmNothiMovement')) {
            var model = getFormData('form-element');
            model.IsFinancial= $('#IsFinancial').prop('checked');
            model.CommentsWhileSending = ckeditorRemarks.getData();
            var url = "/NothiMovement/SaveNothiMovement";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    loadNothiMovement(0);
                    resetForm();
                }
            });

        }
    };

    return {
        init: init
    };
}();