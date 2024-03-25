function Serversidetable_Env(Id) {
    debugger;
    let table = $(UI_Fields.MAINTABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/AuditEnv/GetAll_Env_Audit',
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
                    { data: 'Lead_Auditor_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Service_Provider_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Lead_Auditor_Name' },
            { data: 'Service_Provider_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {

                    let badge;
                    let Status;
                    switch (data) {
                        case 'Schedule Initiated':
                            badge = 'badge bg-info';
                            Status = 1;
                            break;
                        case 'Audit Scheduled':
                            badge = 'badge bg-primary';
                            Status = 2;
                            break;
                        case 'Checklist Submitted,HSE Manager Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 3;
                            break;
                        case 'HSE Manager Rejected the Assessment':
                            badge = 'badge bg-danger';
                            Status = 4;
                            break;
                        case 'HSSE Director Approval Pending':
                            badge = 'badge bg-success';
                            Status = 5;
                            break;
                        case 'HSSE Director Rejected':
                            badge = 'badge bg-danger';
                            Status = 6;
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-warning';
                            Status = 7;
                            break;
                        case 'Lead Auditor Approval Pending':
                            badge = 'badge bg-success';
                            Status = 8;
                            break;
                        case 'Lead Auditor Rejected':
                            badge = 'badge bg-danger';
                            Status = 9;
                            break;
                        case 'HSE Manager Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 10;
                            break;
                        case 'HSE Manager Rejected':
                            badge = 'badge bg-danger';
                            Status = 11;
                            break;
                        case 'Audit Closed':
                            badge = 'badge bg-success';
                            Status = 12;
                            break;

                    }
                    let st = (data == "Closed On Spot" || data == "Observation Completed") ? "Observation Completed" : data;

                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Env_Audit_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';

                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Schedule Initiated") {
                        if (row.Lead_Auditor_Id == "All") {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        else {
                            if (row.Login_Id == row.Lead_Auditor_Id) {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Audit Scheduled") {
                        if (row.Lead_Auditor_Id == "All") {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        else {
                            if (row.Login_Id == row.Lead_Auditor_Id) {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                    }
                    if (data == "Checklist Submitted,HSE Manager Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "HSE Manager Rejected the Assessment" || data == "HSSE Director Rejected") {
                        if (row.Access_Schedule == "All") {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else {
                            if (row.Login_Id == row.Access_Schedule) {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "HSSE Director Approval Pending") {
                        if (row.Role_Id == "9") {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                   
                    if (data == "Corrective Action Pending") {
                        if (row.Service_Provider_Id == row.Login_Id) {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "Lead Auditor Approval Pending") {
                        if (row.Lead_Auditor_Id == "All") {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                            } else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        else {
                            if (row.Login_Id == row.Lead_Auditor_Id) {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Lead Auditor Rejected" || data == "HSE Manager Rejected") {
                        if (row.Service_Provider_Id == row.Login_Id) {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "HSE Manager Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    //if (data == "HSE Manager Rejected") {
                    //    html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    //}
                    if (data == "Audit Closed") {
                        html += '<a onclick="Scheduled_View(' + row.Env_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Audit_Env_Report(' + row.Env_Audit_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
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
                title: 'Confined Space Permit List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Confined Space Permit'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function Scheduled_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_AUD_SCH_VIEW).show(100);
    Hzurl = '/AuditEnv/_ViewEnv_Aud_Sch';
    Hzurl += '/?Env_Audit_Id=' + val;
    $(UI_Fields.LOAD_AUD_SCH_VIEW).load(Hzurl);
}

function Fn_Audit_Env_Report(id, unique_Id) {
    $(".preloader").show();
    $(UI_Fields.LIST_VIEW).hide(100);
    let EnvAuditId = parseInt(id);
    $.post("/AuditEnvReport/Environmental_Audit_Report", { Env_Audit_Id: EnvAuditId, Unique_Id: unique_Id }, function (data) {
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



