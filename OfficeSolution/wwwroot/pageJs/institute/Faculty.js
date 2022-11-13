var Faculty = function () {
    var ckeditorAddress = "";
    var ckeditorAbout = "";
    var ckeditorProfileSection = "";
    var init = function () {
        initialEvent();
        loadFaculty();
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
            var facultyId = $('#FacultyId').val();
            var url = "/Faculty/GetProfileSectionDetails";
            ajaxRequest(url, 'GET', { profileSectionId: profileSectionId, facultyId: facultyId }, true, false, function (res) {
                if (res) {
                    ckeditorProfileSection.setData(res.ProfileSectionDetails);
                    showHideProfileSection();
                }
            });
        });

        $(document).on('click', '.btnEdit', function () {
            var facultyId = $(this).data('id');
            ckeditorProfileSection.setData("");
            $('#ProfileSectionId').val();
            var url = "/Faculty/GetFaculty";
            ajaxRequest(url, 'GET', { facultyId: facultyId }, true, false, function (res) {
                setFormData(res, null);
                $('#DateOfBirth').val(datefromjsonUS(res.DateOfBirth));
                ckeditorAddress.setData(res.Address);
                ckeditorAbout.setData(res.About);
                showHideProfileSection();
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
                showNotification(3, "Select faculty!!");
            }
        });
    };
    var showHideProfileSection = function () {
        var facultyid = $('#FacultyId').val();
        if (facultyid > 0) {
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
        iValidation.RemoveValidation('frmFaculty');
    };
    var loadFaculty = function () {
        var url = "/Faculty/GetAllFaculty";
        $("#formDetails").load(url);
    };

    var saveProfileSection = function () {
        var model = new Object();
        model.FacultyId = $('#FacultyId').val();
        model.ProfileSectionId = $('#ProfileSectionId').val();
        model.ProfileSectionDetails = ckeditorProfileSection.getData();

        model.IsActive = $('#IsActive').prop("checked");
        var url = "/Faculty/SaveProfileSection";
        ajaxRequest(url, 'POST', model, false, true, function (res) {
            showNotification(res.MessageType, res.Message);
            if (res.MessageType == '1' || res.MessageType == 'Success') {
                showHideProfileSection();
            }
        });
    };

    var saveData = function () {

        if (iValidation.Validate('frmFaculty')) {
            var model = getFormData('form-element');
            model.Address = ckeditorAddress.getData();
            model.About = ckeditorAbout.getData();
            model.ProfileSectionDetails = ckeditorProfileSection.getData();

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