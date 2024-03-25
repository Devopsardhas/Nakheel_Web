function ServerSideTable_Request() {
    let table = $(UI_Fields.MainTable).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/Insp_Request_GetAll',
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
                    { data: 'Insp_Type_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Inspection_Date', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(6)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead input:eq(7)').val() } },
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
                        case 'Request Approval Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Request Rejected':
                            badge = 'badge bg-danger';
                            break;
                        case 'Inspection Finding Pending':
                        case 'Inspection Finding Approval Pending':
                        case 'Scheduled Initiated':
                            badge = 'badge bg-info';
                            break;
                        case 'Inspection Finding Rejected':
                            badge = 'badge bg-danger';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Corrective Action Pending':
                            badge = 'badge bg-info';
                            break;
                        case 'Completed':
                            badge = 'badge bg-success';
                            break;
                        case 'Document Verified':
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
                    if (data == "Request Approval Pending") {
                        if (row.Insp_Type_Id == "Inspection") {
                            if (row.Insp_Category_Name == "Environmental") {
                                if (row.Role_Id == "10" || row.Role_Id == "12" || row.Role_Id == "13") {
                                    html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                                } else {
                                    html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                                }
                            }
                            else {
                                if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                    html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                                } else {
                                    html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                                }
                            }
                        } else {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }

                    }
                    if (data == "Request Rejected") {
                        if (row.Role_Id == "5" && row.Login_Id == row.CreatedBy) {
                            html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "Document Verified") {
                        html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Insp_Spot_Doc_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Inspection Finding Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                    }
                    if (data == "Scheduled Initiated") {
                        if (row.Access_Schedule != "All" && row.Access_Schedule != "NA") {
                            if (row.Login_Id == row.Access_Schedule) {
                                html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else {
                            if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                                html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        }
                    }
                    if (data == "Inspection Finding Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_Spot_Pre_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';

                    }
                    if (data == "Corrective Action Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_Spot_Pre_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Inspection Finding Rejected") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Edit(' + row.Insp_Request_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_Spot_Pre_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';

                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11" || row.Role_Id == "5") {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_Spot_Pre_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
                    }
                    if (data == "Completed") {
                        html += '<a onclick="Finding(' + row.Insp_Request_Id + ')" href="javascript:void(0);" title="View" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Insp_Spot_Report(' + row.Insp_Request_Id + ',' + "'" + row.Unique_Id + "'" + ')" style="cursor: pointer;"><i style="color: red;font-size: 18px" class="mdi mdi-file-pdf"></i></a>';
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

    //$('#MainTable thead tr').clone(true).addClass('filters').appendTo('#MainTable thead');

    //var table = $('#MainTable').DataTable({
    //    orderCellsTop: true,
    //    fixedHeader: true,
    //    initComplete: function () {
    //        var api = this.api();
    //        // For each column
    //        api.columns().eq(0).each(function (colIdx) {
    //            // Set the header cell to contain the input element
    //            var cell = $('.filters th').eq($(api.column(colIdx).header()).index());
    //            var title = $(cell).text();
    //            $(cell).html('<input type="text" placeholder="' + title + '" />');
    //            // On every keypress in this input
    //            $('input', $('.filters th').eq($(api.column(colIdx).header()).index()))
    //                .off('keyup change')
    //                .on('keyup change', function (e) {
    //                    e.stopPropagation();
    //                    // Get the search value
    //                    $(this).attr('title', $(this).val());
    //                    var regexr = '({search})'; //$(this).parents('th').find('select').val();
    //                    var cursorPosition = this.selectionStart;
    //                    // Search the column for that value
    //                    api
    //                        .column(colIdx)
    //                        .search((this.value != "") ? regexr.replace('{search}', '(((' + this.value + ')))') : "", this.value != "", this.value == "")
    //                        .draw();
    //                    $(this).focus()[0].setSelectionRange(cursorPosition, cursorPosition);
    //                });
    //        });
    //    }
    //});
}


function ServerSideTable_CA() {
    let table = $(UI_Fields.MainTableCA).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Inspection/Insp_Spot_CA_GetAll',
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
                    { data: 'Insp_Type_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'CreatedBy_Name', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'CreatedDate', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTableCA_wrapper').find('thead input:eq(6)').val() } },
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
            { data: 'Insp_Type_Name' },
            { data: 'CreatedBy_Name' },
            { data: 'CreatedDate' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    switch (data) {
                        case 'Action Closure Pending':
                            badge = 'badge bg-info';
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



function Fn_Insp_Spot_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/InspReport/Spot_Insp_Report", { Insp_Request_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

function Fn_Insp_Spot_Pre_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/InspReport/Spot_Pre_Insp_Report", { Insp_Request_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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
function Fn_Insp_Spot_Doc_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View_Div").hide();
    $.post("/InspReport/Insp_Spot_Document_Review_Report", { Insp_Request_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

