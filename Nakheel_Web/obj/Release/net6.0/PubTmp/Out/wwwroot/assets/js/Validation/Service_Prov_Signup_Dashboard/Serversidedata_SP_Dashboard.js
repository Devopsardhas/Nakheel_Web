//Pop Up Table
function SPCardToggle(Cid, txtid, CreatedBy) {
    let txt = $(txtid).text();
    if (txt == "0" || txt == "0%") {
        toastr["info"]("Data Not Available!");
    }
    else {
        //ShowHideLoader(1);
        $(UI_FIELDS1.IMPACT_DIV_TABLE).dataTable().fnDestroy();
        $(UI_FIELDS1.SERVICE_PROVIDER_Div_TBODY).html('');
        let Year = $(UI_FIELDS1.DRP_YEAR).val();
        if (Year == null || Year == "" || Year == undefined) {
            Year = new Date().getFullYear();
        }
        let Zone_Id = $(UI_FIELDS1.M_ZONE).val();
        let Community_Id = $(UI_FIELDS1.COMMUNITY_ID).val();
        let Building_Id = $(UI_FIELDS1.BUILDING_ID).val();
        let From_Date = $(UI_FIELDS1.FROM_DATE).val();
        let To_Date = $(UI_FIELDS1.TO_DATE).val();
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
        $.post("/ServiceProviderSignup_Dashboard/ServiceProv_Dashboard_Card_View", { dash_Params: model }, function (data) {
            debugger;
            if (data != null) {
                $("#OverDueHead").text("Service Provider Report");
                let hide = [];
                let html = "";
                if (Cid == '1' || Cid == '2' || Cid == '3' || Cid == '4' || Cid == '5' || Cid == '6' || Cid == '7' ) {
                    
                    $(data).each(function (i, e) {
                        html += '<tr><td align="left" valign="middle">' + e.Company_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Manager_Incharge + '</td>';
                        html += '<td align="left" valign="middle">' + e.Zone_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Community_Master_Name + '</td>';
                        html += '<td align="left" valign="middle">' + e.Contract_End_Date + '</td>';
                        html += '<td align="left" valign="middle">' + e.Scope_Of_Work + '</td>';
                        html += '<td align="left" valign="middle">' + e.Purchase_Order_Number + '</td>';
                        html += '<td align="left" valign="middle">' + e.Status + '</td>';
                        html += '<td align="left" valign="middle">' + e.Contract_Status + '</td>';
                    });
                }
                $(UI_FIELDS1.SERVICE_PROVIDER_Div_TBODY).append(html);
                $(UI_FIELDS1.CARD_MODEL).modal('toggle');
                SP_ApplyDataTable(UI_FIELDS1.IMPACT_DIV_TABLE, hide, 'Service Provider Report');
            }
            else {
                //ShowHideLoader(2);
                toastr["error"]("Something went wrong Please Try Again!");
            }
        });
    }
}
function SP_ApplyDataTable(id, hide, title) {
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