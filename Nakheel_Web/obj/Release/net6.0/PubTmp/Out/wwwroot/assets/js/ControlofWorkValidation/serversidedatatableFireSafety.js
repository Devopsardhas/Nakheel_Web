function Serversidetable_FireSafety(Id) {
    let table = $(UI_Fields.MAINTABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/ControlOfWork/Get_All_Fire_Safety_Request',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                d.CreatedBy = Id;
                let table = $(UI_Fields.MAINTABLE).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },

                ];
                return JSON.stringify(d);
            },

            dataSrc: function (json) {
                return json.data;
            }
        },

        order: [[0, 'desc']],
        pageLength: 100,
        columns: [
            { data: 'Unique_Id' },
            { data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'CreatedBy_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {

                    let badge;
                    let Status;
                    switch (data) {
                        case 'Zone Supervisor Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;

                        case 'HSE Staff/HSE Team Approval Pending':
                            badge = 'badge bg-primary';
                            Status = 2;
                            break;
                        case 'Zone Supervisor Rejected':
                            badge = 'badge bg-danger';
                            Status = 3;
                            break;
                        case 'HSE Staff/HSE Team Approved':
                            badge = 'badge  bg-success';
                            Status = 4;
                            break;
                        case 'HSE Staff/HSE Team Rejected':
                            badge = 'badge bg-danger';
                            Status = 5;
                            break;

                        case 'Sp uploaded,Waiting for Zone Supervisor Approval':
                            badge = 'badge bg-primary';
                            Status = 6;
                            break;

                        case 'Renewal Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 7;
                            break;

                        case 'Zone Supervisor Verified,Renewal Approved':
                            badge = 'badge bg-success';
                            Status = 8;
                            break;

                        case 'Zone Supervisor Rejected the Renewal':
                            badge = 'badge bg-danger';
                            Status = 9;
                            break;

                        case 'Closed':
                            badge = 'badge bg-success';
                            Status = 10;
                            break;

                        case 'Zone Supervisor Rejected the Evidence':
                            badge = 'badge bg-danger';
                            Status = 11;
                            break;

                        case 'Waiting for HSE Staff/HSE Team Approval':
                            badge = 'badge bg-warning';
                            Status = 12;
                            break;

                        case 'HSE Staff/HSE Team Rejected the Evidence':
                            badge = 'badge bg-danger';
                            Status = 13;
                            break;


                    }
                    let st = (data == "Closed On Spot" || data == "Observation Completed") ? "Observation Completed" : data;

                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.CSP_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';

                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Zone Supervisor Approval Pending") {
                        html += '<a onclick="Fn_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Csp_Reassign(' + row.CSP_Id + ')" href="javascript:void(0);" title="Re-Assign" class="text-danger" data-bs-original-title="Re-Assign" aria-label="Re-assign"> <i class="mdi mdi-sync font-size-20"></i></a>'
                        html += '<a onclick="Fn_FS_Report_Req(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Fire & Safety Permit Report" class="text-info" data-bs-original-title="Confined Space Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                    }
                    if (data == "HSE Staff/HSE Team Approval Pending") {
                        html += '<a onclick="Fn_View_FireSafety_Hse(' + row.CSP_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }
                    if (data == "Zone Supervisor Rejected") {
                        html += '<a onclick="Fn_Edit_FireSafety(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "HSE Staff/HSE Team Approved") {
                        html += '<a onclick="Fn_View_FireSafety_Hse(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        html += '<a onclick="Fn_FS_Report_Req(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Fire & Safety Permit Report" class="text-info" data-bs-original-title="Confined Space Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HSE Staff/HSE Team Rejected") {
                        html += '<a onclick="Fn_Edit_FireSafety(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Sp uploaded,Waiting for Zone Supervisor Approval") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Renewal Approval Pending") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Zone Supervisor Verified,Renewal Approved") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Zone Supervisor Rejected the Renewal") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Zone Supervisor Rejected the Evidence") {
                        html += '<a onclick="Fn_Edit_Evidence_edit(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Waiting for HSE Staff/HSE Team Approval") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Closed") {
                        html += '<a onclick="Fn_Evidence_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_FS_Report(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Fire & Safety Permit Report" class="text-info" data-bs-original-title="Confined Space Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }

                    if (data == "HSE Staff/HSE Team Rejected the Evidence") {
                        html += '<a onclick="Fn_Edit_Evidence_edit(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    return html;
                }
            },

        ],
        "initComplete": function (settings, json) {

            $(UI_Fields.MAINTABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
        },
        dom: 'lBfrtip',
        "columnDefs": [
            {
                "targets": [], // index of columns to be hidden initially
                "visible": false,
                "searchable": false
            },
        ],
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel',
                title: 'Fire Safety Permit List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Fire Safety Permit'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function Fn_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.FIRE_SAFETY_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewFireSafety';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_FIRE_SAFETY_LIST).load(Hzurl);
}
function Fn_View_Confine(val) {

    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.CONFINED_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewConfinedSpace';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_CONFINED_LIST).load(Hzurl);
}
function Fn_View_FireSafety_Hse(val) {

    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.FIRE_SAFETY_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewFireSafety';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_FIRE_SAFETY_LIST).load(Hzurl);

}

function Fn_Evidence_Zone_View(val) {

    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.FIRE_SAFETY_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewFireSafety';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_FIRE_SAFETY_LIST).load(Hzurl);

}

function Fn_Edit_Evidence_edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.FIRE_SAFETY_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewFireSafety';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_FIRE_SAFETY_LIST).load(Hzurl);
}

