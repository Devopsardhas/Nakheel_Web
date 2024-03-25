function Mitigation_Action_ServerTable(id) {
    debugger;
    let table = $(UI_Fields.TBL_MITIGATION).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/MigitationAction/Alert_Assignee_GetAll',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                d.CreatedBy = id;
                let table = $(UI_Fields.TBL_MITIGATION).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_ID ', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Zone', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Community', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Remarks', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Mitigation', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Created_By', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#tbl_Migitation_wrapper').find('thead input:eq(6)').val() } },

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
            { data: 'Unique_ID' },
            { data: 'Zone' },
            { data: 'Community' },
            { data: 'Remarks' },
            { data: 'Mitigation' },
            { data: 'Created_By'},
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {

                    let badge;
                    let Status;
                    switch (data) {
                        case '1':
                            badge = 'badge bg-warning';
                            Status = 'Service Provider Action Pending';
                            break;   
                        case '2':
                            badge = 'badge bg-warning';
                            Status = 'Service Provider Action Pending';
                            break;  

                        case '3':
                            badge = 'badge bg-info';
                            Status = 'Zone Supervisor Approval Pending';
                            break;   

                        case '4':
                            badge = 'badge bg-danger';
                            Status = 'Zone Supervisor Rejected';
                            break;   

                        case '5':
                            badge = 'badge bg-success';
                            Status = 'Completed';
                            break;  
                    }
                    return '<a href="javascript:void(0);" class="' + badge + '">' + Status + '</a>';

                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "1") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Alert_Task_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }      
                    if (data == "2") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Alert_Task_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }   
                    if (data == "3") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Alert_Task_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';

                    }
                    if (data == "4") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Alert_Task_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }
                    if (data == "5") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Alert_Task_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';

                    }
                    return html;
                }
            }
        ],
        "initComplete": function (settings, json) {

            $(UI_Fields.TBL_MITIGATION).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Mitigation Action List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Mitigation Action'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]

    });

   
}  
function Fn_Edit_Trigger(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.MITIGATION_VIEW).show(100);
    Hzurl = '/MigitationAction/_View_Mitigation_Action';
    Hzurl += '/?Alert_Task_Id=' + val;
    $(UI_Fields.VIEW_MITIGATION_LIST).load(Hzurl);

}