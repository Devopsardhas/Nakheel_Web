function ServerSideTable_SoftService() {
    debugger
    let table = $(UI_Fields.MainTable).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/SoftService_Inspection_GetAll',
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
                    { data: 'Inspection_Date', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Inspection_Date' },
            { data: 'CreatedBy_Name' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Corrective Action Pending':
                        case 'Action Closure Pending':
                        case 'Review Pending':
                        case 'TCOE Manager Approval Pending':
                        case 'TCOE Manager Review Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Completed':
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
                    if (data == "Corrective Action Pending" || data == "Review Pending") {
                        if (row.Role_Id == "20") {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_SoftService_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "20") {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_SoftService_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "TCOE Manager Approval Pending" || data == "TCOE Manager Review Pending") {
                        if (row.Role_Id == "8" || row.Role_Id == "7") {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_SoftService_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';

                    }
                    if (data == "Completed") {
                        html += '<a onclick="Landscape_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Insp_SoftService_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
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


function Fn_Insp_SoftService_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/InspReport/Insp_SoftService_Report", { Insp_Request_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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


