function ServerSideTable_Inc() {
    debugger;
    let table = $(UI_Fields2.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/Incident/GetIncidentReporting',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields2.MAIN_TABLE).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Inc_Category_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Inc_Type_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Zone_Name', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Community_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(4)').val() } },
                    { data: 'Last_Reported_By', search: { value: $('#MainTable_wrapper').find('thead input:eq(5)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
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
            { data: 'Inc_Category_Id' },
            { data: 'Inc_Type_Id' },
            { data: 'Zone_Name' },
            { data: 'Community_Id' },
            { data: 'Last_Reported_By' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case 'Zone Supervisor Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 1;
                            break;
                        case 'HSE Team Approval Pending':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;
                        case 'Incident Closed':
                            badge = 'badge bg-success';
                            Status = 3;
                            break;
                        case 'Lead Investigator Approval Pending':
                            badge = 'badge bg-info';
                            Status = 4;
                            break;
                        case 'Investigation Pending':
                            badge = 'badge bg-info';
                            Status = 5;
                            break;
                        case 'Supervisor Investigation Approval Pending':
                            badge = 'badge bg-secondary';
                            Status = 6;
                            break;
                        case 'HSE Director Approval Pending':
                            badge = 'badge bg-dark';
                            Status = 7;
                            break;
                        case 'Supervisor Line Manger Approval Pending':
                            badge = 'badge bg-secondary';
                            Status = 8;
                            break;
                        case 'Supervisor Review Action Pending':
                            badge = 'badge bg-dark';
                            Status = 9;
                            break;
                        case 'Action Closure Approval Pending':
                            badge = 'badge bg-info';
                            Status = 10;
                            break;
                        case 'Action Closure Verified':
                            badge = 'badge bg-dark';
                            Status = 11;
                            break;
                        case 'Investigation Closed':
                            badge = 'badge bg-success';
                            Status = 12;
                            break;
                        case 'Investigation Approval Pending':
                            badge = 'badge bg-info';
                            Status = 13;
                            break;
                        case 'HSE Manager Approval Pending':
                            badge = 'badge bg-dark';
                            Status = 14;
                            break;
                        case 'Investigation Rejected':
                            badge = 'badge bg-danger';
                            Status = 15;
                            break;
                        default:
                            break;

                    }
                    let st = (data === "Closed On Spot" || data === "Hazard Completed") ? "Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Inc_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';
                }
            },

            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Zone Supervisor Approval Pending") {
                        html += '<a onclick="Fn_Edit(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;display:none"> <i class="mdi mdi-pencil font-size-18"></i></a>';
                        html += '<a onclick="Fn_Delete(' + row.Inc_Id + ')" href="javascript:void(0);" title="Delete" class="text-danger" data-bs-original-title="Delete" aria-label="Delete" style="cursor: pointer;display:none">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-delete font-size-18"></i></a>';
                        if (row.Role_Id == "5" || row.Role_Id == "4" || row.Role_Id == "3") {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="Review" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }

                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }

                    }
                    if (data == "HSE Team Approval Pending") {
                        if (row.Role_Id == "9" || row.Role_Id == "10" || row.Role_Id == "11") {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit Incident" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="Review Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + row.Unique_Id + ')" href="javascript:void(0);" title="Clouser Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Incident Closed") {
                        html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Lead Investigator Approval Pending") {
                        if (row.Lead_Investigator_Id == row.Login_Id) {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Supervisor Investigation Approval Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                        else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title=Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Investigation Pending") {
                        html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title=Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "HSE Director Approval Pending") {
                        if (row.Role_Id == "9") {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Supervisor Line Manger Approval Pending") {
                        if (row.Role_Id == "2") {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Supervisor Review Action Pending") {
                        if (row.Role_Id == "5") {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Action Closure Approval Pending") {
                        html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }

                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Action Closure Verified") {
                        if (row.Lead_Investigator_Id == row.Login_Id) {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Investigation Closed") {
                        html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                        html += '<a onclick="Fn_Final_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Final Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Investigation Approval Pending") {
                        if (row.Lead_Investigator_Id == row.Login_Id) {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';

                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        //html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "HSE Manager Approval Pending") {
                        if (row.Role_Id == "10") {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        }
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';

                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                            //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        if (row.Is_Knowledge_Share != "No") {
                            html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Health & Safety - Knowledge shared/ Lesson learned" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-account" style="color:green;font-size:20px;cursor:pointer"></i></a>';
                        }
                    }
                    if (data == "Investigation Rejected") {
                        html += '<a onclick="Fn_Approval_View_HSE(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        if (row.Add_Description_3 == "Yes") {
                            html += '<a onclick="Fn_Incident_Investigation_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        } else {
                            html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                        }
                        //html += '<a onclick="Fn_Incident_Report(' + row.Inc_Id + ',' + "'" + row.Unique_Id + "'" + ')" href="javascript:void(0);" title="Incident Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields2.MAIN_TABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Incident_Report',

            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Incident_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function ServerSideTable_Investing() {
    debugger;
    let table = $(UI_Fields2.MAIN_TABLE).DataTable({
        serverSide: true,
        processing: true,
        serverSide: true,
        searching: true,
        ordering: true,
        paging: true,
        ajax: {
            url: '/Incident/GetInvestigation',
            type: 'POST',
            contentType: 'application/json',
            data: function (d) {
                d.draw = d.draw || 1;
                d.start = d.start || 0;
                d.length = d.length || 10;
                d.order = d.order || [{ column: 0, dir: 'desc' }];
                d.search = d.search || { value: '' };
                let table = $(UI_Fields2.MAIN_TABLE).DataTable();
                let pageInfo = table.page.info();
                d.page = pageInfo.page;
                d.columns = [
                    { data: 'Unique_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(0)').val() } },
                    { data: 'Inc_Category_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(1)').val() } },
                    { data: 'Inc_Type_Id', search: { value: $('#MainTable_wrapper').find('thead input:eq(2)').val() } },
                    { data: 'Zone', search: { value: $('#MainTable_wrapper').find('thead input:eq(3)').val() } },
                    { data: 'Status', search: { value: $('#MainTable_wrapper').find('thead .Status1').val() } },
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
            { data: 'Inc_Category_Id' },
            { data: 'Inc_Type_Id' },
            { data: 'Zone' },
            {
                data: 'Status',
                className: 'text-center align-middle',
                render: function (data, type, row) {
                    let badge;
                    let Status;
                    switch (data) {
                        case 'Investigation Pending':
                            badge = 'badge bg-info';
                            Status = 2;
                            break;
                        case 'Investigation Approval Pending':
                            badge = 'badge bg-warning';
                            Status = 4;
                        case 'Investigation Approved':
                            badge = 'badge bg-secondary';
                            Status = 5;
                            break;
                        case 'Investigation Rejected':
                            badge = 'badge bg-danger';
                            Status = 5;
                            break;
                        default:
                            break;

                    }
                    let st = (data === "Closed On Spot" || data === "Hazard Completed") ? "Completed" : data;
                    return '<a href="#" class="' + badge + '" onclick="event.preventDefault();ViewDetails(' + "'" + row.Inc_Id + "'," + "'" + Status + "'" + ')">' + st + '</a>';
                }
            },

            {
                data: 'Status',
                render: function (data, type, row) {
                    let html = "";
                    if (data == "Zone Manager Approval Pending") {
                        html += '<a onclick="Fn_Edit(' + row.Inc_Id + ')" href="javascript:void(0);" title="Edit" class="text-success" data-bs-original-title="Edit" aria-label="View" style="cursor: pointer;display:none"> <i class="mdi mdi-pencil font-size-18"></i></a>'
                            + '<a onclick="Fn_Delete(' + row.Inc_Id + ')" href="javascript:void(0);" title="Delete" class="text-danger" data-bs-original-title="Delete" aria-label="Delete" style="cursor: pointer;display:none">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-delete font-size-18"></i></a>'
                            + '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Investigation Pending") {
                        if (row.Add_Description_3 == "No") {
                            html += '<a onclick="FnInvesting_Edit(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        } else {
                            html += '<a onclick="FnInvesting_Edit_by_Id(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';
                        }
                    }
                    if (data == "Incident Closed") {
                        html += '<a onclick="Fn_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="Fn_Clouser_Report(' + row.Inc_Id + ')" href="javascript:void(0);" title="Clouser Report" class="text-info" data-bs-original-title="Report" aria-label="Report">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-file-pdf" style="color:red;font-size:20px;cursor:pointer"></i></a>';
                    }
                    if (data == "Investigation Approval Pending") {
                        html += '<a onclick="Fn_Approval_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Investigation Approved") {
                        html += '<a onclick="Fn_Approval_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                    }
                    if (data == "Investigation Rejected") {
                        //html += '<a onclick="Fn_Approval_View(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-info" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-eye font-size-18"></i></a>';
                        html += '<a onclick="FnInvesting_Edit_by_Id(' + row.Inc_Id + ')" href="javascript:void(0);" title="View Incident" class="text-success" data-bs-original-title="Delete" aria-label="Delete">&nbsp;&nbsp;&nbsp;<i class="mdi mdi-pencil font-size-18"></i></a>';

                    }
                    return html;
                }
            },
        ],
        "initComplete": function (settings, json) {
            $(UI_Fields2.MAIN_TABLE).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
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
                title: 'Incident_Report',
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: 'Incident_Report'
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
}

function FnInvesting_Edit(val) {
    $(UI_Fields2.LIST_VIEW).hide(100);
    $(UI_Fields2.ADD_INCIDENTINVS_LIST).show(100);
    Hzurl = '/Incident/_ViewIncidentReport';
    Hzurl += '/?Inc_Id=' + val;
    $('#Justification_Comments').val('');
    $('#Description_circumstances').val('');
    //var cat_id = $("#txt_Inc_Category_Id").val();
    //alert(cat_id);
    $(UI_Fields2.VIEW_INCIDENT_LIST).load(Hzurl);
}

function FnInvesting_Edit_by_Id(Inc_Id) {
    debugger
    $(UI_Fields2.LIST_VIEW).hide(100);
    $(UI_Fields2.ADD_INCIDENTINVS_LIST).show(100);
    Hzurl = '/Incident/_ViewIncidentReport';
    Hzurl += '/?Inc_Id=' + Inc_Id;

    var inc_id = Inc_Id.toString();

    var Obj = {
        Inc_Id: inc_id,
    };
    $.ajax({
        url: "/Incident/_EditInvestigationReport",
        type: "POST",
        cache: false,
        data: JSON.stringify(Obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != null) {
                $("#Investigation_Id").val(data.Investigation_Id);
                $('#Description_circumstances').val(data.Description_circumstances);
                var count_value = 1;
                var otherPartiesCount = 1;
                var UnsafeAct = 1;
                $.each(data.L_Inves_Attachements, function (key, value) {
                    var extension = value.File_Path.substr((value.File_Path.lastIndexOf('.') + 1));

                    var thisName = $('.clsyes_no_' + count_value).attr('name');
                    var varYes_No_Upl = $('.file_Upl_' + count_value).attr('id');
                    var varYes_No = $('.Rem_' + count_value).attr('id');
                    var varComments = $('.comments_' + count_value).attr('id');
                    var varFiles = $('.com_' + count_value).attr('id');

                    if (value.Title == "Police Report") {
                        $(".hdncls_Police_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "Medical Report") {
                        $(".hdncls_Medical_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "Technical Report") {
                        $(".hdncls_Technical_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "DCD Report") {
                        $(".hdncls_DCD_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "Witness statement") {
                        $(".hdncls_Witness_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "IP Statement") {
                        $(".hdncls_IP_Documents_File").val(value.File_Path);
                    }
                    if (value.Title == "Others") {
                        $(".hdncls_Others_Documents_File").val(value.File_Path);
                    }

                    $("input[name=" + thisName + "][value=" + value.Is_Attachements + "]").prop('checked', true);

                    if (value.Is_Attachements == "Yes") {
                        $("#" + varYes_No).css("display", "none");
                        $("#" + varFiles).removeAttr('style');

                        if (extension == "png" || extension == "jpeg") {
                            $("#UploadPhots_" + count_value).append('<a href="' + value.File_Path + '" target=_blank > <img src=" ' + value.File_Path + '" style=height:20px;width:20px></a >');
                        }
                        else {
                            $("#UploadPhots_" + count_value).append('<a href="' + value.File_Path + '" target=_blank > <img src="/assets/images/pdf.png" style=height:20px;width:20px></a >');
                        }
                        $("#" + varYes_No_Upl).removeAttr('name');
                    }
                    else {

                        $("#" + varYes_No).removeAttr('style');
                        $("#" + varFiles).css("display", "none");
                        $("#" + varComments).val(value.Attachements_Description);

                    }
                    count_value++;

                });

                $.each(data.L_Inves_Other_Parties_Involved, function (i, e) {

                    var thisOtherName = $('.OtherParties_' + otherPartiesCount).attr('id');
                    var varName = $("#" + thisOtherName).val([e.Other_Name]);

                    var thisOtherContact = $('.otherPartiesContact_' + otherPartiesCount).attr('id');
                    var varContact = $("#" + thisOtherContact).val([e.Other_Contact_no]);

                    otherPartiesCount++;
                });

                $.each(data.L_Inves_Immediate_Cause_Unsafe_Act, function (x, y) {
                    //$("[value=" + y.M_Unsafe_Act_Id + "]").prop('checked', true);
                    $("input[name=M_UnsafeActList][value=" + y.M_Unsafe_Act_Id + "]").prop('checked', true);
                });

                $.each(data.L_Inves_Immediate_Cause_Unsafe_Cond, function (a, b) {
                    $("input[name=M_UnsafeConditionList][value=" + b.M_Unsafe_Cond_Id + "]").prop('checked', true);
                });

                $.each(data.L_Inves_Root_Cause_PF, function (c, d) {
                    $("input[name=PersonalFactorList][value = " + d.M_RootCause_PF_Id + "]").prop('checked', true);
                });

                $.each(data.L_Inves_Root_Cause_SF, function (e, f) {
                    $("input[name=SystemFactorList][value=" + f.M_RootCause_SF_Id + "]").prop('checked', true);
                });

                $.each(data.L_Inves_Mechanism_InjuryIllness, function (g, h) {
                    $("input[name=MechanismInjuryList][value =" + h.M_Mechanism_InjuryIllness_Id + "]").prop('checked', true);
                });

                $.each(data.L_Inves_AgencySource_InjuryIllness, function (k, l) {
                    $("input[name=AgencySourceInjuryList][value =" + l.M_AgencySource_InjuryIllness_Id + "]").prop('checked', true);
                });

                $("#Additional_Information").val(data.Additional_Information);
                $("#Risk_Assessment_Impact").val(data.Risk_Assessment_Environmental_Impact);

                $("#GetActionTakenList").empty();
                $.each(data.L_Inves_Actions_Taken_Immediately, function (m, n) {
                    $("#GetActionTakenList").append('<tr><td><input type="hidden" class="BuildingID" value="0"/><input type="text" class="form-control clsDescriptionAction" name="Building_Name" value="' + n.Description_of_Actions + '"/></td><td><input type="text" class="form-control clsActiontakenby" name=Building_Description value="' + n.Action_Taken_By + '"/></td><td><input type="date" class="form-control clsActionDate" name=clsActionDate value="' + n.Date_Completed + '"/></td><td><button type="button" class="btn btn-danger TblDeleteButton">Remove</button></td></tr>')

                });
                $("#GetIncidentRootCauseList").empty();
                $.each(data.L_Inves_Incident_Root_Cause, function (o, p) {
                    $("#GetIncidentRootCauseList").append('<tr><td><input type="hidden" class="BuildingID" value="0"/><input type="text" value="' + p.Root_Cause_Description + '" class="form-control clsIncidentRootCause" name="IncidentRootCauseDes" /><label id="txtIncidentRootCause" style="color:red"></label></td><td><button type="button" class="btn btn-danger TblRootCauseDeleteButton">Remove</button></td></tr>')
                });

                $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).empty();
                var indexValue = 1;
                $.each(data.L_Inves_Corrective_Actions, function (q, r) {
                    $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).append('<tr><td><input type="hidden" class="BuildingID" value="0"/><input type="text" class="form-control clsDesCorrectiveAction" name="DescriptionCorrectiveAction" value="' + r.Description_Actions + '" /><label id="txtDescriptionCorrectiveAction" style="color:red"></label></td><td align="left" valign="middle"><textarea class="form-control  clsAction_Taken" id="" spellcheck="false" readonly>' + r.Action_Taken + '</textarea></td><td><select class="form-control clsRiskMatrix clsRiskMatrix_' + indexValue + '" name="CorrectiveActionPriority" placeholder="This is a search placeholder"> <option selected value="0">--Select--</option><option value="High">High</option><option value="Low">Low</option><option value="Medium">Medium</option></select><label id="txtCorrectiveActionPriority" style="color:red"></label></td><td><input type="date" class="form-control clsCorrectiveActionDate" name=CorrectiveActionDate value="' + r.Target_Date + '"/><label id="txtCorrectiveActionDate" style="color:red"></label></td><td align="left" valign="middle"><input type="hidden" id="hdnService_Appendix_A" class="Corrective_Actions_APPENDIX_A_Viewhide" value="' + r.Upload_Evidence + '"/><a href='+ r.Upload_Evidence +' target="_blank"><img src="/assets/images/pdf.png" style="height:20px;width:20px" /></a></td><td><button type="button" class="btn btn-danger TblCorrActionDeleteButton">Remove</button></td></tr>')
                    $('.clsRiskMatrix_' + indexValue).val(r.Priority);
                    indexValue++;
                });

                var extension2 = data.Invs_File_Path.substr((data.Invs_File_Path.lastIndexOf('.') + 1));
                if (extension2 == "png" || extension2 == "jpeg") {
                    $("#Inves_Evidence_Upl").append('<a href="' + data.Invs_File_Path + '" target=_blank > <img src=" ' + data.Invs_File_Path + '" style=height:80px;width:80px></a >');
                }
                else {
                    $("#Inves_Evidence_Upl").append('<a href="' + data.Invs_File_Path + '" target=_blank > <img src="/assets/images/pdf.png" style=height:80px;width:80px></a >');
                }

                $("#Invs_File_Path").removeAttr('name');
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
    $(UI_Fields2.VIEW_INCIDENT_LIST).load(Hzurl);
}

function Fn_Edit(val) {
    alert(val)
}

function Fn_Delete(val) {
    alert(val)
}

function Fn_View(val) {
    debugger;
    var Hzurl = "";
    $(UI_Fields2.LIST_VIEW).hide(100);
    $(UI_Fields2.INC_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewIncidentReport';
    Hzurl += '/?Inc_Id=' + val;
    $(UI_Fields2.VIEW_INCIDENT_LIST).load(Hzurl);

    var INV_Hzurl = "";
    INV_Hzurl = '/Incident/_ViewInvestigationReport';
    INV_Hzurl += '/?Inc_Id=' + val;
    $(UI_Fields2.VIEW_INVESTIGATION_LIST_APPR).load(INV_Hzurl);

    var APP_INV_Hzurl = "";
    APP_INV_Hzurl = '/Incident/_ViewIncidentReportApproveProcess';
    APP_INV_Hzurl += '/?Inc_Id=' + val;
    $(UI_Fields2.VIEW_INV_APPROVE_PROCESS).load(APP_INV_Hzurl);
}

function Fn_Approval_View_HSE(val) {
    debugger;
    //$(UI_Fields2.LIST_VIEW).hide(100);
    //$(UI_Fields2.ADD_INCIDENTINVS_LIST).hide(100);
    //$(UI_Fields2.VIEW_INCIDENTINVS_LIST).show(100);

    //Hzurl = '/Incident/_ViewIncidentReport';
    //Hzurl += '/?Inc_Id=' + val;
    //$(UI_Fields2.VIEW_INC_LIST).load(Hzurl);

    //INV_Hzurl = '/Incident/_ViewInvestigationReport';
    //INV_Hzurl += '/?Inc_Id=' + val;
    //$(UI_Fields2.VIEW_INVESTIGATION_LIST).load(INV_Hzurl);

    //var APP_INV_Hzurl = "";
    //APP_INV_Hzurl = '/Incident/_ViewIncidentReportApproveProcess';
    //APP_INV_Hzurl += '/?Inc_Id=' + val;
    //$(UI_Fields2.VIEW_INV_APPROVE_PROCESS).load(APP_INV_Hzurl);

    $(UI_Fields2.LIST_VIEW).hide(100);
    $(UI_Fields2.INC_DETAILS_VIEW).show(100);
    Hzurl = '/Incident/_ViewIncidentReport';
    Hzurl += '/?Inc_Id=' + val;
    $(UI_Fields2.VIEW_INCIDENT_LIST).load(Hzurl);

    

    INV_Hzurl = '/Incident/_ViewInvestigationReport';
    INV_Hzurl += '/?Inc_Id=' + val;
    $(UI_Fields2.VIEW_INVESTIGATION_LIST_APPR).load(INV_Hzurl);
}

function Fn_Incident_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View").hide();
    $.post("/Report/Incident_Report", { Inc_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

function Fn_Incident_Investigation_Report(id, Unique_Id) {
    let NotiID = parseInt(id);
    $(".preloader").show();
    $("#List_View").hide();
    $.post("/Report/Incident_Investigation_Report", { Inc_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

function Fn_Clouser_Report(id, Unique_Id) {
    debugger;
    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Inc_Knowledge_Share_Report", { Inc_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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

function Fn_Final_Report(id, Unique_Id) {

    $(".preloader").show();
    $("#List_View").hide();
    let NotiID = parseInt(id);
    $.post("/Report/Investigation_Report", { Inc_Id: NotiID, Unique_Id: Unique_Id }, function (data) {
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


