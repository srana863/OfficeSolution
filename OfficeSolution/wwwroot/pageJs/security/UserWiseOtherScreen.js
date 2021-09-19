var UserWiseOtherScreen = function () {
    var userScreenDetailsViewModel = [];
    var newId = 1;
    var init = function () {
        initialEvent();
    };
    var initialEvent = function () {
        getUserCombo('UserId', true);
        getModuleCombo('ModuleId', true);
        $('#btnAdd').click(addScreenToList);
        $(document).on('change', '#UserId', function () {
            loadUserWiseOtherScreenData();
        });
        $(document).on('change', '#ModuleId', function () {
            var moduleId = $(this).val();
            getSubModuleCombo('SubModuleId', moduleId, true);
        });

        $(document).on('change', '#SubModuleId', function () {
            var moduleId = $('#ModuleId').val();
            var subModuleId = $(this).val();

            getScreenCombo('ScreenCode', moduleId, subModuleId, true);
        });
        $('#btnSave').click(saveData);
        $('#btnClear').click(resetform);

        $(document).on('click', '#AllAdd', function () {
            if (this.checked) {
                $('.canAdd:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.canAdd:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });
        $(document).on('click', '#AllStatus', function () {
            if (this.checked) {
                $('.status:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.status:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });
        $(document).on('click', '#AllView', function () {
            if (this.checked) {
                $('.canView:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.canView:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });

        $(document).on('click', '#AllModify', function () {
            if (this.checked) {
                $('.canModify:checkbox').each(function () {
                    this.checked = true;
                });
            } else {
                $('.canModify:checkbox').each(function () {
                    this.checked = false;
                });
            }
        });

        $(document).on('click', '.btnDelete', function () {
            var isTemp = $(this).data('id');
            var sl = $(this).data('nsid');

            if (isTemp.indexOf("t") >= 0) {
                $('#' + isTemp).remove();
            } else {
                if (sl > 0) {
                    $.confirm({
                        title: 'Delete Message',
                        text: 'Do you want to delete this?',
                        confirm: function () {
                            var url = "/ScreenPermission/DeleteUserWiseOtherScreen";
                            ajaxRequest(url, 'POST', { sl: sl }, true, false, function (res) {
                                showNotification(res.MessageType, res.Message);
                                if (res.MessageType == '1' || res.MessageType == 'Success') {
                                    $('#' + isTemp).remove();
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
                    showNotification(3, "Select Screen!!");
                }

            }
        });

    };

    var addScreenToList = function () {
        var screenCode = $('#ScreenCode').val();
        if (screenCode) {
            var moduleName = $('#ModuleId option:selected').html();
            var subModuleName = $('#SubModuleId option:selected').html();
            var screenName = $('#ScreenCode option:selected').html();
            var isExist = 0;
            $('#tblPermissionDetails>tbody tr').each(function () {
                $projectName = $(this).find('td').eq(1).text().trim();
                var chk = $(this).attr('class');
                if (chk.indexOf("odd") >= 0) {
                    if (chk.indexOf("ok") <= -1) {
                        $(this).remove();
                    }
                }

                if ($projectName == ScreenCode) {
                    showNotification(3, "Already Exist!");
                    isExist = 1;
                }
            });
            if (isExist == 0) {
                var tr = null;
                var permissionSL = "NSt" + newId;
                tr += "<tr id=" + permissionSL + "  class='ok'>";
                tr += "<td hidden><input type='hidden' value='0' /></td>";
                tr += "<td hidden><input type='hidden' value='" + screenCode + "' /></td>";
                tr += "<td>" + moduleName + "</td>";
                tr += "<td>" + subModuleName + "</td>";
                tr += "<td>" + screenName + "</td>";
                tr += "<td style='align-content:center'><input type='checkbox' class='flat-red canView'></td>";
                tr += "<td style='align-content:center'><input type='checkbox' class='flat-red canModify'></td>";
                tr += "<td style='align-content:center'><input type='checkbox' class='flat-red canAdd'></td>";
                tr += "<td style='align-content:center'><input type='checkbox' class='flat-red status'></td>";

                tr += "<td style='align-content:center'> <button type='button' class='btn btn-danger btn-sm btnDelete' data-nsid='0' data-id=" + permissionSL + "><i class='fa fa-trash'></i> Delete </button></td>";
                tr += "</tr>";

                $("#tblPermissionDetails > tbody").append(tr);

                newId++;

            }
        } else {
            showNotification(3, "Select Screen!");
        }


    };

    var resetform = function () {
        clearFormField();
        iValidation.RemoveValidation('frmUserWiseOtherScreen');
    };
    var loadUserWiseOtherScreenData = function () {
        var moduleId = $('#ModuleId').val();
        var subModuleId = $('#SubModuleId').val();
        var userId = $('#UserId').val();

        if (userId > 0) {
            var url = "/ScreenPermission/GetAllUserWiseOtherScreen?userId=" + userId + "&moduleId=" + moduleId + "&subModuleId=" + subModuleId;
            $("#formDetails").load(url);

        } else {
            showNotification(3, "Select User!");
        }
    };
    var saveData = function () {
        userScreenDetailsViewModel = [];
        var permissionData = new Object();
        var userId = $("#UserId").val();
        if (userId > 0) {
            $('#tblPermissionDetails>tbody tr').each(function (i) {
                permissionData = new Object();
                permissionData.UserId = userId;
                permissionData.SL = $(this).find('td').eq(0).find('input').val().trim();
                permissionData.ScreenCode = $(this).find('td').eq(1).find('input').val().trim();
                permissionData.CanView = $(this).find('td').eq(5).find('input[type="checkbox"]').prop("checked");
                permissionData.CanModify = $(this).find('td').eq(6).find('input[type="checkbox"]').prop("checked");
                permissionData.CanAdd = $(this).find('td').eq(7).find('input[type="checkbox"]').prop("checked");
                permissionData.IsActive = $(this).find('td').eq(8).find('input[type="checkbox"]').prop("checked");
                alert(permissionData.UserId);
                userScreenDetailsViewModel.push(permissionData);
            });
        }
        if (userScreenDetailsViewModel.length > 0) {
            var url = "/ScreenPermission/SaveUserWiseOtherScreen";
            $.post(url, { userScreenDetailsViewModel: userScreenDetailsViewModel }, function (res) {
                showNotification(res.MessageType, res.Message);
                loadUserWiseOtherScreenData();
            });
        } else {
            showNotification(-1, "Please select permission!");
        }

    };

    return {
        init: init
    };
}();


