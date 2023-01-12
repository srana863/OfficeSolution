var Employee = function () {
    var ckeditorAddress = "";
    var ckeditorAbout = "";
    var ckeditorProfileSection = "";
    var init = function () {
        initialEvent();
        loadEmployee();
        showHideProfileSection();
        getDepartmentCombo('DepartmentId', true);
        getDesignationCombo('DesignationId', true);
        getProfileSectionCombo('ProfileSectionId', true);
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
        $('#btnAddProfileSection').click(saveProfileSection);
        $('#btnClear').click(resetForm);
        $(document).on('change', '#ProfileSectionId', function () {
            ckeditorProfileSection.setData("");
            var profileSectionId = $(this).val();
            var EmployeeId = $('#EmployeeId').val();
            var url = "/Employee/GetProfileSectionDetails";
            ajaxRequest(url, 'GET', { profileSectionId: profileSectionId, EmployeeId: EmployeeId }, true, false, function (res) {
                if (res) {
                    ckeditorProfileSection.setData(res.ProfileSectionDetails);
                    showHideProfileSection();
                }
            });
        });

        $(document).on('click', '.btnEdit', function () {
            var EmployeeId = $(this).data('id');
            ckeditorProfileSection.setData("");
            $('#ProfileSectionId').val();
            var url = "/Employee/GetEmployee";
            ajaxRequest(url, 'GET', { EmployeeId: EmployeeId }, true, false, function (res) {
                setFormData(res, null);
                $('#DateOfBirth').val(datefromjsonUS(res.DateOfBirth));
                ckeditorAddress.setData(res.Address);
                ckeditorAbout.setData(res.About);
                showHideProfileSection();
            });
        });
        $(document).on('click', '.btnDelete', function () {
            var EmployeeId = $(this).data('id');

            if (EmployeeId > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/Employee/DeleteEmployee";
                        ajaxRequest(url, 'POST', { EmployeeId: EmployeeId }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            if (res.MessageType == '1' || res.MessageType == 'Success') {
                                loadEmployee();
                                showHideProfileSection();
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
                showNotification(3, "Select Employee!!");
            }
        });
    };
    var showHideProfileSection = function () {
        var EmployeeId = $('#EmployeeId').val();
        if (EmployeeId > 0) {
            $("#profilesectiondivid").show();
        } else {
            $("#profilesectiondivid").hide();
        }


    };
    var resetForm = function () {
        clearFormField();
        ckeditorAddress.setData("");
        ckeditorAbout.setData("");
        ckeditorProfileSection.setData("");
        showHideProfileSection();
        iValidation.RemoveValidation('frmEmployee');
    };
    var loadEmployee = function () {
        var url = "/Employee/GetAllEmployee";
        $("#formDetails").load(url);
    };

    var saveProfileSection = function () {
        var model = new Object();
        model.EmployeeId = $('#EmployeeId').val();
        model.ProfileSectionId = $('#ProfileSectionId').val();
        model.ProfileSectionDetails = ckeditorProfileSection.getData();

        model.IsActive = $('#IsActive').prop("checked");
        model.PAOfDeptHead = $('#PAOfDeptHead').prop("checked");
        model.IsOfficeHead = $('#IsOfficeHead').prop("checked");
        var url = "/Employee/SaveProfileSection";
        ajaxRequest(url, 'POST', model, false, true, function (res) {
            showNotification(res.MessageType, res.Message);
            if (res.MessageType == '1' || res.MessageType == 'Success') {
                showHideProfileSection();
            }
        });
    };

    var saveData = function () {

        if (iValidation.Validate('frmEmployee')) {
            var model = getFormData('form-element');
            model.Address = ckeditorAddress.getData();
            model.About = ckeditorAbout.getData();
            model.ProfileSectionDetails = ckeditorProfileSection.getData();

            model.IsActive = $('#IsActive').prop("checked");
            model.PAOfDeptHead = $('#PAOfDeptHead').prop("checked");
            model.IsOfficeHead = $('#IsOfficeHead').prop("checked");
            var url = "/Employee/SaveEmployee";
            ajaxRequest(url, 'POST', model, false, true, function (res) {
                showNotification(res.MessageType, res.Message);
                if (res.MessageType == '1' || res.MessageType == 'Success') {
                    resetForm();
                    loadEmployee();
                }
            });

        }
    };

    return {
        init: init
    };
}();