function Fn_Edit_FireSafety(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.ADD_FIRESAFETY_PERMIT).show(100);
    $(UI_Fields.GOOGLE_MAP_API).show(100);
    //Load_GoogleMaps();
    $.post("/ControlOfWork/_Edit_Fire_Safety", { ID: val }, function (data) {
        if (data != null) {
            $("#FS_FireSafe_Id").val(data.CSP_Id);
            FS_Get_Edit_Business_Unit("#FS_Business_Unit_Id", data.Business_Unit_Id);
            FS_Get_Edit_Zone_Unit("#FS_Zone_Id", data.Zone_Id, data.Community_Id, data.Building_Id);
            FS_Get_Edit_DMS_No_Edit("#FireSaFeDms_Id", data.DMS_No_Id);
            FS_Get_Comp_Person_Edit("#FS_Competent", data.Competent_Person, data.DMS_No_Id, "#FS_Building_Id", data.Building_Id);
            $("#FS_Company_Name").val(data.Company_Name);
            $("#FS_Tittle_No").val(data.Contrator_Title_No);
            $("#FS_Isolation").val(data.Isolation_Impair_Coordinate);
            $("#Fire_Safe_Name").val(data.Name);
            $("#Fire_Email_Id").val(data.Email_Id);
            $("#FS_Date_Time").val(data.Date_and_Time);
            $("#FS_Description").val(data.Description_of_Work);
            $("input[name=Isolation][value=" + data.Degree_Isolation + "]").attr('checked', 'checked');
            if (data.Fire_Alarm_Chk != null) {
                $("#Fire_Alarm_Id").val(data.Fire_Alarm_Chk).attr('checked', 'checked');
            }
            
            if (data.Fire_Protection_Chk != null) {
                $("#Fire_Pro_Id").val(data.Fire_Protection_Chk).attr('checked', 'checked');
            }
            if (data.PAVA_Chk != null) {
                $("#Pava_Id").val(data.PAVA_Chk).attr('checked', 'checked');
            }
            $("#During_Work_Hours").val(data.During_Work_Hrs);
            $("#Out_Work_Hours").val(data.Out_Work_Hrs);
            $("#Alt_Fire_Fight").val(data.alter_fire_fight_option);
            $("#Fire_Watcher_Details").val(data.Fire_watcher_Detail);
            $("#FS_Reason_Isolation").val(data.Reason_Isolation);

            $("#FS_Additional").val(data.Additional_Precautions);
            $("#FS_Start_Date").val(data.Work_Duration_From_Date);
            $("#FS_End_Date").val(data.Work_Duration_To_Date);
            $("#FS_Con_Start_Date").val(data.Contractor_Start_Date);
            $("#FS_Con_End_Date").val(data.Contractor_End_Date);

            $("#Comp_Number").val(data.Competent_Number);
            $("#Hse_Name").val(data.HSE_Officer_Name);
            $("#Hse_Number").val(data.HSE_Mobile_Number);
            $("#Accept_Name").val(data.Declare_Name);
            $("#Accept_Desig").val(data.Declare_Designation);
            $("input[name=Declare_Chk][value=" + data.Declare_Chk + "]").attr('checked', 'checked');

            $("#Method_files").removeAttr('required');
            $("#Method_files").removeAttr('name');

            $("#Risk_files").removeAttr('required');
            $("#Risk_files").removeAttr('name');

            $("#Emr_files").removeAttr('required');
            $("#Emr_files").removeAttr('name');

            $("#Hse_files").removeAttr('required');
            $("#Hse_files").removeAttr('name');

            $("#Competencyfiles").removeAttr('required');
            $("#Competencyfiles").removeAttr('name');

            $("#Signed_files").removeAttr('required');
            $("#Signed_files").removeAttr('name');

            var count = 1;
            $.each(data._Add_CSP_Ques, function (key, value) {
                var varYes = $('#Check' + count).attr('name');
                var varRemark = $('#Remarks' + count).attr('name');

                $("input[name=" + varYes + "][value=" + value.CSP_Ques_Radio_Btn + "]").prop('checked', true);
                $("#" + varRemark).val(value.CSP_Remarks);
                if (value.CSP_Ques_Radio_Btn == "No" || value.CSP_Ques_Radio_Btn == "NA") {
                    $("#Remarks" + count).show();
                }
                else {
                    $("#Remarks" + count).hide();
                }
                count++;


            });
            $("#Check").change(function () {
                $("#Remarks").val("");
            });


            if (data.length != 0) {
                $("#Reject_Reason").show();
                $.each(data._Get_Zone_Reject_List, function (key, value) {
                    $("#Reject_Reason_List").append([
                        '<tr>',
                        '<td>' + '<lable>' + value.Unique_Id + ' </lable>' + '</td>',
                        '<td>' + '<lable>' + value.Remarks + ' </lable>' + '</td>',
                        '<td>' + '<lable>' + value.Zone_Approver_Name + ' </lable>' + '</td>',
                        '</tr>'
                    ].join(''));
                });
            }
            else {
                $("#Reject_Reason").hide();
            }

            if (data._Method_Statement_Files != null) {
                $("#FS_Method_Pdf").show();
                $.each(data._Method_Statement_Files, function (i, e) {
                    $("#FS_Method_Pdf").append([
                        '<input type="hidden" value="' + e.Method_File_Id + '" class="Method_File_Id" />',
                        '<input type = "hidden" value = "' + e.Method_File_Path + '" class= "Method_File_Path" />',
                        '<a class="Method_Pdf_Files mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Method_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Risk_Assess_Files != null) {
                $("#FS_Risk_Pdf").show();
                $.each(data._Risk_Assess_Files, function (i, e) {
                    $("#FS_Risk_Pdf").append([
                        '<input type="hidden" value="' + e.Risk_File_Id + '" class="Risk_File_Id" />',
                        '<input type = "hidden" value = "' + e.Risk_File_Path + '" class= "Risk_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Risk_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Emr_Plan_Files != null) {
                $("#FS_Emr_Pdf").show();
                $.each(data._Emr_Plan_Files, function (i, e) {
                    $("#FS_Emr_Pdf").append([
                        '<input type="hidden" value="' + e.Emr_File_Id + '" class="Emr_File_Id" />',
                        '<input type = "hidden" value = "' + e.Emr_File_Path + '" class= "Emr_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Emr_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._HSE_Plan_Files != null) {
                $("#FS_HSE_Pdf").show();
                $.each(data._HSE_Plan_Files, function (i, e) {
                    $("#FS_HSE_Pdf").append([
                        '<input type="hidden" value="' + e.Hse_File_Id + '" class="Hse_File_Id" />',
                        '<input type = "hidden" value = "' + e.Hse_File_Path + '" class= "Hse_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Hse_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Staff_Comptency_Files != null) {
                $("#FS_Staff_Pdf").show();
                $.each(data._Staff_Comptency_Files, function (i, e) {
                    $("#FS_Staff_Pdf").append([
                        '<input type="hidden" value="' + e.File_Id + '" class="File_Id" />',
                        '<input type = "hidden" value = "' + e.File_Path + '" class="File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Sp_Signed_Files != null) {
                $("#Signed_Pdf").show();
                $.each(data._Sp_Signed_Files, function (i, e) {
                    $("#Signed_Pdf").append([
                        '<input type="hidden" value="' + e.Sign_File_Id + '" class="File_Id" />',
                        '<input type = "hidden" value = "' + e.Sign_File_Path + '" class="File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Sign_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Area_Affected.length != 0) { 
                var count = 1;
                $("#GetAresAffectList").empty();
                $.each(data._Area_Affected, function (key, value) {
                    var Areas_Txt = "AreaName" + count;
                    var html = "";
                    html += '<tr>'
                    html += '<td><input type="text" class="form-control AreaName" name="AreaName" id="' + Areas_Txt + '" autocomplete="off" value="' + value.Extent_Area_Affected + '"/></td>'
                    if (count != "1") {
                        html += '<td><button type="button" class="btn btn-danger TblAreaAffected_Remove">Remove</button></td>'
                    }
                    html += '</tr>'
                    $("#tblAreaAffect").append(html);
                    count++;
                  
                });
            }
            else {
                $("#tblAreaAffect").hide();
            }

            if (data._Personnel_Affected.length != 0) {
                var count = 1;
                $("#GetPersonnelAffectList").empty();
                $.each(data._Personnel_Affected, function (key, value) {
                    var Personel_Txt = "Personnel_name" + Personnel_data;
                    var html = "";
                    html += '<tr>'
                    html += '<td><input type="text" class="form-control Personnel_name" id="' + Personel_Txt + '" name="Personnel_name" autocomplete="off" value="' + value.Extent_Personnel_Affected + '" /></td>'
                    if (count != "1") {
                        html += '<td><button type="button" class="btn btn-danger TblPersonnel_Remove">Remove</button></td>'
                    }
                    html += '</tr>'
                    $("#tblPersonnelAffect").append(html);
                    count++;

                });
            }
            else {
                $("#tblPersonnelAffect").hide();
            }

            if (data._Method_Isolation.length != 0) {
                $("#tblMethodAffect").show();
                var count = 1;
                $("#GetMethodList").empty();
                $.each(data._Method_Isolation, function (key, value) {
                    var Isolation_txt = "Isolation_name" + Isolation_data;
                    var html = "";
                    html += '<tr>'
                    html += '<td><input type="text" class="form-control Isolation_name" name="Isolation_name" id="' + Isolation_txt + '" autocomplete="off" value="' + value.Method_Isolation +'"/></td>'
                    if (count != "1") {
                        html += '<td><button type="button" class="btn btn-danger TblMethod_Remove">Remove</button></td>'
                    }
                    html += '</tr>'
                    $("#tblMethodAffect").append(html);
                    count++;

                });
            }
            else {
                $("#tblMethodAffect").hide();
            }
        }
        else {
            Swal.fire({
                position: 'top-end',
                icon: 'Error',
                title: 'Error',
                showConfirmButton: false,
                timer: 1500
            });
        }
    });
}



function Fn_FS_Report_Req(id, unique_Id) {
    $(".preloader").show();
    $(UI_Fields.LIST_VIEW).hide(100);
    let Csp_ID = parseInt(id);
    $.post("/ControlofWorkReport/Fire_Permit_Request_Report", { CSP_Id: Csp_ID, Unique_Id: unique_Id }, function (data) {
        if (data) {
            window.open(data);
        }
        else {
            toastr["error"]("Please try again");
        }
    }).always(function () {
        $(UI_Fields.LIST_VIEW).show(100);
        $(".preloader").hide();
    });
}

function Fn_FS_Report(id, unique_Id) {
    $(".preloader").show();
    $(UI_Fields.LIST_VIEW).hide(100);
    let Csp_ID = parseInt(id);
    $.post("/ControlofWorkReport/Fire_Safety_Report", { CSP_Id: Csp_ID, Unique_Id: unique_Id }, function (data) {
        if (data) {
            window.open(data);
        }
        else {
            toastr["error"]("Please try again");
        }
    }).always(function () {
        $(UI_Fields.LIST_VIEW).show(100);
        $(".preloader").hide();
    });
}

function Fn_Csp_Reassign(val) {

}





