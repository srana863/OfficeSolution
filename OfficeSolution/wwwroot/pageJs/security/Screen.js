var Screen = function () {
    var init = function () {
        initialEvent();
        loadScreen();
    };
    var initialEvent = function () {
        getModuleCombo('ModuleId', true);
        $(document).on('change', '#ModuleId', function () {
            var moduleId = $(this).val();
            getSubModuleCombo('SubModuleId', moduleId, true);
        });
        $(document).on('change', '#SubModuleId', function () {
            var moduleId = $('#ModuleId').val();
            var subModuleId = $(this).val();
            getSubModuleSectionsCombo('SectionId', moduleId, subModuleId ,true);
        });
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var screenId = $(this).data('id');
            var url = "/Security/GetScreen";
            ajaxRequest(url, 'GET', { screenId: screenId }, true, false, function (res) {
                getSubModuleCombo('SubModuleId', res.ModuleId, true);
                getSubModuleSectionsCombo('SectionId', res.ModuleId, res.SubModuleId, true);
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var screenId = $(this).data('id');

            if (screenId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Security/DeleteScreen";
                        ajaxRequest(url, 'POST', { screenId: screenId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadScreen();
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
                showNotification(3, "Select screen!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmScreen');
    };
    var loadScreen = function () {
        var url = "/Security/GetAllScreen";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmScreen')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Security/SaveScreen";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadScreen();
                }
            });

        }
    };

    return {
        init: init
    };
}();