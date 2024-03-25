function Emer_Alert_ServerTable(id) {
    debugger;
    let table = $(UI_Fields.TBL_ALERT).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/EmergencyAlert/Emr_Alert_GetAll',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                d.CreatedBy = id;
                let table = $(UI_Fields.TBL_ALERT).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_ID', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Zone_Id', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community_Id', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Sub_Building_Id', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Created_By', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#tbl_Alert_wrapper').find('thead input:eq(6)').val() } },

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
            { data: 'Zone_Id' },
            { data: 'Community_Id' },
            { data: 'Sub_Building_Id' },
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
                            Status = 'Zone Supervisor Approval Pending';
                            break; 

                        case '2':
                            badge = 'badge bg-danger';
                            Status = 'Zone Supervisor Rejected';
                            break;    

                        case '3':
                            badge = 'badge bg-success';
                            Status = 'Zone Supervisor Approved';
                            break;    
                    }
                    return '<a href="javascript:void(0);" class="' + badge + '">' + Status + '</a>';
                    //let st = (data == "Closed On Spot" || data == "Emergency Completed") ? "Security Incident Completed" : data;
                    //return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Emer_Miti_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';


                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "1") {
                        html += '<a onclick="Fn_View(' + row.EMR_Alert_ID + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }     
                    if (data == "2") {
                        html += '<a onclick="Fn_View(' + row.EMR_Alert_ID + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }   
                    if (data == "3") {
                        html += '<a onclick="Fn_View(' + row.EMR_Alert_ID + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }   
                    return html;
                }
            }
        ],
        "initComplete": function (settings, json) {

            $(UI_Fields.TBL_ALERT).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Hot-Spot Identification List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Hot-Spot Identification'
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
    $(UI_Fields.MAIN_ADD_SPOT_DIV).hide(100);
    $(UI_Fields.EMR_ALERT_VIEW).show(100);
    Hzurl = '/EmergencyAlert/_View_Emr_Alert';
    Hzurl += '/?EMR_Alert_ID=' + val;
    $(UI_Fields.VIEW_EMR_ALERT_LIST).load(Hzurl);

}