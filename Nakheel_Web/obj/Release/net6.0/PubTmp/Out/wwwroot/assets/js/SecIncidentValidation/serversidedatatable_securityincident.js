function Security_Incident_ServerTable() {
    debugger
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/SecurityIncident/GetAll_SecIncident_Work',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields.MAIN_TABLE).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Building_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Sec_Inc_Category_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
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
        pageLength: 25,
        columns: [
            { data: 'Unique_Id' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Building_Name' },
            { data: 'Sec_Inc_Category_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {

                    let badge;
                    let Status;
                    switch (data) {
                        case 'Action Pending':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'Approval Pending':
                            badge = 'badge bg-primary';
                            Status = 2;
                            break;
                        case 'Incident Completed': 
                            badge = 'badge bg-success';
                            Status = 3;
                            break;
                    }
                    let st = (data == "Closed On Spot" || data == "Security Incident Completed") ? "Security Incident Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Sec_Inc_Report_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';
                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Action Pending") {
                        if (row.Role_Id == "14" || row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            html += '<a onclick="Fn_View(' + row.Sec_Inc_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else { 
                            html += '<a onclick="Fn_View(' + row.Sec_Inc_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SecurityIncWOA_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="No Action" class="text-success" data-bs-original-title="No Action" aria-label="Non Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        html += '<a onclick="Fn_SecurityInc_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Action" class="text-warning" data-bs-original-title="Action" aria-label="Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Approval Pending") {
                        if (row.Role_Id == "5" || row.Role_Id == "14" || row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            html += '<a onclick="Fn_View_Incident(' + row.Sec_Inc_Report_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View_Incident(' + row.Sec_Inc_Report_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SecurityIncWOA_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="No Action" class="text-success" data-bs-original-title="No Action" aria-label="Non Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        html += '<a onclick="Fn_SecurityInc_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Action" class="text-warning" data-bs-original-title="Action" aria-label="Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Incident Completed") {
                        html += '<a onclick="Fn_View_Incident(' + row.Sec_Inc_Report_Id + ')"href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_SecurityIncWOA_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="No Action" class="text-success" data-bs-original-title="No Action" aria-label="Non Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        html += '<a onclick="Fn_SecurityInc_Report(' + row.Sec_Inc_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Action" class="text-warning" data-bs-original-title="Action" aria-label="Action">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    return html;
                }
            }
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields.MAIN_TABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Security_Report',
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Security_Report'
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
    $(UI_Fields.SEC_INCIDENT_VIEW).show(100);
    Hzurl = '/SecurityIncident/ViewSecurityIncidentReport';
    Hzurl += '/?Sec_Inc_Report_Id=' + val;
    $(UI_Fields.VIEW_SECURITY_INC_LIST).load(Hzurl);
}
function Fn_View_Incident(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.SEC_INCIDENT_VIEW).show(100);
    Hzurl = '/SecurityIncident/ViewSecurityIncidentReport';
    Hzurl += '/?Sec_Inc_Report_Id=' + val;
    $(UI_Fields.VIEW_SECURITY_INC_LIST).load(Hzurl);
}
function Fn_SecurityIncWOA_Report(id, Unique_Id) {
    //alert("WOA");
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    var WOAUnique_Id = "WOA" + Unique_Id;
    $.post("/SecurityIncReport/SecurityIncidentWOA_Report", { Sec_Inc_Report_Id: NotiID, Unique_Id: WOAUnique_Id }, function (data) {
        if (data) {
            window.open(data);
        }
        else {
            toastr["error"]("Please try again");
        }
    }).always(function () {
        $("#List_View").show();
        $(".preloader").hide();
    });
}
function Fn_SecurityInc_Report(id, Unique_Id) {
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/SecurityIncReport/SecurityIncident_Report", { Sec_Inc_Report_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
        if (data) {
            window.open(data);
        }
        else {
            toastr["error"]("Please try again");
        }
    }).always(function () {
        $("#List_View").show();
        $(".preloader").hide();
    });
}