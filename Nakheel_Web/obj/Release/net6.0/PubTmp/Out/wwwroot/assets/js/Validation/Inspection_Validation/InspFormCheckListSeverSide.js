function ServerSideTable_HandOverBuilding() {
    debugger
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/HandOverInspection/GetAll_HandOver_Building',
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
                    //{ data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'HandOver_Type', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Date_of_Audit', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                ];
                return JSON.stringify(d);
                debugger;
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        order: [[0, 'desc']],
        pageLength: 100,
        columns: [
            { data: 'Unique_Id' },
            { data: 'HandOver_Type' },
            //{ data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Date_of_Audit' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'HandOver Inspection Pending':
                            badge = 'badge bg-warning';
                            break;
                        case 'HandOver Finding Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'HandOver Finding Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'HandOver Assign Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-success';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-success';
                            break;
                        case 'HandOver Inspection Completed':
                            badge = 'badge bg-success';
                            break;
                        case 'HandOver Inspection Rejected':
                            badge = 'badge bg-danger';
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
                    if (data == "HandOver Inspection Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HOQns_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HOQns_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    //if (data == "HandOver Finding Pending") {
                    //    if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                    //        if (row.HandOver_Type == "HandOver Infrastructure") {
                    //            html += '<a onclick="Fn_Infra_Qn_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    //        }
                    //        else {
                    //            html += '<a onclick="Fn_HOQns_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    //        }
                    //    } else {
                    //        html += '<a onclick="Fn_HO_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                    //    }
                    //    html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    //}
                    if (data == "HandOver Finding Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Assign Action Pending") {
                        if (row.Role_Id == "7") {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Pending") {
                        if (row.Role_Id == "7") {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HO_CA_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HO_CA_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Inspection Completed") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HO_CA_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HO_CA_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Inspection Rejected") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HOQns_Edit(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-danger" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HOQns_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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
//function Fn_HO_Edit(val) {
//    $(UI_Fields.LIST_VIEW).hide(100);
//    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
//    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
//    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
//    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
//}
function Fn_HOQns_Edit(val) {
    //alert('Qns');
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    $(UI_Fields.VIEW_HANDOVER_QNS_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewBuildingQuestionnairs';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
}
//function Fn_HOQns_View(val) {
//    $(UI_Fields.LIST_VIEW).hide(100);
//    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
//    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
//    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
//    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
//    Hzurl = '/HandOverInspection/_ViewHndOverBldgQuestionnairs';
//    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
//    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
//}
function Fn_HOQns_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    $(UI_Fields.VIEW_HANDOVER_QNS_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHndOverBldgQuestionnairs';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
}
function Fn_HO_CA_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    $(UI_Fields.VIEW_HANDOVER_QNS_LIST).show(100);
    $(UI_Fields.VIEW_HANDOVER_CLOSURE_ACTION).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHndOverBldgQuestionnairs';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHandOver_Closure';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEW_HANDOVER_CLOSURE_LIST).load(Hzurl);
}
function Fn_Infra_Qn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    $(UI_Fields.VIEW_HANDOVER_QNS_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHandOverInfrastructureQns';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
}
function Fn_Insp_HandOver_Report(id, Unique_Id) {
    debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Insp_HandOver_Report", { Insp_HndOver_Building_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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
//Health & Safety Inspection
function ServerSideTable_HealthSafetyBuilding() {
    debugger
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/HandOverInspection/GetAll_HealthSafety_Inspection',
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
                    { data: 'Health_Safety_Type', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    //{ data: 'HandOver_Type', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Inspected_By_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Date_of_Inspection', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                ];
                return JSON.stringify(d);
                debugger;
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        order: [[0, 'desc']],
        pageLength: 100,
        columns: [
            { data: 'Unique_Id' },
            { data: 'Health_Safety_Type' },
            //{ data: 'HandOver_Type' },
            //{ data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Inspected_By_Name'},
            { data: 'Date_of_Inspection' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Health Safety Pending':
                            badge = 'badge bg-warning';
                            break;
                        case 'Finding Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Finding Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Assign Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Inspection Completed':
                            badge = 'badge bg-success';
                            break;
                        case 'Schedule Initiated':
                            badge = 'badge bg-warning';
                            break;
                        case 'Inspection Rejected':
                            badge = 'badge bg-danger';
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
                    if (data == "Health Safety Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HS_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Finding Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            if (row.Health_Safety_Type == "2") {
                                html += '<a onclick="Fn_HS_MC_Qn_Edit(' + row.Insp_HealthSafety_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Fn_HS_Qn_Edit(' + row.Insp_HealthSafety_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                        } else {
                            html += '<a onclick="Fn_HS_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Finding Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Assign Action Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Pending") {
                        if (row.Role_Id == "5" || row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Approval Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_Qn_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Inspection Completed") {
                        html += '<a onclick="Fn_HS_CA_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Schedule Initiated") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HS_Edit(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_HS_CA_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Inspection Rejected") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_HS_Edit(' + row.Insp_HealthSafety_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>'; 
                        } else {
                            html += '<a onclick="Fn_HS_CA_View(' + row.Insp_HealthSafety_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_HS_Report(' + row.Insp_HealthSafety_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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
function Fn_HS_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyMCQuestionnairs';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_HS_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetHealthSafetyQnsView';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_HS_Qn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyMCQuestionnairs';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_HS_MC_Qn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyMCQuestionnairs';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST).load(Hzurl);
}
function Fn_HS_Qn_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetHealthSafetyQnsView';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_HS_CA_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHEALTHSAFETY_BUILDING).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    $(UI_Fields.DIV_HEALTHSAFETY_CLOSURE_ACTION).show(100);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyBuilding';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetHealthSafetyQnsView';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_QNS_LIST_VIEW).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyClosureAction';
    Hzurl += '/?Insp_HealthSafety_Id=' + val;
    $(UI_Fields.VIEW_HEALTHSAFETY_CLOSURE_LIST).load(Hzurl);
}
function Fn_HS_Report(id, Unique_Id) {
    debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Insp_HealthSafety_Report", { Insp_HealthSafety_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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
//Service Provider Inspection
function ServerSideTable_Insp_ServiceProvider() {
    debugger
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/HandOverInspection/GetAll_ServiceProvider_Inspection',
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
                    { data: 'Service_Provider_Type', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    //{ data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Service_Provider_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Inspection_Date', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                ];
                return JSON.stringify(d);
                /*debugger;*/
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        order: [[0, 'desc']],
        pageLength: 100,
        columns: [
            { data: 'Unique_Id' },
            { data: 'Service_Provider_Type' },
            //{ data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Service_Provider_Id' },
            { data: 'Inspection_Date' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        //case 'Service Provider Pending':
                        //    badge = 'badge bg-warning';
                        //    break;
                        case 'Service Provider Finding Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Approval Pending By HSSE Team':
                            badge = 'badge bg-info';
                            break;
                        case 'Approval Pending By HSSE Manager':
                            badge = 'badge bg-info';
                            break;
                        case 'Assign Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Schedule Completed':
                            badge = 'badge bg-success';
                            break;
                        case 'Schedule Initiated':
                            badge = 'badge bg-warning';
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
                    //if (data == "Service Provider Pending") {
                    //    if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                    //        html += '<a onclick="Fn_SP_Edit(' + row.Insp_Service_Provider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    //    } else {
                    //        html += '<a onclick="Fn_SP_View(' + row.Insp_Service_Provider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                    //    }
                    //    html += '<a onclick="Fn_SP_Report(' + row.Insp_Service_Provider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    //}
                    if (data == "Service Provider Finding Pending") {
                        if (row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            if (row.Health_Safety_Type == "2") {
                                html += '<a onclick="Fn_SP_MC_Qn_Edit(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Fn_SP_Qn_Edit(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                        } else {
                            html += '<a onclick="Fn_SP_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Approval Pending By HSSE Team") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Approval Pending By HSSE Manager") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Assign Action Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Pending") {
                        if (row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Approval Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Service Report" class="text-info" data-bs-original-title="Service Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Schedule Completed") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_SP_CA_View(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_CA_View(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Schedule Initiated") {
                        if (row.Role_Id == "15" || row.Role_Id == "16" || row.Role_Id == "17") {
                            html += '<a onclick="Fn_SP_Qn_Edit(' + row.Insp_ServiceProvider_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_SP_Qn_Edit(' + row.Insp_ServiceProvider_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_SP_Report(' + row.Insp_ServiceProvider_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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
function Fn_SP_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
}
function Fn_SP_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetServiceProviderQnsView';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_QNS_LIST).load(Hzurl);
}
function Fn_SP_Qn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewServiceProviderCSQnsObs';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_QNS_LIST).load(Hzurl);
}
function Fn_SP_MC_Qn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHealthSafetyMCQuestionnairs';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_QNS_LIST).load(Hzurl);
}
function Fn_SP_Qn_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetServiceProviderQnsView';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_QNS_LIST).load(Hzurl);
}
function Fn_SP_CA_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_SERVICE_PROVIDER).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST).show(100);
    //$(UI_Fields.DIV_HEALTHSAFETY_CLOSURE_ACTION).show(100);
    Hzurl = '/HandOverInspection/_ViewServiceProviderSchedule';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetServiceProviderQnsView';
    Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    $(UI_Fields.VIEW_SERVICE_PROVIDER_QNS_LIST).load(Hzurl);
    //Hzurl = '/HandOverInspection/_ViewHealthSafetyClosureAction';
    //Hzurl += '/?Insp_ServiceProvider_Id=' + val;
    //$(UI_Fields.VIEW_HEALTHSAFETY_CLOSURE_LIST).load(Hzurl);
}
function Fn_SP_Report(id, Unique_Id) {
    debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Insp_ServiceProvider_Report", { Insp_ServiceProvider_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

//Fire & Life Safety Inspection
function ServerSideTable_Insp_FireLifeSafety() {
    debugger
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/HandOverInspection/GetAll_FireLifeSafety_Inspection',
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
                    //{ data: 'Remarks', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Community_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Access_Schedule', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Inspection_Date', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                ];
                return JSON.stringify(d);
                debugger;
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        order: [[0, 'desc']],
        pageLength: 100,
        columns: [
            { data: 'Unique_Id' },
            //{ data: 'Remarks' },
            { data: 'Business_Unit_Name' },
            { data: 'Zone_Name' },
            { data: 'Community_Name' },
            { data: 'Access_Schedule' },
            { data: 'Inspection_Date' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Fire & Life Safety Pending':
                            badge = 'badge bg-warning';
                            break;
                        case 'Finding Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Approval Pending By HSSE Manager':
                            badge = 'badge bg-info';
                            break;
                        case 'Assign Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Fire & Life Safety Completed':
                            badge = 'badge bg-success';
                            break;
                        case 'Schedule Initiated':
                            badge = 'badge bg-warning';
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
                    if (data == "Fire & Life Safety Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_FLQns_Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Finding Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_FLQns_Edit(' + row.Insp_Request_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    //if (data == "Approval Pending By HSSE Team") {
                    //    if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                    //        html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    //    } else {
                    //        html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                    //    }
                    //    html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    //}
                    if (data == "Approval Pending By HSSE Manager") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Assign Action Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Pending") {
                        html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Corrective Action Approval Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Fire & Life Safety Completed") {
                        html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Schedule Initiated") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="FnFLS_Edit(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_FLQns_View(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_FLReport(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Report View" class="text-info" data-bs-original-title="Report View" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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
function FnFLS_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_FIRELIFE_SAFETY).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewFireLifeSafetySchedule';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewFireLifeSafetyCSQuestionnairs';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_FLQns_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_FIRELIFE_SAFETY).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewFireLifeSafetySchedule';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewFireLifeSafetyCSQuestionnairs';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_FLQns_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEW_FIRELIFE_SAFETY).show(100);
    $(UI_Fields.DIV_QUESTIONNAIRE_LIST_VIEW).show(100);
    Hzurl = '/HandOverInspection/_ViewFireLifeSafetySchedule';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_LIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_GetFireLifeSafetyQnsView';
    Hzurl += '/?Insp_Request_Id=' + val;
    $(UI_Fields.VIEW_FIRELIFE_SAFETY_QNS_LIST_VIEW).load(Hzurl);
}
function Fn_FLReport(id, Unique_Id) {
    debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Insp_FireLifeSafety_Report", { Insp_Request_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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