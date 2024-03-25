function BackBtn() {
    $(UI_Fields.LOADER).show();
    $(UI_Fields.DRILL_FRM_DIV).hide();
    $(UI_Fields.LIST_VIEW).show(100);
    $(UI_Fields.LOADER).hide();
}

function SubmitAssignee() {

    let token = GetformToken(UI_Fields.DRILL_ASSIGN_FRM);
    let Drill_ID = $(UI_Fields.ASSIGN_DS_ID).val();
    let varSP_DRP_ID = $(UI_Fields.SP_DRP).val();
    let varDC_DRP_ID = $(UI_Fields.DC_DRP).val();
    if (!varSP_DRP_ID) {
        ToastrMessage("error", "Please Select Service provider!");
        $(UI_Fields.SP_DRP).focus();
        return false;
    }
    if (!varDC_DRP_ID) {
        ToastrMessage("error", "Please Select Commander!");
        $(UI_Fields.DC_DRP).focus();
        return false;
    }
    let model = {
        Drill_Schedule_ID: Drill_ID,
        Commander: varDC_DRP_ID,
        Service_Provider: varSP_DRP_ID
    };
    AjaxAsynT('/DrillSchedule/Drill_Add_Assign', { drill_: model }, token, function (resp) {
        if (resp.STATUS_CODE == "200") {
            $(UI_Fields.POP_UP).modal('hide');
            LOAD_GRID();
        }
        else {
            ToastrMessage("error", "Error While processing your request.");
        }
    });
}

function LoadTable() {
    let table = $(UI_Fields.MAINTABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/DrillSchedule/Drill_Schedule_Get_All',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields.MAINTABLE).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    //{ data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    //{ data: 'Business_Unit_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    //{ data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                    // Add other column search properties as needed
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
            {
                data: 'Unique_ID', render: function (data) {
                    return '<lable class="Unique_Id">' + data +'</lable>'
                }
            },
            { data: 'Zone' },
            { data: 'Community' },
            { data: 'Building_Name' },
            { data: 'Drill_Type' },
            { data: 'Created_By' },
            { data: 'Commander' },
            { data: 'Service_Provider' },
            { data: 'Created_Date' },
            { data: 'Remarks'},
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case '1':
                            badge = 'badge bg-warning';
                            Status = 'Assign Action Pending';
                            break;
                        case '2':
                            badge = 'badge bg-info';
                            Status = 'Report Submission Pending';
                            break;
                        case '3':
                            badge = 'badge bg-danger';
                            Status = 'Rejected';
                            break;
                        case '4':
                            badge = 'badge bg-info';
                            Status = 'Zone Manager Verification Pending';
                            break;
                        case '5':
                            badge = 'badge bg-success';
                            Status = 'NCM Manager Recommendations Pending';
                            break;
                        case '6':
                            badge = 'badge bg-warning';
                            Status = 'Rejected Action';
                            break;
                        case '7':
                            badge = 'badge bg-warning';
                            Status = 'Zone Supervisor Action Assign Pending';
                            break;
                        case '8':
                            badge = 'badge bg-warning';
                            Status = 'Action Closure Pending';
                            break;
                        case '9':
                            badge = 'badge bg-warning';
                            Status = 'NCM Manager Verification Pending';
                            break;
                        case '10':
                            badge = 'badge bg-success';
                            Status = 'Completed';
                            break;
                        default:
                            break;

                    }
                    return '<a href="javascript:void(0);" class="' + badge + '">' + Status + '</a>';
                    //return '<a href="javascript:void(0);" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Hazard_Notification_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';

                }
            },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let html;
                    switch (data) {
                        case '1':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-primary AssignT" data-bs-original-title="Edit" aria-label="View"><i class="mdi mdi-account-hard-hat font-size-18"></i></a></div>';
                            break;
                        case '2':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-info DrillEdit" data-bs-original-title="Edit" aria-label="View"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '3':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-danger DrillEdit" data-bs-original-title="Edit" aria-label="View"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '4':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-info DrillEdit" data-bs-original-title="View" aria-label="View"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '5':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-success DrillEdit" data-bs-original-title="View" aria-label="View"><i class="mdi mdi-eye font-size-18"></i></a></div>';
                            break;
                        case '6':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-success DrillEdit" data-bs-original-title="Edit" aria-label="Edit"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '7':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-success DrillEdit" data-bs-original-title="Edit" aria-label="Edit"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '8':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-success DrillEdit" data-bs-original-title="Edit" aria-label="Edit"><i class="mdi mdi-pencil font-size-18"></i></a></div>';
                            break;
                        case '9':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-warning DrillEdit" data-bs-original-title="Edit" aria-label="Edit"><i class="mdi mdi-eye font-size-18"></i></a></div>';
                            break;
                        case '10':
                            html = '<div class="d-flex gap-3"><input class="Drill_ID" type="hidden" value="' + row.Drill_Schedule_ID + '"/><a href="javascript:void(0);" title="" class="text-success DrillEdit" data-bs-original-title="Edit" aria-label="Edit"><i class="mdi mdi-eye font-size-18"></i></a><a href="javascript:void(0);" style="cursor: pointer;" class="DrillReport"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a></div>';
                            break;
                        default:
                            break;

                    }
                    return html;

                }
            },

        ],
        "initComplete": function (settings, json) {
            $(UI_Fields.MAINTABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
            $(UI_Fields.LOADER).hide();
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
                title: 'Drill Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Drill Report'
            },
            //{
            //    extend: 'colvis',
            //    text: 'Show Columns',
            //    columns: ':not(.dt-head-hidden)'
            //}
        ]
    });
    //$('#MainTable_wrapper').find('thead th:eq(0)').append('<br><input type="text" placeholder="Search"/>');
    //$('#MainTable_wrapper').find('thead th:eq(1)').append('<br><input type="text" placeholder="Search"/>');
    //$('#MainTable_wrapper').find('thead th:eq(2)').append('<br><input type="text" placeholder="Search"/>');
    //$('#MainTable_wrapper').find('thead th:eq(5)').append('<br><input type="text" class="Status1" placeholder="Search"/>');
}