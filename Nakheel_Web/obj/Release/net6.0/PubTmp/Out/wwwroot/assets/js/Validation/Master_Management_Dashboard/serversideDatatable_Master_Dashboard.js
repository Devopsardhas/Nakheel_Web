//Pop Up Table
function INCCardToggle(Cid, txtid, CreatedBy) {
    debugger;
    let txt = $(txtid).text();
    //alert(Cid + ' ' + txt)
    if (txt == "0" || txt == "0%") {
        toastr["info"]("Data Not Available!");
    }

    else {
        //ShowHideLoader(1);
        $(UI_FIELDS.POP_UP_TABLE).dataTable().fnDestroy();
        $(UI_FIELDS.IMAPACT_DIV_TBODY).html('');
        let Year = $(UI_FIELDS.DRP_YEAR).val();
        //if (Year == null || Year == "" || Year == undefined) {
        //    Year = new Date().getFullYear();
        //}
        let Zone_Id = $(UI_FIELDS.DRP_ZONE).val();
        let Community_Id = $(UI_FIELDS.DRP_COMMUNITY).val();
        let Building_Id = $(UI_FIELDS.DRP_BUILDING).val();
        let From_Date = $(UI_FIELDS.FROM_DATE).val();
        let To_Date = $(UI_FIELDS.TO_DATE).val();
        let Category_Name = '';
        let model = {
            Year: Year,
            Zone_ID: Zone_Id,
            Community_Id: Community_Id,
            Building_Id: Building_Id,
            CreatedBy: CreatedBy,
            Category_Name: Category_Name,
            Card_View_Id: Cid,
            From_Date: From_Date,
            To_date: To_Date
        };
        $.post("/Incident/Incident_Dashboard_Card_View", { dash_Params: model }, function (data) {
            debugger;
            if (data != null) {
                let hide = [];
                let html = "";
                if (Cid == '1' || Cid == '2' || Cid == '3' || Cid == '4' || Cid == '5' || Cid == '6' || Cid == '7' || Cid == '8' || Cid == '9' || Cid == '10' || Cid == '11' || Cid == '12' || Cid == '13') {
                    $("#OverDueHead").text("Incident Report");
                    $("#TH_2").text("Description");
                    $("#TH_1").text("Date");
                    $("#TH_3").text("Time");
                    $("#TH_5").text("Notified By");
                    $("#TH_4").text("Status");
                    $(data).each(function (i, e) {
                        html += '<tr><td align="left" valign="middle">' + e.Unique_Id + '</td>';
                        //html += '<td align="left" valign="middle">' + e.CreatedBy_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Business_Unit_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Project_Building_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Category_Id + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Type_Id + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Description + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Date + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Time + '</td>';
                        html += '<td align="left" valign="middle">' + e.Zone_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Community_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Building_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Is_Injury_Illness + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Business_Unit_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Master_Community_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Key_Report_Path + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Status + '</td>'
                        //html += '<td align="left" valign="middle">' + e.CreatedDate + '</td><</tr >';
                    });
                }
                else {
                    $("#OverDueHead").text("Overdue Report");
                    hide = [14, 15, 16, 17, 18, 19];
                    if (CTRL_Type == "1") {
                        $("#TH_2").text("Action");
                        $("#TH_5").text("Assignee");
                    }
                    else {
                        $("#TH_2").text("Description");
                        $("#TH_5").text("Notified By");
                    }
                    $("#TH_3").text("Target Date");
                    hide = [4, 7, 8, 9, 10, 11, 12, 13, 14];
                    $("#OverDueHead").text("Corrective Action Report");
                    $("#TH_2").text("Action");
                    $("#TH_1").text("Target Date");
                    $("#TH_5").text("Assignee");
                    $("#TH_3").text("Status");
                    $(data).each(function (i, e) {
                        html += '<tr><td align="left" valign="middle">' + e.Unique_Id + '</td>';
                        //html += '<td align="left" valign="middle">' + e.CreatedBy_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Business_Unit_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Project_Building_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Category_Id + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Type_Id + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Description + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Date + '</td>';
                        html += '<td align="left" valign="middle">' + e.Inc_Time + '</td>';
                        html += '<td align="left" valign="middle">' + e.Zone_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Community_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Building_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Is_Injury_Illness + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Business_Unit_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Master_Community_Name + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Key_Report_Path + '</td>';
                        //html += '<td align="left" valign="middle">' + e.Status + '</td>'
                        //html += '<td align="left" valign="middle">' + e.CreatedDate + '</td><</tr >';
                    });
                }
                //ShowHideLoader(2);
                $(UI_FIELDS.IMAPACT_DIV_TBODY).append(html);
                $(UI_FIELDS.CARD_MODEL).modal('toggle');
                ApplyDataTable(UI_FIELDS.POP_UP_TABLE, hide, 'Incident Report');
            }
            else {
                //ShowHideLoader(2);
                toastr["error"]("Something went wrong Please Try Again!");
            }
        });
    }
}
function ApplyDataTable(id, hide, title) {
    debugger
    $(id).DataTable({
        "order": [[0, "desc"]],
        //"scrollX": true,
        //"fixedHeader": true,
        "initComplete": function (settings, json) {
            $(id).wrap("<div class='tableFixHead' style='overflow:auto;min-height:auto;max-height:430px; width:100%;position:relative;'></div>");
        },
        "columnDefs": [
            {
                "targets": hide, // index of columns to be hidden initially
                "visible": false,
                "searchable": false
            },
        ],
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: 'Excel',
                title: title
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                title: title
            },
            {
                extend: 'colvis',
                text: 'Show Columns',
                columns: ':not(.dt-head-hidden)'
            }
        ]
    });
    $(id).attr("style", "width:100%");
}

