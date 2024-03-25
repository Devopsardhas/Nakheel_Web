function Serversidetable_HotWork(Id) {
    let table = $(UI_Fields.MAINTABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/ControlOfWork/Get_All_Hot_Work',
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
                        html += '<a onclick="Fn_HW_Report_Req(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Hot Work Permit Report" class="text-info" data-bs-original-title="Hot Work Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                    }
                    if (data == "HSE Staff/HSE Team Approval Pending") {
                        html += '<a onclick="Fn_View_HotWork_Hse(' + row.CSP_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }
                    if (data == "Zone Supervisor Rejected") {
                        html += '<a onclick="Fn_Edit_Hot_Work(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "HSE Staff/HSE Team Approved") {
                        html += '<a onclick="Fn_View_HotWork_Hse(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        html += '<a onclick="Fn_HW_Report_Req(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Hot Work Permit Report" class="text-info" data-bs-original-title="Hot Work Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HSE Staff/HSE Team Rejected") {
                        html += '<a onclick="Fn_Edit_Hot_Work(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Sp uploaded,Waiting for Zone Supervisor Approval") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Renewal Approval Pending") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Zone Supervisor Verified,Renewal Approved") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Zone Supervisor Rejected the Renewal") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Zone Supervisor Rejected the Evidence") {
                        html += '<a onclick="Fn_Edit_Evidence_edit(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Waiting for HSE Staff/HSE Team Approval") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                    }

                    if (data == "Closed") {
                        html += '<a onclick="Fn_Evid_HW_Zone_View(' + row.CSP_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_HW_Report(' + row.CSP_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Hot Work Permit Report" class="text-info" data-bs-original-title="Hot Work Permit Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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
                title: 'Hot Work Permit List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Hot Work Permit'
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
    $(UI_Fields.HOT_WORK_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewHotWork';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_HOT_WORK_LIST).load(Hzurl);
}
function Fn_View_Confine(val) {

    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.HOT_WORK_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewHotWork';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_HOT_WORK_LIST).load(Hzurl);
}
function Fn_View_HotWork_Hse(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.HOT_WORK_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewHotWork';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_HOT_WORK_LIST).load(Hzurl);
}

function Fn_Evid_HW_Zone_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.HOT_WORK_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewHotWork';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_HOT_WORK_LIST).load(Hzurl);
}

function Fn_Edit_Evidence_edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.HOT_WORK_REQUEST_VIEW).show(100);
    Hzurl = '/ControlOfWork/_ViewHotWork';
    Hzurl += '/?CSP_Id=' + val;
    $(UI_Fields.VIEW_HOT_WORK_LIST).load(Hzurl);
}


function Fn_Edit_Hot_Work(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.ADD_HOT_WORK_PERMIT).show(100);
    $(UI_Fields.GOOGLE_MAP_API).show(100);
    Load_GoogleMaps();
    $.post("/ControlOfWork/_EditHotWork", { ID: val }, function (data) {
        if (data != null) {
            $("#Hot_Work_ID").val(data.CSP_Id);
            HW_Get_Edit_Business_Unit("#Hw_Business_Unit_Id", data.Business_Unit_Id);
            HW_Get_Edit_Zone_Unit("#Hw_Zone_Id", data.Zone_Id, data.Community_Id, data.Building_Id);
            HW_Get_Edit_DMS_No_Edit("#Contractor_Id", data.DMS_No_Id);
            HW_Get_Comp_Person_Edit("#C_Competent", data.Competent_Person, data.DMS_No_Id, "#Hw_Building_Id", data.Building_Id);        
            $("#C_Company_Name").val(data.Company_Name);
            $("#C_Tittle_No").val(data.Contrator_Title_No);        
            $("#C_Name").val(data.Name);
            $("#C_Email_Id").val(data.Email_Id);
            $("#C_Date_Time").val(data.Date_and_Time);
            $("#C_Description").val(data.Description_of_Work);
            $("#C_Additional_Pre").val(data.Additional_Precautions);
            $("#C_From_Date").val(data.Work_Duration_From_Date);
            $("#C_To_Date").val(data.Work_Duration_To_Date);
            $("#Con_Start").val(data.Contractor_Start_Date);
            $("#Con_End").val(data.Contractor_End_Date);

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
                $("#HW_Method_Pdf").show();
                $.each(data._Method_Statement_Files, function (i, e) {
                    $("#HW_Method_Pdf").append([
                        '<input type="hidden" value="' + e.Method_File_Id +'" class="Method_File_Id" />',
                        '<input type = "hidden" value = "' + e.Method_File_Path + '" class= "Method_File_Path" />',
                        '<a class="Method_Pdf_Files mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' +e.Method_File_Path+'" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Risk_Assess_Files != null) {
                $("#HW_Risk_Pdf").show();
                $.each(data._Risk_Assess_Files, function (i, e) {
                    $("#HW_Risk_Pdf").append([
                        '<input type="hidden" value="' + e.Risk_File_Id + '" class="Risk_File_Id" />',
                        '<input type = "hidden" value = "' + e.Risk_File_Path + '" class= "Risk_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Risk_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Emr_Plan_Files != null) {
                $("#HW_Emr_Pdf").show();
                $.each(data._Emr_Plan_Files, function (i, e) {
                    $("#HW_Emr_Pdf").append([
                        '<input type="hidden" value="' + e.Emr_File_Id + '" class="Emr_File_Id" />',
                        '<input type = "hidden" value = "' + e.Emr_File_Path + '" class= "Emr_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Emr_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._HSE_Plan_Files != null) {
                $("#HW_HSE_Pdf").show();
                $.each(data._HSE_Plan_Files, function (i, e) {
                    $("#HW_HSE_Pdf").append([
                        '<input type="hidden" value="' + e.Hse_File_Id + '" class="Hse_File_Id" />',
                        '<input type = "hidden" value = "' + e.Hse_File_Path + '" class= "Hse_File_Path" />',
                        '<a class="mdi mdi-file-pdf" style="color: red;font-size: 20px" href="' + e.Hse_File_Path + '" target="_blank"></a>'
                    ]);
                });
            }

            if (data._Staff_Comptency_Files != null) {
                $("#HW_Staff_Pdf").show();
                $.each(data._Staff_Comptency_Files, function (i, e) {
                    $("#HW_Staff_Pdf").append([
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


function Fn_HW_Report_Req(id, unique_Id) {
    $(".preloader").show();
    $(UI_Fields.LIST_VIEW).hide(100);
    let Csp_ID = parseInt(id);
    $.post("/ControlofWorkReport/Hot_Work_Request_Report", { CSP_Id: Csp_ID, Unique_Id: unique_Id }, function (data) {
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

function Fn_HW_Report(id, unique_Id) {
    $(".preloader").show();
    $(UI_Fields.LIST_VIEW).hide(100);
    let Csp_ID = parseInt(id);
    $.post("/ControlofWorkReport/Hot_Work_Report", { CSP_Id: Csp_ID, Unique_Id: unique_Id }, function (data) {
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



