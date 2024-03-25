function AddTM() {
    let DrpID = "TM" + 1;
    let html = "";
    //html += '<tr><td><input type="hidden" value="0"  name="Verify_TeamMember_List[' + 1 + '].Inc_Verification_TeamMember_Id"/><select class="form-select  required TM_NAME" id="' + DrpID + '"  name="Verify_TeamMember_List[' + 1 + '].TeamMember_Id">' + '<option style="text-align:center;">--Select Team Member--</option><option>Employee 1</option>' + '</select><span data-valmsg-replace="true" data-valmsg-for="Verify_TeamMember_List[' + 1 + '].TeamMember_Id" class="field-validation-valid text-danger V_TM_ID"></span></td>';
    html += '<tr><td><div class="form-group"><input type="hidden" class="Drill_CRR_ID"  name="_Acts[' + Count + '].Drill_CRR_ID" value="0"/><textarea class="form-control Imp_Action required" style="resize:vertical;height:17px" name="_Acts[' + Count + '].Action_Des"></textarea><span data-valmsg-replace="true" data-valmsg-for="_Acts[' + Count + '].Action_Des" class="field-validation-valid text-danger V_Imp_Action"></span></div></td>';
    html += '<td><div class="form-group"><label><input type="radio" class="SpotRd" name="_Acts[' + Count + '].Close_On_Spot" value="Yes">Yes</label><label><input type="radio" class="SpotRd" name="_Acts[' + Count + '].Close_On_Spot" value="No" checked="">No</label><span data-valmsg-replace="true" data-valmsg-for="_Acts[' + Count + '].Close_On_Spot" class="field-validation-valid text-danger V_SpotRd"></span></div></td>';
    html += '<td><div class="form-group"><input type="date" class="form-control TargetDate" min="' + todaydate + '" readonly name="_Acts[' + Count + '].Target_Date"/><span data-valmsg-replace="true" data-valmsg-for="_Acts[' + Count + '].Target_Date" class="field-validation-valid text-danger V_Target_Date"></span></div></td >';
    html += '<td><div class="form-group"> <textarea class="form-control ActionTaken" name="_Acts[' + Count + '].Corrective_Action" style="resize:vertical;height:17px" readonly="">NA</textarea><span data-valmsg-replace="true" data-valmsg-for="_Acts[' + Count + '].Corrective_Action" class="field-validation-valid text-danger V_ActionTaken"></span></div></td>';
    html += '<td><label class="file-placeholder" ><img src="/assets/images/image-.png" class="Imgtag" height = "50px" width = "50px" /><input type="hidden" class="Photo_Path" name="_Acts[' + Count + '].Photo_Path"/><input type="file" class="form-control Evidance" accept=".jpg,.png,.jpeg" disabled="" style="display: none;"></label></td>';
    html += '<td><button type="button" class="btn btn-danger DelBtn">Remove</button></td></tr>';
    $(UI_Fields.TBODY_TEAM_MEM).append(html);
    Count++;
    /* $('#'+DrpID).append(newDropdown);*/
    /*ApplySelect2(".TM_NAME");*/
}

function ResetValues() {
    let counter = 0;
    $("#tbl_TeamMem tbody > tr").each(function () {
        let ASSIGN_EMP = "ASSIGN_EMP" + counter;
        $(this).closest('tr').find('.Drill_CRR_ID').attr("name", "_Acts[" + counter + "].Drill_CRR_ID");
        $(this).closest('tr').find('.Imp_Action').attr("name", "_Acts[" + counter + "].Action_Des");
        $(this).closest('tr').find('.V_Imp_Action').attr("data-valmsg-for", "_Acts[" + counter + "].Action_Des");
        $(this).closest('tr').find('.SpotRd').attr("name", "_Acts[" + counter + "].Close_On_Spot");
        $(this).closest('tr').find('.V_SpotRd').attr("data-valmsg-for", "_Acts[" + counter + "].Close_On_Spot");
        $(this).closest('tr').find('.TargetDate').attr("name", "_Acts[" + counter + "].Target_Date");
        $(this).closest('tr').find('.V_TargetDate').attr("data-valmsg-for", "_Acts[" + counter + "].Target_Date");
        $(this).closest('tr').find('.ActionTaken').attr("name", "_Acts[" + counter + "].Corrective_Action");
        $(this).closest('tr').find('.V_ActionTaken').attr("data-valmsg-for", "_Acts[" + counter + "].Corrective_Action");
        $(this).closest('tr').find('.Photo_Path').attr("name", "_Acts[" + counter + "].Photo_Path");
        counter++;
    })
}

