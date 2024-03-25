function ServerSideTable_Request() {
    debugger
    let table = $(UI_Fields.MainTable).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/Complaint_Register_GetAll',
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
                    { data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Reported_By_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Reported_By_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Service Request Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Assessment Pending':
                        case 'Complaint Review Pending':
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Complaint Closed':
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
                    if (data == "Service Request Pending") {
                        if (row.Role_Id == "5" && row.Login_Id == row.CreatedBy) {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    else if (data == "Assessment Pending") {
                        if (row.Login_Id == row.Service_Provider_Id) {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    else if (data == "Complaint Review Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    else if (data == "Action Closure Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.If_Observation == "Yes") {
                            html += '<a onclick="Fn_Insp_Advisory_Notice_Report(' + row.Complaint_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:18px;cursor:pointer"></i></a>';
                        }
                    }
                    else if (data == "Complaint Closed") {
                        html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        if (row.If_Observation == "Yes") {
                            html += '<a onclick="Fn_Insp_Advisory_Notice_Report(' + row.Complaint_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:18px;cursor:pointer"></i></a>';
                        }
                    }
                    else {
                        html += '<a onclick="Finding(' + row.Complaint_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
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


function ServerSideTable_CA() {
    let table = $(UI_Fields.MainTableCA).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/Insp_Spot_CA_GetAll',
            type: 'POST',
            contentType: 'application/json',

            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields.MainTableCA).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Business_Unit_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Insp_Type_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Insp_Type_Name' },
            { data: 'CreatedBy_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
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
                    if (data == "Action Closure Pending") {
                        html += '<a onclick="Fn_Add_Action_Closure(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields.MainTableCA).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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



function Fn_Insp_Advisory_Notice_Report(id, Unique_Id) {
    debugger
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/InspReport/Pre_Advisory_Notice_Report", { Complaint_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

