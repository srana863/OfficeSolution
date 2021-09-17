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
        moduleId:moduleId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function getSubModuleSectionsCombo(controlId, moduleId, subModuleId, isDefaultRecordRequired) {
    var url = '/Common/GetSubModuleSectionsCombo';
    var data = {
        moduleId:moduleId,
        subModuleId:subModuleId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function getScreenCombo(controlId, moduleId, subModuleId, sectionId, isDefaultRecordRequired) {
    var url = '/Common/GetScreenCombo';
    var data = {
        moduleId:moduleId,
        subModuleId:subModuleId,
        sectionId:sectionId
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

