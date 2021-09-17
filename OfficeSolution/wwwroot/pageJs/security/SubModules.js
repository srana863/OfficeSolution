var SubModules = function () {
    var init = function () {
        initialEvent();
        loadSubModules();
    };
    var initialEvent = function () {
        getModuleCombo('ModuleId',true);
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var subModuleId = $(this).data('id');
            var url = "/Security/GetSubModules";
            ajaxRequest(url, 'GET', { subModuleId: subModuleId }, true, false, function (res) {
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var subModuleId = $(this).data('id');

            if (subModuleId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Security/DeleteSubModules";
                        ajaxRequest(url, 'POST', { subModuleId: subModuleId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadSubModules();
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
                showNotification(3, "Select Sub Module!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmSubModules');
    };
    var loadSubModules = function () {
        var url = "/Security/GetAllSubModules";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmSubModules')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Security/SaveSubModules";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadSubModules();
                }
            });

        }
    };

    return {
        init: init
    };
}();