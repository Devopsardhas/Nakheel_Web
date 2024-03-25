function ServerSideTable_Request() {
    let table = $(UI_Fields.MainTable).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/AuditNCMForm/Audit_NCM_Form_GetAll',
            type: 'POST',
            contentType: 'application/json',

            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields.MainTable).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Audit_Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Audit_Type_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Audit_Date', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead input:eq(7)').val() } },
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
            { data: 'Audit_Unique_Id' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Audit_Type_Name' },
            { data: 'Audit_Date' },
            { data: 'CreatedBy_Name' },
            {
                data: 'Status_Name',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Corrective Action Pending':
                        case 'HSE Manager Approval Pending':
                        case 'NCR Report Pending':
                        case 'HSSE Director Approval Pending':
                        case 'Lead Auditor Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'HSE Manager Rejected':
                        case 'HSSE Director Rejected':
                        case 'Lead Auditor Rejected':
                            badge = 'badge bg-danger';
                            break;
                        case 'NCR Report Closed':
                            badge = 'badge bg-success';
                            break;
                        default:
                            break;
                    }
                    return '<a href="#" class="' + badge + '" >' + data + '</a>';
                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "30") {
                        if (row.Service_Provider_Id == row.Login_Id) {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "25") {
                        if (row.CreatedBy == row.Login_Id) {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "1") {
                        if (row.CreatedBy == row.Login_Id) {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "9") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "2" || data == "10") {
                        if (row.Service_Provider_Id == row.Login_Id) {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "3") {
                        html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "5") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "6" || data == "8") {
                        if (row.CreatedBy == row.Login_Id) {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    else if (data == "7") {
                        if (row.Role_Id == "9") {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Edit(' + row.Am_Sp_NCR_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Audit_NCR_Report(' + row.Am_Sp_NCR_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields.MainTable).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
        },
        dom: 'lBfrtip',
        "columnDefs": [
            {
                "targets": [],
                "visible": false,
                "searchable": false
            },
        ],
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel',
                title: 'Request_Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Request_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}
function Fn_Audit_NCR_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/AuditReport/Non_Conformance_Report", { Am_Sp_NCR_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
        if (data) {
            window.open(data);
        }
        else {
            toastr["error"]("Please try again");
            $("#List_View_Div").show();
            $(".preloader").hide();
        }
    }).always(function () {
        $("#List_View_Div").show();
        $(".preloader").hide();
    });
}