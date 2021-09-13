function loadBranchDivisionCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetBranchDivisionCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadBranchRegionCombo(controlId, divisionId, isDefaultRecordRequired) {
    var url = '/Common/GetBranchRegionCombo';
    var data = {
        divisionId:divisionId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadProductCategoryCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetProductCategoryCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function loadSystemEmployeeCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetSystemEmployeeCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function loadFiscalYearCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetFiscalYearCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function loadRoleCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetRoleCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadCountry(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetCountryCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadDivisionCombo(controlId, countryCode, isDefaultRecordRequired) {
    var url = '/Common/GetDivisionCombo';
    var data = {
        countryCode: countryCode
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadDistrict(controlId, divisionID, isDefaultRecordRequired) {
    var url = '/Common/GetDistrictCombo';
    var data = {
        divisionID: divisionID
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadThana(controlId, districtID, isDefaultRecordRequired) {
    var url = '/Common/GetThanaCombo';
    var data = {
        districtID: districtID
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}
function loadMaritalStatus(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetMaritalStatusCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadIssueSubCategory(controlId, issueCategoryId,isDefaultRecordRequired) {
    var url = '/Common/GetIssueSubCategoryCombo';
    var data = {
        issueCategoryId: issueCategoryId
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadIssueCategory(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetIssueCategoryCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function loadDutyEmployeeCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetDutyEmployeeCombo';
    var data = {

    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}


function loadDesignationCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetDesignationCombo';
    var data = {
        
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadEmployeeDivisionCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetEmployeeDivisionCombo';
    var data = {
       
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}
function loadSectionCombo(controlId,departmentID ,isDefaultRecordRequired) {
    var url = '/Common/GetSectionCombo';
    var data = {
        departmentID:departmentID
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadAreaCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetAreaCombo';
    var data = {
    };
    loadCombo(controlId, url, data, isDefaultRecordRequired);
}

function loadMonthCombo(controlId, isDefaultRecordRequired) {
    var url = '/Common/GetMonthCombo';
    var data = {
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
                    $("#" + controlId).get(0).options[$("#" + controlId).get(0).options.length] = new Option(item.Text, item.Value, item.Selected,item.Selected);
                });
            }
        },
        error: function () {
        }
    });
}

