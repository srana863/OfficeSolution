var SubModuleSections = function () {
    var init = function () {
        initialEvent();
        loadSubModuleSections();
    };
    var initialEvent = function () {
        getModuleCombo('ModuleId', true);
        $(document).on('change', '#ModuleId', function () {
            var moduleId = $(this).val();
            getSubModuleCombo('SubModuleId', moduleId, true);
        });        
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var sectionId = $(this).data('id');
            var url = "/Security/GetSubModuleSections";
            ajaxRequest(url, 'GET', { sectionId: sectionId }, true, false, function (res) {

                getSubModuleCombo('SubModuleId', res.ModuleId, true);

                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var sectionId = $(this).data('id');

            if (sectionId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Security/DeleteSubModuleSections";
                        ajaxRequest(url, 'POST', { sectionId: sectionId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadSubModuleSections();
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
                showNotification(3, "Select Sub Module Section!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmSubModuleSections');
    };
    var loadSubModuleSections = function () {
        var url = "/Security/GetAllSubModuleSections";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmSubModuleSections')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Security/SaveSubModuleSections";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadSubModuleSections();
                }
            });

        }
    };

    return {
        init: init
    };
}();