function AddObsrTM() {
    let DrpID = "TM" + 1;
    let html = "";
    html += '<tr><td><div class="form-group"><input type="hidden" class="Drill_CRR_ID"  name="_Acts[' + ObsrCount + '].Drill_CRR_ID" value="0"/><textarea class="form-control Imp_Action required" style="resize:vertical;height:17px" name="_Acts[' + ObsrCount + '].Action_Des"></textarea><span data-valmsg-replace="true" data-valmsg-for="_Acts[' + ObsrCount + '].Action_Des" class="field-validation-valid text-danger V_Imp_Action"></span></div></td>';
    html += '<td><button type="button" class="btn btn-danger DelBtn">Remove</button></td></tr>';
    $(UI_Fields.TBODY_TASK_OBSR).append(html);
    ObsrCount++;
    /* $('#'+DrpID).append(newDropdown);*/
    /*ApplySelect2(".TM_NAME");*/
}

function ResetObsrValues() {
    let counter = 0;
    $("#tbl_TeamMem tbody > tr").each(function () {
        let ASSIGN_EMP = "ASSIGN_EMP" + counter;
        $(this).closest('tr').find('.Drill_CRR_ID').attr("name", "_Acts[" + counter + "].Drill_CRR_ID");
        $(this).closest('tr').find('.Imp_Action').attr("name", "_Acts[" + counter + "].Action_Des");
        $(this).closest('tr').find('.V_Imp_Action').attr("data-valmsg-for", "_Acts[" + counter + "].Action_Des");
        counter++;
    })
}

function Approve(btn) {
    btn.prop('disabled', true);
    let val = btn.val();
    let Drill_ID = $(UI_Fields.MAIN_DRILL_ID).val();
    //let token = GetformToken(UI_Fields.DRILL_ASSIGN_FRM);
    let token = "";
    let model;
    let url;
    let submit = true;
    switch (val) {
        case "1":
            url = '/DrillSchedule/Drill_Update_Status';
            model = {
                Drill_Schedule_ID: Drill_ID,
                Status: "5"
            };
            break;

        case "2":
            submit = false;
            $(UI_Fields.REJECT_POP).modal('show');
            break;
        case "3":
            url = '/DrillSchedule/Drill_Add_Review';
            let ACT = [];
            let rowCount = $('#tblTaskAssign tbody > tr.NewActions').length;
            //let valid = $(UI_Fields.FRM_TASK_ASSIGN).valid();
            if (rowCount > 0) {
                $('#tblTaskAssign tbody > tr.NewActions').each(function () {
                    let Action = $(this).closest('tr').find('.NewAct').val();
                    if (!Action) {
                        ToastrMessage("error", "Enter the Action Description!");
                        submit = false;
                        return false;
                    }
                    let data = {};
                    data.Action_Des = Action;
                    data.Remarks = "3";
                    data.Drill_CRR_ID = "0";
                    ACT.push(data);
                });
            }
            model = {
                Drill_Schedule_ID: Drill_ID,
                Status: "7",
                _Acts: ACT
            };
            break;
        case "4":
            url = '/DrillSchedule/Drill_Update_Status';
            model = {
                Drill_Schedule_ID: Drill_ID,
                Status: "10"
            };
            break;
        default:
            btn.prop('disabled', false);
            return false;
            break;
    }
    if (submit) {

        AjaxAsynT(url, { drill_: model }, token, function (resp) {
            if (resp.STATUS_CODE == "200") {
                LOAD_GRID();
            }
            else {
                ToastrMessage("error", "Error While processing your request.");
            }
        });
    }
    else {
        btn.prop('disabled', false);
        return false;
    }

}
//Action
function AddNewAction() {
    let html = "";
    html += '<tr class="NewActions"><td colspan="4"><div class="form-group"><textarea class="form-control NewAct required" style="resize:vertical;height:17px"></textarea></div></td>';
    html += '<td><button type="button" class="btn btn-danger DelBtnAct">Remove</button></td></tr>';
    $(UI_Fields.TBODY_TASK_ASSIGN).append(html);
}

