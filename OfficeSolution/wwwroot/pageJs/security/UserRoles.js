var UserRoles = function () {
    var init = function () {
        initialEvent();
        loadUserRoles();
    };
    var initialEvent = function () {

        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var roleId = $(this).data('id');
            var url = "/Security/GetUserRoles";
            ajaxRequest(url, 'GET', { roleId: roleId }, true, false, function (res) {
                setFormData(res, null);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var roleId = $(this).data('id');

            if (roleId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Security/DeleteUserRoles";
                        ajaxRequest(url, 'POST', { roleId: roleId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadUserRoles();
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
                showNotification(3, "Select User Roles!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmUserRoles');
    };
    var loadUserRoles = function () {
        var url = "/Security/GetAllUserRoles";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmUserRoles')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Security/SaveUserRoles";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadUserRoles();
                }
            });

        }
    };

    return {
        init: init
    };
}();