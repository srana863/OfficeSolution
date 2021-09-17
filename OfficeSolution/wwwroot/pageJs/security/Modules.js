var Modules = function () {
    var init = function () {
        initialEvent();
        loadModules();
    };
    var initialEvent = function () {

        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var moduleId = $(this).data('id');
            var url = "/Security/GetModules";
            ajaxRequest(url, 'GET', { moduleId: moduleId }, true, false, function (res) {
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var moduleId = $(this).data('id');

            if (moduleId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Security/DeleteModules";
                        ajaxRequest(url, 'POST', { moduleId: moduleId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadModules();
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
                showNotification(3, "Select Module!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmModules');
    };
    var loadModules = function () {
        var url = "/Security/GetAllModules";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmModules')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Security/SaveModules";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadModules();
                }
            });

        }
    };

    return {
        init: init
    };
}();