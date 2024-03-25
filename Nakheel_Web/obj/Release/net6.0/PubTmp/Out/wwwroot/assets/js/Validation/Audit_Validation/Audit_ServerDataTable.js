function AuditServerTable() {
    debugger;
    let table = $(UI_FIELDS.MAINTABLE_SERVICE_PROV).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Audit/Get_All_Audit_Schedule',
            type: 'POST',
            contentType: 'application/json',

            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_FIELDS.MAINTABLE_SERVICE_PROV).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Business_Unit_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Zone_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Community_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },

                ];
                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                return json.data;
            }
        },
        order: [[0, 'asc']],
        columns: [
            { data: 'Unique_Id' },
            { data: 'Zone_Id' },
            { data: 'Community_Id' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case 'Audit Scheduled':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'HSE Team Approval Pending':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;

                        default:
                            break;

                    }
                    let st = (data == "Closed On Spot" || data == "Audit Completed") ? "Audit Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();">' + st + '</a>';
                }
            },

            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    debugger;
                    if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                        if (data == "Audit Scheduled") {
                            html += '<a onclick="Service_Prov_Edit_Chk(' + row.Audit_Sch_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else if (data == "HSE Team Approval Pending") {
                            html += '<a  href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                    }
                    else {
                        html += '<a onclick="Service_Prov_Edit_Chk(' + row.Audit_Sch_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                   
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_FIELDS.MAINTABLE_SERVICE_PROV).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Audit_Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Audit_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function Service_Prov_Edit_Chk(Audit_Sch_Id) {
    $(UI_FIELDS.SERVICE_PROVIDER_LIST_VIEW).hide(100);
    $(UI_FIELDS.AUDIT_VIEW).show(100);
    let url = "";
    url = '/Audit/LoadAll_CheckList';
    url += '/?Audit_Sch_Id=' + Audit_Sch_Id;
    $(UI_FIELDS.LOAD_ALL_CHECKLISTS).load(url);
}

function AuditInternalAuditServerTable() {
    debugger
    let table = $(UI_FIELDS1.MAIN_TABLE_INTERNAL).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Audit/GetInternal_Aud_Schedule',
            type: 'POST',
            contentType: 'application/json',

            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_FIELDS1.MAIN_TABLE_INTERNAL).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Zone_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Community_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
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
            { data: 'Zone_Id' },
            { data: 'Community_Id' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case 'Scheduled Initiated':
                            badge = 'badge bg-warning';
                            Status = 25;
                            break;
                        case 'Internal Audit Schedule':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'NCR Form Pending':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;
                        case 'HSE Manger Approval Pending':
                            badge = 'badge bg-dark';
                            Status = 3;
                            break;
                        case 'NCR Action Pending':
                            badge = 'badge bg-secondary';
                            Status = 4;
                            break;
                        default:
                            break;

                    }
                    let st = (data == "Closed On Spot" || data == "Audit Completed") ? "Audit Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();">' + st + '</a>';
                }
            },

            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Scheduled Initiated") {
                        if (row.Lead_Auditor_Id == row.Login_Id) {
                            html += '<a onclick="Internal_AddNew(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Internal_AddNew(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "Internal Audit Schedule") {
                        if (row.UpdatedBy == row.Login_Id) {
                            html += '<a onclick="IAud_Edit_Chk(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="IAud_View_Chk_Sed(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "NCR Form Pending") {
                        if (row.UpdatedBy == row.Login_Id) {
                            html += '<a onclick="IAud_Edit_Chk_Approval(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else  {
                            html += '<a onclick="Internal_AddNew(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "HSE Manger Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="IAud_Edit_Chk_Approval_NCR(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="IAud_View_Chk(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "NCR Action Pending") {
                        /*if (row.Role_Id == "10") {*/
                            html += '<a onclick="IAud_Edit_Chk_NCR_Action(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        //}
                        //else {
                        //    html += '<a onclick="IAud_View_Chk(' + row.Internal_Audit_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //}
                    }
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_FIELDS1.MAIN_TABLE_INTERNAL).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Audit_Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Audit_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}



function IAud_Edit_Chk(Internal_Audit_Id) {
    debugger;
    $("#div_btn_IAud_Submit").show();
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAud';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
    LoadClause();
}


function IAud_View_Chk(Internal_Audit_Id) {
    debugger;
    $("#DivSubmitSD").hide();
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAud';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
    LoadChecklist(Internal_Audit_Id);
}

function IAud_Edit_Chk_Approval(Internal_Audit_Id) {
    debugger;
    LoadChecklist(Internal_Audit_Id);
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAud';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
}

function IAud_Edit_Chk_Approval_NCR(Internal_Audit_Id) {
    debugger;
    LoadNCRChecklist(Internal_Audit_Id);
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAudNcrForm';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
}

function IAud_Edit_Chk_NCR_Action(Internal_Audit_Id) {
    debugger;
    LoadNCRChecklist_Action(Internal_Audit_Id);
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAudNcrForm';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
}

function IAud_View_Chk_Sed(Internal_Audit_Id) {
    debugger;
    $(UI_FIELDS1.INTERNAL_LIST_VIEW).hide(100);
    $(UI_FIELDS1.INTERNAL_AUD_VIEW).show(100);
    let url = "";
    url = '/Audit/_ViewInternalAud';
    url += '/?Internal_Audit_Id=' + Internal_Audit_Id;
    $(UI_FIELDS1.VIEW_INTERNAL_AUDIT_LIST).load(url);
   // LoadChecklist(Internal_Audit_Id);
}


