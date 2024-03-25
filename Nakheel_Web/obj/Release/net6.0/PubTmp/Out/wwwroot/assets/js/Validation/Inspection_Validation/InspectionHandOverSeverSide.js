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
                        case 'HandOver Assign Action Approved':
                            badge = 'badge bg-success';
                            break;
                        case 'Action Closure Pending':
                            badge = 'badge bg-success';
                            break;
                        case 'HandOver Inspection Completed':
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
                    if (data == "HandOver Inspection Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-warning" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Finding Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            if (row.HandOver_Type == "HandOver Infrastructure") {
                                html += '<a onclick="Fn_Infra_Qn_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                            else {
                                html += '<a onclick="Fn_Qn_Edit(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'Edit' + "'" + ',' + "'" + 'Access' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                            }
                        } else {
                            html += '<a onclick="Fn_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Finding Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_Qn_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Assign Action Pending") {
                        if (row.Role_Id == "7") {
                            html += '<a onclick="Fn_Qn_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-info" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Assign Action Approved") {
                        if (row.Role_Id == "7") {
                            html += '<a onclick="Fn_Qn_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Action Closure Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_CA_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_CA_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HandOver Inspection Completed") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_CA_View(' + row.Insp_HndOver_Building_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_CA_View(' + row.Insp_HndOver_Building_Id + ',' + "'" + 'View' + "'" + ',' + "'" + 'NoAccess' + "'" + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;"> <i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        html += '<a onclick="Fn_Insp_HandOver_Report(' + row.Insp_HndOver_Building_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Observation Report" class="text-info" data-bs-original-title="Observation Report" aria-label="Final Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
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

$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form
        $("#F_Handover_Building").validate({
            rules: {
                Business_Unit: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Master_Community_Id: {
                    required: true,
                },
                Zone_Emp: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                HandOver_Type: {
                    required: true,
                },
                //Service_Provider: {
                //    required: true,
                //    minlength: 1
                //},
            },
            messages: {
                Business_Unit: {
                    required: "Please Select Business Unit",
                },
                Community: {
                    required: "Please Select Community",
                },
                Master_Community_Id: {
                    required: "Please Select Master Community",
                },
                Zone_Emp: {
                    required: "Please Select Zone Representative",
                },
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Building_Id: {
                    required: "Please Select Building",
                },
                HandOver_Type: {
                    required: "Please Select Handover Type",
                },
                //Service_Provider: {
                //    required: "Please Select Service Provider",
                //},
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-10 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-10 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-10 text-danger');

                $("#Date_of_Audit-error").removeClass('mt-2');
                $("#Date_of_Audit-error").addClass('m-10 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-10 text-danger');

                $("#Zone_Emp-error").removeClass('mt-2');
                $("#Zone_Emp-error").addClass('m-10 text-danger');

                //$("#Service_Provider-error").removeClass('mt-2');
                //$("#Service_Provider-error").addClass('m-10 text-danger');

                $("#HandOver_Type-error").removeClass('mt-2');
                $("#HandOver_Type-error").addClass('m-10 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Obj = {
                    Insp_HndOver_Building_Id: $("#Insp_HndOver_Building_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Id: $("#Business_Unit").val(),
                    Community_Id: $("#Community_Id").val(),
                    Business_Unit_Type_Name: $("#Hnd_Business_Unit_Type").val(),
                    Company_Name: $("#Company_Name").val(),
                    Inspected_By_Name: $("#Inspected_By_Name").val(),
                    HandOver_Type: $("#HandOver_Type").val(),
                    HandOver_Type_List: $("#HandOver_Type_List").val(),
                    Date_of_Audit: $("#Date_of_Audit").val(),
                    Zone_Representative: $("#Zone_Emp").val(),
                    Service_Provider: $("#Service_Provider").val(),
                    Consultant_Name: $("#Consultant_Name").val(),
                    Consultant_MNo: $("#Consultant_MNo").val(),
                    Contractor_Name: $("#Contractor_Name").val(),
                    Contractor_MNo: $("#Contractor_MNo").val(),
                    Description: $("#Description").val()
                };
                $.ajax({
                    url: "/HandOverInspection/AddHandOverBuilding",
                    type: "POST",
                    cache: false,
                    data: JSON.stringify(Obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data == "200") {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Added Successfully',
                                showConfirmButton: false,
                                timer: 1500
                            }).then(function () {
                                window.location.reload();
                            });
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
        });
    });
});
function Fn_Edit(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
}
function Fn_View(val) {
    $(UI_Fields.LIST_VIEW).hide(100);
    $(UI_Fields.DIV_VIEWHANDOVER_BUILDING).show(100);
    Hzurl = '/HandOverInspection/_ViewHandOverBuilding';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.VIEWHANDOVER_BUILDINGLIST).load(Hzurl);
    Hzurl = '/HandOverInspection/_ViewHndOverBldgQuestionnairs';
    Hzurl += '/?Insp_HndOver_Building_Id=' + val;
    $(UI_Fields.GET_QUESTIONNAIRE_LIST).load(Hzurl);
}
function Fn_Qn_Edit(val) {
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
function Fn_Qn_View(val) {
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
function Fn_CA_View(val) {
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