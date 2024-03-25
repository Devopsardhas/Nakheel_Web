function Emer_Mitigation_ServerTable() {
    debugger
    let table = $(UI_Fields.MAINTABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/Emergencymitigation/GetAll_Emer_Mitigation',
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
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Community_name', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Building_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Emer_Title', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Zone_Name' },
            { data: 'Community_name' },
            { data: 'Building_Name' },
            { data: 'Emer_Title' },
            { data:'CreatedBy_Name'},
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {

                    let badge;
                    let Status;
                    switch (data) {
                        case 'Escalate To Bronze Team':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'Escalate To Silver Team':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;
                        case 'Escalate To Gold Team':
                            badge = 'badge bg-primary';
                            Status = 3;
                            break;
                        case 'Reviewed':
                            badge = 'badge bg-dark';
                            Status = 4;
                            break;
                        case 'Escalate To Bronze / Silver Team':
                            badge = 'badge bg-dark';
                            Status = 5;
                            break;
                    }
                    let st = (data == "Closed On Spot" || data == "Emergency Completed") ? "Security Incident Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Emer_Miti_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';


                }
            },
            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Escalate To Bronze Team") {
                        html += '<a onclick="Fn_View(' + row.Emer_Miti_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Csp_Reassign(' + row.CSP_Id + ')" href="javascript:void(0);" title="Re-Assign" class="text-danger" data-bs-original-title="Re-Assign" aria-label="Re-assign"> <i class="mdi mdi-sync font-size-20"></i></a>'
                    }
                    if (data == "Escalate To Silver Team") {
                        html += '<a onclick="Fn_View(' + row.Emer_Miti_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Escalate To Gold Team") {
                        html += '<a onclick="Fn_View(' + row.Emer_Miti_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Reviewed") {
                        html += '<a onclick="Fn_View(' + row.Emer_Miti_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Escalate To Bronze / Silver Team") {
                        html += '<a onclick="Fn_View(' + row.Emer_Miti_Id + ')"href="javascript:void(0);" title="Review" class="text-primary" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    return html;
                }
            }
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields.MAINTABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Rain Mitigation List',
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Rain Mitigation'
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
    debugger
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.EMER_MITI_VIEW).show(100);
    Hzurl = '/Emergencymitigation/ViewEmer_Mitigation';
    Hzurl += '/?Emer_Miti_Id=' + val;
    $(UI_Fields.EMER_MITI_LIST).load(Hzurl);
}