function ZMSubmit() {
    let ACT = [];
    let token = GetformToken(UI_Fields.FRM_SCH_TASK_ASSIGN);
    let valid = $(UI_Fields.FRM_SCH_TASK_ASSIGN).valid();
    let Drill_ID = $(UI_Fields.MAIN_DRILL_ID).val();
    if (valid) {
        $('#tblTaskAssignZS tbody > tr.NewActions').each(function () {
            
            let v_Drill_CRR_ID = $(this).closest('tr').find('.Drill_CRR_ID').val();
            let v_Assign_Mem = $(this).closest('tr').find('.Assign_Mem').val();
            let v_Target_Date = $(this).closest('tr').find('.Assign_Date').val();
            let v_Assign_Type = $(this).closest('tr').find('.Assignee_Type').val();
            let data = {};
            data.Drill_CRR_ID = v_Drill_CRR_ID;
            data.Assignee_Id = v_Assign_Mem;
            data.Target_Date = v_Target_Date;
            data.Assign_Type = v_Assign_Type;
            ACT.push(data);
        });
        let model = {
            Drill_Schedule_ID: Drill_ID,
            Status: "8",
            _Acts: ACT
        };
        AjaxAsynT("/DrillSchedule/Drill_Add_Assignees", { drill_: model }, token, function (resp) {
            if (resp.STATUS_CODE == "200") {
                LOAD_GRID();
            }
            else {
                ToastrMessage("error", "Error While processing your request.");
            }
        });
    }
}
function Compare(ST, END) {
    let startTime = $(ST).val();
    let endTime = $(END).val();
    if (startTime && endTime) {
        let startDate = new Date('2000-01-01 ' + startTime);
        let endDate = new Date('2000-01-01 ' + endTime);
        if (startDate >= endDate) {
            ToastrMessage("error", "Invalid Time Selection!");
            $(ST).val('');
            $(END).val('');
        }
    }
}

function ApplyFileUploadConfig(id, IntPreview, IntConfig, urlid, type) {
    id.fileinput({
        allowedFileExtensions: type,
        showUpload: false,
        showRemove: true,
        showPreview: true,
        dropZoneTitle: 'Drag & drop or',
        dropZoneClickTitle: '<br>Click here to select files',
        minFileCount: 0,
        maxFileCount: 5,
        autoReplace: true,
        autoOrientImage: true,
        maxFileSize: 30720,
        //initialPreviewCount: 3,
        browseOnZoneClick: true,
        initialPreviewAsData: true,
        overwriteInitial: false,
        initialPreview: IntPreview,
        initialPreviewConfig: IntConfig,
        uploadExtraData: { img_key: 1 }
    }).on("filebatchselected", function (event, data) {
        let totalFileSize = 0;
        $.each(data, function (index, file) {
            totalFileSize += file.size;
        });
        if (totalFileSize > 30720000) {
            toastr["error"]("Total file size must not exceed 30 MB");
            id.fileinput('clear');
            return false;
        }
    }).on("filebeforedelete", function (event, key) {

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning", showCancelButton: !0,
            confirmButtonColor: "#34c38f",
            cancelButtonColor: "#f46a6a",
            confirmButtonText: "Yes, delete it!"
        }).then(function (t) {
            if (t.isConfirmed) {
                let keyParts = key.split(',');
                if (keyParts.length == 2) {
                    let Uid = parseInt(keyParts[0]);
                    let k = parseInt(keyParts[1]);
                    AjaxAsynWT(urlid, { Key: Uid }, function (resp) {
                        if (resp.STATUS_CODE == "200") {
                            Swal.fire("Deleted!", "Your file has been deleted.", "success");
                            id.fileinput('destroy');
                            IntPreview.splice(k, 1);
                            IntConfig.splice(k, 1);
                            ApplyFileUploadConfig(id, NM_Prev, NM_Config, urlid, AllowImg);
                        }
                        else {
                            Swal.fire("Failed!", "Something went wrong", "error");
                        }
                    });

                }
            }
        });
        return true;
    });

    //$(id).("filebeforedelete", function (event, data) {
    //    alert(data);
    //    return true;
    //});

}


function DelPhotoList(data, id, urlid, delurl) {
    $.ajax({
        url: delurl,
        type: "POST",
        cache: false,
        async: false,
        data: { ID: data },
        dataType: "json",
        success: function (data) {

            if (data.Status == "200") {
                NM_Prev = [];
                NM_Config = [];
                id.fileinput('destroy');
                if (data.Get_ById != null) {
                    $(data.Get_ById).each(function (i, e) {

                        if (urlid == 1) {
                            NM_Prev.push(e.File_Path);
                            NM_Config.push({
                                caption: "Image " + (i + 1),
                                filetype: "image/png",
                                url: e.File_Path,
                                width: "120px",
                                downloadUrl: false,
                                type: "image",
                                key: parseInt(e.Inc_Outcome_Photo_Id)
                            });
                        }
                        else {
                            NM_Prev.push(e.File_Path);
                            NM_Config.push({
                                caption: "Image " + (i + 1),
                                filetype: "image/png",
                                url: e.File_Path,
                                width: "120px",
                                downloadUrl: false,
                                type: "image",
                                key: parseInt(e.Inc_Inve_Photo_Id)
                            });

                        }
                    });
                }
                ApplyFileUpload(id, NM_Prev, NM_Config, urlid, AllowImg);
                Swal.fire("Deleted!", "Your file has been deleted.", "success");
            }
            else {
                Swal.fire("Failed!", "Something went wrong", "error");
            }
        },
    });

}