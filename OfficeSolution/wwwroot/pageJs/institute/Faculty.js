var Faculty = function () {
    var ckeditorAddress = "";
    var ckeditorAbout = "";
    var ckeditorProfileSection = "";
    var init = function () {
        initialEvent();
        loadFaculty();
        getDepartmentCombo('DepartmentId', true);
        getDesignationCombo('DesignationId', true);
        getGenderCombo('Gender', true);
        CKEDITOR.replace('Address');
        CKEDITOR.replace('About');
        CKEDITOR.replace('ProfileSectionDetails');
        ckeditorAbout = CKEDITOR.instances['About'];
        ckeditorAddress = CKEDITOR.instances['Address'];
        ckeditorProfileSection = CKEDITOR.instances['ProfileSectionDetails'];
    };
    var initialEvent = function () {

        $('#btnSave').click(saveData);
        $('#btnClear').click(resetForm);
        $(document).on('click', '.btnEdit', function () {
            var facultyId = $(this).data('id');
            var url = "/Faculty/GetFaculty";
            ajaxRequest(url, 'GET', { facultyId: facultyId }, true, false, function (res) {
                setFormData(res, null);
                $('#DateOfBirth').val(datefromjsonUS(res.DateOfBirth));
                 ckeditorAddress.setData(res.Address);
                 ckeditorAbout.setData(res.About);
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var facultyId = $(this).data('id');

            if (facultyId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Faculty/DeleteFaculty";
                        ajaxRequest(url, 'POST', { facultyId: facultyId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadFaculty();
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
                showNotification(3, "Select faculty!!");
            }
        });
    };
    var resetForm = function () {
        clearFormField();
        ckeditorAddress.setData("");
        ckeditorAbout.setData("");
        iValidation.RemoveValidation('frmFaculty');
    };
    var loadFaculty = function () {
        var url = "/Faculty/GetAllFaculty";
        $("#formDetails").load(url);
    };
    var saveData = function () {
      
        if (iValidation.Validate('frmFaculty')) {
            var model = getFormData('form-element');
            model.Address = ckeditorAddress.getData();
            model.About = ckeditorAbout.getData();

            model.IsActive = $('#IsActive').prop("checked");
            var url = "/Faculty/SaveFaculty";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadFaculty();
                }
            });

        }
    };

    return {
        init: init
    };
}();