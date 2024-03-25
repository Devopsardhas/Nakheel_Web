//Pop Up Table
function INCCardToggle(Cid, txtid, CreatedBy) {
    let txt = $(txtid).text();
    //alert(Cid + ' ' + txt)
    if (txt == "0" || txt == "0%") {
        toastr["info"]("Data Not Available!");
    }

    else {
        //ShowHideLoader(1);
        $(UI_Fields.POP_UP_TABLE).dataTable().fnDestroy();
        $(UI_Fields.IMAPACT_DIV_TBODY).html('');
        let Year = $(UI_Fields.DRP_YEAR).val();
        let Zone_Id = $(UI_Fields.M_ZONE).val();
        let Community_Id = $(UI_Fields.COMMUNITY_ID).val();
        let Building_Id = $(UI_Fields.BUILDING_ID).val();
        let From_Date = $(UI_Fields.FROM_DATE).val();
        let To_Date = $(UI_Fields.TO_DATE).val();
        //let CTRL_Type = $('input[name="CTRL_TYPE"]:checked').val();
        //let yr = $(UI_Fields.DRP_YEAR).val();
        //let Bu = $(UI_Fields.BUSINESS_UNIT_ID).val();
        //if (Bu == "-1") {
        //    Bu = '';
        //}
        //let Pb = $(UI_Fields.PROJ_BUILD_ID).val();
        //let Categ = $(UI_Fields.DRP_CAT).val();
        //let varThree_Month = '';
        //let varSix_Month = '';
        //let FD = '';
        //let TD = '';
        //let ch = $(UI_Fields.LAST_SIX).is(':checked');
        //if (ch) {
        //    varSix_Month = 'true';
        //    yr = '';
        //}
        //let ch1 = $(UI_Fields.LAST_THREE).is(':checked');
        //if (ch1) {
        //    varThree_Month = 'true';
        //    yr = '';
        //}
        //let FilterDate = $(UI_Fields.FILTER_DATE).is(':checked');
        //if (FilterDate) {
        //    FD = $(UI_Fields.FROM_DATE).val();
        //    TD = $(UI_Fields.TO_DATE).val();
        //    yr = '';
        //}
        //let CreatedBy = CreatedBy;
        let Category_Name = '';

        let model = {
            //Year: yr,
            //BU_ID: Bu,
            //PB_ID: Pb,
            //Six_Month: varSix_Month,
            //From_Date: FD,
            //To_date: TD,
            //Controller_Type: CTRL_Type,
            //Category_Type: Categ,
            //Three_Month: varThree_Month,
            Year: yr,
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
                $(UI_Fields.IMAPACT_DIV_TBODY).append(html);
                $(UI_Fields.CARD_MODEL).modal('toggle');
                ApplyDataTable(UI_Fields.POP_UP_TABLE, hide, 'Incident Report');
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

function DrawGraphInc_Trend_monthwise(arr, name) {
    options = {
        series: arr,
        chart: {
            type: "bar",
            height: 250,
            stacked: !0,
            toolbar: {
                show: true,
                download: true,
            },
            zoom: {
                enabled: !0
            }
        },
        responsive: [{
            breakpoint: 480,
            options: {
                legend: {
                    position: "bottom",
                    offsetX: -10,
                    offsetY: 0
                }
            }
        }],
        plotOptions: {
            bar: {
                horizontal: !1,
                borderRadius: 10
            }
        },
        xaxis: {
            categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        },
        legend: {
            position: "bottom"
        },
        fill: {
            opacity: 1
        },
        colors: barchartColors = ["#0D87AC"],
    };
    (chart = new ApexCharts(document.querySelector("#Graph7"), options)).render();
    chart.updateOptions({
        xaxis: {
            categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        },
        series: arr,
    });
}
function INC_Analytics_Trend_Monthwise(Cid, txtid, CreatedBy) {
    debugger;
    let txt = $(txtid).text();
    //alert(Cid + ' ' + txt)
    if (txt == "0" || txt == "0%") {
        toastr["info"]("Data Not Available!");
    }

    else {
        let Zone_Id = $(UI_Fields.M_ZONE).val();
        let Community_Id = $(UI_Fields.COMMUNITY_ID).val();
        let Building_Id = $(UI_Fields.BUILDING_ID).val();
        let From_Date = $(UI_Fields.FROM_DATE).val();
        let To_Date = $(UI_Fields.TO_DATE).val();
        let Category_Name = '';
        let model = {
            Zone_ID: Zone_Id,
            Community_Id: Community_Id,
            Building_Id: Building_Id,
            CreatedBy: CreatedBy,
            Category_Name: Category_Name,
            Card_View_Id: Cid,
            From_Date: From_Date,
            To_date: To_Date
        };
        $.post("/Incident/Incident_Dashboard_Trend_MonthWise", { dash_Params: model }, function (data) {
            debugger;
            if (data != null) {
                var Chartdata = [];
                let html = "";
                if (Cid == '1' || Cid == '2' || Cid == '3' || Cid == '4' || Cid == '5' || Cid == '6' || Cid == '7' || Cid == '8') {
                    $(data).each(function (i, m) {
                        var violation = {};
                        var monthdata = [];
                        violation["name"] = m.Name;
                        monthdata.push(m.January);
                        monthdata.push(m.February);
                        monthdata.push(m.March);
                        monthdata.push(m.April);
                        monthdata.push(m.May);
                        monthdata.push(m.June);
                        monthdata.push(m.July);
                        monthdata.push(m.August);
                        monthdata.push(m.September);
                        monthdata.push(m.October);
                        monthdata.push(m.November);
                        monthdata.push(m.December);
                        violation["data"] = monthdata;
                        Chartdata.push(violation);
                    });
                    DrawGraphInc_Trend_monthwise(Chartdata, 'Incident Trend');
                }

            }
            else {
                toastr["error"]("Something went wrong Please Try Again!");
            }
        });
    }
}