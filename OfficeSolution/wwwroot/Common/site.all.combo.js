
function getNothiCombo(controlId, departmentId, isDefaultRecordRequired) {
    var url = '/Common/GetNothiCombo';
    var data = {
        departmentId: departmentId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getNothiTypeCombo(controlId, departmentId, isDefaultRecordRequired) {
    var url = '/Common/GetNothiTypeCombo';
    var data = {
        departmentId: departmentId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function getProfileSectionCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetProfileSectionCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getGenderCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetGenderCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}
function getAllDepartmentCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetAllDepartmentCombo';
    var data = {
    };
    loadComboDept(controlId, url, data, isDefaultRecordRequired);
}

function getDepartmentCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetDepartmentCombo';
    var data = {
    };
    loadComboDept(controlId, url, data, isDefaultRecordRequired);
}

function getDesignationCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetDesignationCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getUserCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetUserCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getUserRoleCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetUserRoleCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getModuleCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetModuleCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getSubModuleCombo(controlId, moduleId, isDefaultRecordRequired) {
    var url = '/Common/GetSubModuleCombo';
    var data = {
        moduleId: moduleId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getSubModuleSectionsCombo(controlId, moduleId, subModuleId, isDefaultRecordRequired) {
    var url = '/Common/GetSubModuleSectionsCombo';
    var data = {
        moduleId: moduleId,
        subModuleId: subModuleId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function getScreenCombo(controlId, moduleId, subModuleId, sectionId, isDefaultRecordRequired) {
    var url = '/Common/GetScreenCombo';
    var data = {
        moduleId: moduleId,
        subModuleId: subModuleId,
        sectionId: sectionId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadCombo(controlId, url, parameter, isDefaultRecordRequired) {
    $.ajax({
        url: url,
        type: 'get',
        async: false,
        data: parameter,
        success: function (res) {
            var data = res;

            $("#" + controlId).empty();
            $("#" + controlId).get(0).options.length = 0;
            if (isDefaultRecordRequired) {
                $("#" + controlId).get(0).options[0] = new Option("---- Select -----", "");
            }
            if (data != null) {
                $.each(data, function (index, item) {
                    $("#" + controlId).get(0).options[$("#" + controlId).get(0).options.length] = new Option(item.Text, item.Value, item.Selected, item.Selected);
                });
            }
        },
        error: function () {
        }
    });
}

function loadComboDept(controlId, url, parameter, isDefaultRecordRequired) {
    $.ajax({
        url: url,
        type: 'get',
        async: false,
        data: parameter,
        success: function (res) {
            var data = res;
            var options = null;
            $("#" + controlId).empty();
            $("#" + controlId).get(0).options.length = 0;
            if (isDefaultRecordRequired) {
                $("#" + controlId).get(0).options[0] = new Option("---- Select -----", "");
            }
            if (data != null) {
                $.each(data, function (index, item) {
                    $("#" + controlId).get(0).options[$("#" + controlId).get(0).options.length] = new Option(item.Text, item.Value, item.Selected, item.Selected);
                });

                $.each(data, function (index, item) {
                    options = $("#" + controlId + " option[value=" + item.Value + "]");
                    options.attr('disabled', item.Disabled);
                });

            }
        },
        error: function () {
        }
    });
}


