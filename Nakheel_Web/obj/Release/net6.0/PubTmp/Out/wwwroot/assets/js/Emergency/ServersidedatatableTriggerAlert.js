function Trigger_Alert_ServerTable(id) {
    let table = $(UI_Fields.TBL_TRIGGER_ALERT).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/TriggerAlert/Trigger_Alert_GetAll',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                d.CreatedBy = id;
                let table = $(UI_Fields.TBL_TRIGGER_ALERT).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'REF_NO', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Unique_ID', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Zone', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Community', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Mitigation', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Created_By', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#tbl_Trigger_Alert_wrapper').find('thead input:eq(6)').val() } },

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
            { data: 'REF_NO' },
            { data: 'Unique_ID' },
            { data: 'Zone' },
            { data: 'Community' },
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
                            Status = 'Alert Triggered';
                            break;  
                        case '2':
                            badge = 'badge bg-danger';
                            Status = 'Rejected';
                            break;  

                        case '3':
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
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Trigger_ID + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }    
                    if (data == "2") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Trigger_ID + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';

                    } 
                    if (data == "3") {
                        html += '<a onclick="Fn_Edit_Trigger(' + row.Trigger_ID + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Mitigation_Report(' + row.Trigger_ID + ',' + row.Remarks + ')" href="javascript:void(0);" title="Mitigation Rain Report" class="text-info" data-bs-original-title="Mitigation Rain Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                    }  
                    return html;
                }
            }
        ],
        "initComplete": function (settings, json) {

            $(UI_Fields.TBL_TRIGGER_ALERT).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Trigger Alert List',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Trigger Alert'
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
    $(UI_Fields.TRIGGER_ALERT_VIEW).show(100);
    $(UI_Fields.TRIGGER_CHK_VIEW).hide(100);
    $("#Mitigation_Report").hide(100);
    Hzurl = '/TriggerAlert/_View_Trigger_Alert';
    Hzurl += '/?Trigger_ID=' + val;
    $(UI_Fields.VIEW_TRIGGER_ALERT_LIST).load(Hzurl);
}

function Fn_Mitigation_Report(id,val) {
    $(UI_Fields.TRIGGER_ALERT_VIEW).hide(100);
    $(UI_Fields.LIST_VIEW).hide(100);
 
    $(UI_Fields.MITIGATION_REPORT_VIEW).show(100);
    Hzurl = '/TriggerAlert/_View_Mitigation_Report';
    Hzurl += '/?Trigger_ID=' + id + '&Remarks=' + val;
    $(UI_Fields.VIEW_MITIGATION_REPORT_LIST).load(Hzurl);
}