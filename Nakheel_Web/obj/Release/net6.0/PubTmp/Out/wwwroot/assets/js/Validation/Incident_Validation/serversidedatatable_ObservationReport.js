function ServerSideTable_Observation() {
    debugger;
    let table = $(UI_Fields.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,

        ajax: {
            url: '/Incident/GetObservationReporting',
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
                    { data: 'Community_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    //{ data: 'Business Unit', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    //{ data: 'Category', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Observation_Type', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Last_Reported_By', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
                    //{ data: 'Action', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
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
            { data: 'Unique_Id' },
            { data: 'Zone_Name' },
            { data: 'Community_Id' },
            //{ data: 'Business_Unit_Id' },
            //{ data: 'Category' },
            { data: 'Observation_Type' },
            { data: 'Last_Reported_By' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case 'Observation Pending':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'Observation Action Closure Pending':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;
                        case 'Observation Action Approval Pending':
                            badge = 'badge bg-info';
                            Status = 3;
                            break;
                        case 'Closure Action Approval Pending':
                            badge = 'badge bg-info';
                            Status = 4;
                            break;
                        case 'Observation Completed':
                            badge = 'badge bg-success';
                            Status = 5;
                            break;
                        case 'Action Closure Closed By Zone':
                            badge = 'badge bg-info';
                            Status = 6;
                            break;
                        case 'Observation Rejected':
                            badge = 'badge bg-danger';
                            Status = 7;
                            break;
                        case 'Observation Closed On Spot':
                            badge = 'badge bg-info';
                            Status = 7;
                            break;
                        default:
                            break;

                    }
                    let st = (data == "Closed On Spot" || data == "Observation Completed") ? "Observation Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Inc_Obser_Report_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';
                }
            },

            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    debugger;
                    if (data == "Observation Pending") {
                        html += '<a onclick="Fn_Edit(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="Review" style="cursor: pointer;display:none"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Delete(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Delete" class="text-danger" data-bs-original-title="Delete" aria-label="Delete" style="cursor: pointer;display:none">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-delete font-size-18"></i></a>';
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            if (row.Observation_Type == "Health & Safety") {
                                html += '<a onclick="Fn_Edit(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else if (row.Role_Id == "12" || row.Role_Id == "13") {
                            if (row.Observation_Type == "Environment") {
                                html += '<a onclick="Fn_Edit(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else if (row.Role_Id == "5"){
                            html += '<a onclick="Fn_Edit(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="Edit">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="View" aria-label="Review">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Observation Action Closure Pending") {
                        html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Observation Action Approval Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Closure Action Approval Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            if (row.Observation_Type == "Health & Safety") {
                                html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else if (row.Role_Id == "12" || row.Role_Id == "13") {
                            if (row.Observation_Type == "Environment") {
                                html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                            } else {
                                html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                            }
                        } else {
                            html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Closed By Zone") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Closure_Edit_Evd(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                            html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Observation Completed") {
                        html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-success" data-bs-original-title="View" aria-label="View">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Final Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Observation Rejected") {
                        html += '<a onclick="Fn_View(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-danger" data-bs-original-title="View" aria-label="View">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Rework(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Resubmit" class="text-success" data-bs-original-title="View" aria-label="View">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-reload font-size-18"></i></a>';
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Observation Closed On Spot") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11" || row.Role_Id == "12" || row.Role_Id == "13") {
                            html += '<a onclick="Fn_Positive(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="View" aria-label="View">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Fn_Positive(' + row.Inc_Obser_Report_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Observation_Report(' + row.Inc_Obser_Report_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    return html;
                }
            },
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
                title: 'Observation_Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Observation_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function Fn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.OBS_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewObservationReport';
    Hzurl += '/?Inc_Obser_Report_Id=' + val;
    $(UI_Fields.VIEW_OBSERVATION_LIST).load(Hzurl);
}
function Fn_Positive(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.OBS_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewObservationReport';
    Hzurl += '/?Inc_Obser_Report_Id=' + val;
    $(UI_Fields.VIEW_OBSERVATION_LIST).load(Hzurl);
}
function Fn_Delete(val) {
    alert(val)
}
function Fn_View(val) {
    //debugger;
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.OBS_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewObservationReport';
    Hzurl += '/?Inc_Obser_Report_Id=' + val;
    $(UI_Fields.VIEW_OBSERVATION_LIST).load(Hzurl);
    //alert(Hzurl)
    //Hzurl = '/Incident/_ViewSupervisorObservationReport';
    //Hzurl += '/?Inc_Obser_Report_Id=' + val;
    //$("#ViewIncidentList_Supervisor").load(Hzurl);
}
function Closure_Edit_Evd(val) {
    //debugger;
    //alert(val)
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.OBS_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewObservationReport';
    Hzurl += '/?Inc_Obser_Report_Id=' + val;
    $(UI_Fields.VIEW_OBSERVATION_LIST).load(Hzurl);
    //Hzurl = '/Incident/_ViewSupervisorObservationReport';
    //Hzurl += '/?Inc_Obser_Report_Id=' + val;
    //$(UI_Fields.VIEW_OBSERVATION_FINAL_LIST).load(Hzurl);
}
function Fn_Rework(val) {
    //debugger;
    //alert('Rework:' + ' ' + val)
    var Inc_Observaion_Id = val.toString();
    var Obj = {
        Inc_Obser_Report_Id: Inc_Observaion_Id
    };
    $.ajax({
        url: "/Incident/Edit_Observation_Reporting",
        type: "POST",
        cache: false,
        data: JSON.stringify(Obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            
            if (data != null) {
                $(UI_Fields.LIST_VIEW).hide(100);
                $(UI_Fields.ADD_INCIDENT_LIST).show(100);
                $(UI_Fields.REJECT_EVIDENCE).show(100);
                $("#Inc_Obser_Report_Id").val(data.Inc_Obser_Report_Id);
                $("#Company_Name").val(data.Company_Name);
                $("#Contact_Name").val(data.Contact_Name);

                $("#Obser_Date").val(data.Obser_Date); 
                $("#Obser_Time").val(data.Obser_Time);
                LOAD_ZONE_EDIT(data.Zone_Id, data.Community_Id, data.Building_Id);
                LOAD_BUSINESS_UNIT_EDIT(data.Business_Unit_Id);
                if (data.Business_Unit_Id == '1') {
                    $(UI_Fields.BUSINESS_UNIT_TYPE).show();
                    $("#Obs_Business_Unit_Type").val(data.Business_Unit_Type_Name);
                }
                $("input[name=Category][value=" + data.Category + "]").attr('checked', 'checked');
                $("input[name=Observation_Type][value=" + data.Observation_Type + "]").attr('checked', 'checked');
                
                if (data.Observation_Type == 'Health_Safety') {
                    LOAD_HEALTH_SAFETY_EDIT(data.L_Inc_Health_Observation_Type);
                }
                else if (data.Observation_Type == 'Environment') {
                    LOAD_ENVIRONMENT_EDIT(data.L_Inc_Environment_Observation_Type);
                }
                $("#Obser_Details").val(data.Obser_Details);

                $("#Lat").val(data.Loc_Latitude);
                $("#Long").val(data.Loc_Longitude);
                Location();

                $(UI_Fields.GOOGLE_MAP_API).hide();
                $("#map-canvas_view").show();

                $.each(data.L_Inc_Observation_Photos, function (key, value) {
                    var html = "";
                    html += '<div class="col-md-4">';
                    html += '<img src="' + value.Photo_File_Path +'" style="height:150px;width:150px" />';
                    html += '</div>';
                    $("#EditinputPhoto").append(html);
                });
                $.each(data.L_Inc_Observation_Videos, function (key, value) {
                    var html = "";
                    html += '<div class="col-md-4">';
                    html += '<iframe src="' + value.Video_File_Path + '">' + value.Video_File_Path + '</iframe>';
                    html += '</div>';
                    $("#EditinputVideo").append(html);
                    //alert(html)
                });

                $("#Description_Observation").val(data.Description_Observation);
                $("#Imm_Corrective_Actions").val(data.Imm_Corrective_Actions);
            }
            else {
                Swal.fire({
                    position: 'top-end',
                    icon: 'Error',
                    title: 'Error',
                    showConfirmButton: false,
                    timer: 1500
                });
            }
        },
    });
}
function Fn_Observation_Report(id, Unique_Id) {
    //debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Observation_Report", { Inc_Obser_Report_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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
