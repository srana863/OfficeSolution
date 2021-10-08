var JobList = function () {
    var init = function () {
        initialEvent();
        loadJobList();
    };

    var initialEvent = function () {
        $('#SOCMainContainer').hide();
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $('#JobDescriptions').summernote();

        $(document).on('click', '.btnEdit', function () {
            var moduleId = $(this).data('id');
            var url = "/Security/GetJobList";
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
                        var url = "/Security/DeleteJobList";
                        ajaxRequest(url, 'POST', { moduleId: moduleId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadJobList();
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

        $('#JobType').on('change',onChangeJobType);
    };

    var resetForm = function () {
        clearFormField();
        iValidation.RemoveValidation('frmJobList');
    };

    var loadJobList = function () {
        var url = "/Recruitment/GetAllJobList";
        $("#formDetails").load(url);
    };
    var saveData = function () {
        if (iValidation.Validate('frmJobList')) {
            var model = getFormData('form-element');
            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Recruitment/SaveJobList";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadJobList();
                }
            });

        }
    };
    var onChangeJobType = function () {
        var type = $(this).val();
        

        if (type == "new") {
            $('#SOCMainContainer').show();
            $('#SOCCode').remove();
            var input = $('<input type="text" class="form-control form-element validation" required id="SOCCode" name="SOCCode" />');
            $('#SOCContainer').append(input);
        }
        else if (type == "existing") {
            $('#SOCMainContainer').show();
            $('#SOCCode').remove();
            var input = $('<select class="form-control form-element validation" required id="SOCCode" name="SOCCode"><option>Select SOC Code</option ></select >');
            $('#SOCContainer').append(input);
            var url = "/Recruitment/GetAllSOC";
            ajaxRequest(url, 'GET', {}, true, false, function (res) {
                $.each(res, function (index, value) {
                    $('#SOCCode').append('<option value="' + value + '">' + value + '</option>');
                });
            });
        }
        else {
            $('#SOCMainContainer').hide();
        }
    };
    return {
        init: init
    };
}();