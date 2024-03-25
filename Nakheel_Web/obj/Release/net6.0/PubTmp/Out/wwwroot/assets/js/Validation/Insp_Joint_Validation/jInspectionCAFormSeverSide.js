function ServerSideTable_Joint_CA() {
    let table = $(UI_Fields.MainTableCA).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/Insp_Joint_CA_GetAll',
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
                    { data: 'Community_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Insp_Type_Id', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Inspection_Date', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(6)').val() } },
                    { data: 'Status', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(7)').val() } },
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
            { data: 'Insp_Type_Id' },
            { data: 'Inspection_Date' },
            { data: 'CreatedBy_Name' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Action Closure Pending':
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
                    if (data == "Action Closure Pending") {
                        html += '<a onclick="Fn_Add_Action_Closure(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                    }
                    if (data == "Completed") {
                        html += '<a onclick="Fn_Add_Action_Closure(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="View" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
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

