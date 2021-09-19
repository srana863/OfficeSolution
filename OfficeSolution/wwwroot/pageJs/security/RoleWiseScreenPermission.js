var RoleWiseScreenPermission = function () {
    var roleScreenDetailsViewModel = [];
    var init = function () {
        initialEvent();
    };
    var initialEvent = function () {
        getUserRoleCombo('RoleId', true);
        getModuleCombo('ModuleId', true);
        $('#btnSave').hide();
        $('#btnClear').hide();
        $(document).on('change', '#ModuleId', function () {
            var moduleId = $(this).val();
            getSubModuleCombo('SubModuleId', moduleId, true);
            loadRoleWiseScreenPermissionData();
        });
        $(document).on('change', '#SubModuleId', function () {
            loadRoleWiseScreenPermissionData();
        });
        $(document).on('change', '#RoleId', function () {
            $('#btnSave').show();
            $('#btnClear').show();
            loadRoleWiseScreenPermissionData();
        });
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetform);

        $(document).on('click', '.btnDelete', function () {
            var sl = $(this).data('nsid');
            if (sl > 0) {
                $.confirm({
                    title: 'Delete Message',
                    text: 'Do you want to delete this?',
                    confirm: function () {
                        var url = "/ScreenPermission/DeleteRoleWiseScreenPermissio";
                        ajaxRequest(url, 'POST', { sl: sl }, true, false, function (res) {
                            showNotification(res.MessageType, res.Message);
                            loadRoleWiseScreenPermissionData();
                        });
                    },
                    cancel: function () {
                        return false;
                    },
                    confirmButton: "Yes",
                    cancelButton: "No"
                });

            } else {
                showNotification(3, "Select Screen!!");
            }

        });

        $(document).on('click', '#AllAdd', function () {
            if (this.checked) {
                $('.CanAdd:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.CanAdd:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });
        $(document).on('click', '#AllStatus', function () {
            if (this.checked) {
                $('.Status:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.Status:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });
        $(document).on('click', '#AllView', function () {
            if (this.checked) {
                $('.CanView:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.CanView:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });

        $(document).on('click', '#AllModify', function () {
            if (this.checked) {
                $('.CanModify:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.CanModify:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });


    };
    var resetform = function () {
        clearFormField();
        iValidation.RemoveValidation('frmRoleWiseScreenPermission');
    };
    var loadRoleWiseScreenPermissionData = function () {
        var moduleId = $('#ModuleId').val();
        var subModuleId = $('#SubModuleId').val();
        var roleId = $('#RoleId').val();

        if (roleId > 0) {
            var url = "/ScreenPermission/GetAllRoleWiseScreenPermission?roleId=" + roleId + "&moduleId=" + moduleId + "&subModuleId=" + subModuleId;
            $("#formDetails").load(url);

        } else {
            showNotification(3, "Select Role!");
        }

    };
    var saveData = function () {
        roleScreenDetailsViewModel = [];
        var permissionData = new Object();
        var roleId = $("#RoleId").val();
        if (roleId > 0) {
            $('#tblPermissionDetails>tbody tr').each(function (i) {
                permissionData = new Object();
                permissionData.RoleId = roleId;
                permissionData.PermissionSL = $(this).find('td').eq(0).find('input').val().trim();
                permissionData.ScreenCode = $(this).find('td').eq(1).find('input').val().trim();
                permissionData.CanView = $(this).find('td').eq(5).find('input[type="checkbox"]').prop("checked");
                permissionData.CanModify = $(this).find('td').eq(6).find('input[type="checkbox"]').prop("checked");
                permissionData.CanAdd = $(this).find('td').eq(7).find('input[type="checkbox"]').prop("checked");
                permissionData.IsActive = $(this).find('td').eq(8).find('input[type="checkbox"]').prop("checked");
                roleScreenDetailsViewModel.push(permissionData);
            });
        }
        if (roleScreenDetailsViewModel.length > 0) {
            var url = "/ScreenPermission/SaveRoleWiseScreenPermission";
            $.post(url, { roleScreenDetailsViewModel: roleScreenDetailsViewModel }, function (res) {
                showNotification(res.MessageType, res.Message);
                loadRoleWiseScreenPermissionData();
            });
        } else {
            showNotification(-1, "Please select permission!");
        }

    };

    return {
        init: init
    };
}();




//var addScreenToList = function () {
//    var screenCode = $('#ScreenCode').val();
//    if (screenCode) {

//        var moduleName = $('#ModuleId option:selected').html();
//        var subModuleName = $('#SubModuleId option:selected').html();
//        var screenName = $('#ScreenCode option:selected').html();
//        var shiftingDate = $('#ShiftingDate').val();
//        var canAdd = $('#CanAdd').prop("checked");
//        var canModify = $('#CanModify').prop("checked");
//        var CanView = $('#CanView').prop("checked");
//        var canViewChecked = canView ? "checked" : "";
//        var canAddChecked = canAdd ? "checked" : "";
//        var canModifyChecked = canModify ? "checked" : "";
//        var isExist = 0;
//        $('#tblPermissionDetails tbody tr').each(function () {
//            $projectName = $(this).find('td').eq(1).text().trim();
//            $day = $(this).find('td').eq(4).find('input').is(':checked');

//            var chk = $(this).attr('class');
//            if (chk.indexOf("odd") >= 0) {
//                if (chk.indexOf("ok") <= -1) {
//                    $(this).remove();
//                }
//            }

//            if ($projectName == ScreenCode && $day == CanView) {
//                showNotification(3, "Already Exist!");
//                isExist = 1;
//            }
//        });
//        if (isExist == 0) {
//            var tr = null;
//            var permissionSL = "NSt" + newId;
//            tr += "<tr id=" + permissionSL + "  class='ok'>";
//            tr += "<td hidden><input type='hidden' value='0' /></td>";
//            tr += "<td>" + screenCode + "</td>";
//            tr += "<td>" + screenName + "</td>";
//            tr += "<td>" + shiftingDate + "</td>";
//            tr += "<td style='align-content:center'><input type='checkbox' " + canViewChecked + " class='flat-red CanView'></td>";
//            tr += "<td style='align-content:center'><input type='checkbox' " + canModifyChecked + " class='flat-red CanModify'></td>";
//            tr += "<td style='align-content:center'><input type='checkbox' " + canAddChecked + " class='flat-red canAdd'></td>";
//            tr += "<td style='align-content:center'><input type='checkbox' " + canAddChecked + " class='flat-red canAdd'></td>";

//            tr += "<td style='align-content:center'> <button type='button' class='btn btn-danger btn-sm btnDelete' data-permissionSL='0' data-id=" + permissionSL + "><i class='fa fa-trash'></i> Delete </button></td>";
//            tr += "</tr>";

//            $("#tblPermissionDetails > tbody").append(tr);

//            newId++;
//            resetform();

//        }
//    } else {
//        showNotification(3, "Select Screen!");
//    }


//};