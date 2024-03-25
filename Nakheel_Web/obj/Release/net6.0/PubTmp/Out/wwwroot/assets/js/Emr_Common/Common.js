
function PopulateDrp(selectElement, data, label) {
    let $select = selectElement;
    $select.empty();
    $select.append("<option selected value='' style='text-align:center'>" + label +"</option>");

    data.forEach(function (e) {
        $select.append("<option value=" + e.Value + ">" + e.Text + "</option>");
    });
}
function AjaxAsynHTML(url, data, id) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        dataType:'html',
        success: function (response) {
            $(id).empty();
            $(id).html(response);
            $(id).show(100);
            $(UI_Fields.LOADER).hide();
            //if (successCallback) {
            //    successCallback(response);
            //}
        },
        error: function (error) {
            toastr["error"]("Error Occurred while processing your request!");
        }
    });
}
function AjaxAsynFile(url, formData, token, successCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        headers: {
            RequestVerificationToken: token
        },
        processData: false,
        contentType: false,
        success: function (response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function (xhr, status, error) {
            ToastrMessage("error", "Error While processing your request.");
        }
    });
}

function AjaxAsynT(url, data, token, successCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: data,
        headers: {
            RequestVerificationToken: token
        },
        success: function (response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            toastr["error"]("Error Occured while processing your request!");
        }
    });
}
function AjaxAsynWT(url, data , successCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        dataType: 'json',
        data: data,
        success: function (response) {
            if (successCallback) {
                successCallback(response);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            toastr["error"]("Error Occured while processing your request!");
        }
    });
}
//function ShowAlert(title, text,callback) {
//    Swal.fire({
//        title: title,
//        text: text,
//        icon: "warning",
//        showCancelButton: !0,
//        confirmButtonColor: "#34c38f",
//        cancelButtonColor: "#f46a6a",
//        confirmButtonText: "Yes"
//    }).then(function (result) {
//        if (result.isConfirmed) {
//            if (callback && typeof callback === "function") {
//                callback();
//            }
//        }
//    });
//}
function LOAD_BUSINESS_UNIT() {

    $.post("/CommonMaster/LoadAllBusinessUnit", function (data) {
        $(UI_Fields.BUSINESS_UNIT).empty();
        $(UI_Fields.BUSINESS_UNIT).append("<option selected value='0' style='text-align:center' disabled> --Select--</option>");
        $(data).each(function (i, e) {
            $(UI_Fields.BUSINESS_UNIT).append("<option value=" + e.Business_Unit_Id + ">" + e.Business_Unit_Name + "</option>");
        });
    });
}
function LOAD_ZONE() {
    $.post("/CommonMaster/LoadAllZone", function (data) {
        $(UI_Fields.ZONE).empty();
        $(UI_Fields.ZONE).append("<option selected value='0' style='text-align:center' disabled> --Select--</option>");
        $(data).each(function (i, e) {
            $(UI_Fields.ZONE).append("<option value=" + e.Zone_Id + ">" + e.Zone_Name + "</option>");
        });
    });
}
function Fn_Zone(Zone_Id) {
    $.post("/CommonMaster/LoadAllCommunitybyZone", { Zone_Id: Zone_Id }, function (data) {
        $(UI_Fields.COMMUNITY_ID).empty();
        $(UI_Fields.BUILDING_ID).empty();
        $(UI_Fields.MASTER_COMMUNITY_ID).empty();
        $(UI_Fields.COMMUNITY_ID).append("<option selected value='0'style='text-align:center'  disabled>--Select--</option>");
        $(data).each(function (i, e) {
            $(UI_Fields.COMMUNITY_ID).append("<option value=" + e.Community_Master_Id + ">" + e.Community_Master_Name + "</option>");
        });
    });
}
function Fn_Community(Community_Id) {
    let Zone_Id = $(UI_Fields.ZONE).val();
    $.post("/CommonMaster/LoadAllBuildingbyZone", { Zone_Id: Zone_Id, Community_Id: Community_Id }, function (data) {
        $(UI_Fields.BUILDING_ID).empty();
        $(UI_Fields.BUILDING_ID).append("<option selected value='' style='text-align:center'>All</option>");
        $(data).each(function (i, e) {
            $(UI_Fields.BUILDING_ID).append("<option value=" + e.Sub_Building_Id + ">" + e.Building_Name + "</option>");
        });
    });
}
function FnBusiness_Unit(val) {
    var buname = $("#Business_Unit option:selected").text();
    if (buname == "NCM") {
        $("#Fn_Business_Unit_Type").show();
    }
    else {
        $("#Fn_Business_Unit_Type").hide();
    }
}
function LOADEMP_FILTERS() {
    var Emp_Community_Id = $(UI_Fields.COMMUNITY_ID).val();
    var Emp_Building_Id = $(UI_Fields.BUILDING_ID).val();
    var Emp_Master_Community_Id = $(UI_Fields.MASTER_COMMUNITY_ID).val();
    $.post("/CommonMaster/LoadEmpMasterFilter", { Emp_Community_Id: Emp_Community_Id, Emp_Building_Id: Emp_Building_Id, Emp_Master_Community_Id: Emp_Master_Community_Id }, function (data) {
        $(UI_Fields.VIEW_EMPLOYEE).empty();
        $(UI_Fields.VIEW_EMPLOYEE).append("<option selected value='0' disabled>--Select--</option>");
        $(data).each(function (i, e) {
            $(UI_Fields.VIEW_EMPLOYEE).append("<option value=" + e.Employee_Identity_Id + ">" + e.First_Name + "</option>");
        });
    });
}
function Load_GoogleMaps() {
    map_Hzurl = '/CommonMaster/_LoadGoogleMap';
    $(UI_Fields.GOOGLE_MAP_API).load(map_Hzurl);
}
function ApplySelect2(id) {
    $(id).select2({
        placeholder: "--Select--",
        theme: 'bootstrap-5',
    });
}


function GetformToken(id) {
    let form = $(id);
    let token = $('input[name="__RequestVerificationToken"]', form).val();
    return token;
}

function ToastrMessage(type,Msg) {
    toastr[type](Msg);
}

function ApplyFileUpload(id, type) {

    id.fileinput({
        allowedFileExtensions: type,
        showUpload: false,
        showRemove: false,
        showPreview: true,
        dropZoneTitle: 'Drag & drop or',
        dropZoneClickTitle: '<br>Click here to select files',
        minFileCount: 0,
        maxFileCount: 5,
        autoReplace: true,
        autoOrientImage: true,
        maxFileSize: 30720,
        //initialPreviewCount: 3,
        browseOnZoneClick: true,
        initialPreviewAsData: true,
    }).on("filebatchselected", function (event, files) {
        let totalFileSize = 0;
        $.each(data, function (index, file) {
            totalFileSize += file.size;
        });
        if (totalFileSize > 30720000) {
            toastr["error"]("Total file size must not exceed 30 MB");
            id.fileinput('clear');
            return false;
        }
    });
}

function ApplyFileUploadEdit(id, IntPreview, IntConfig, urlid, type) {
    id.fileinput({
        allowedFileExtensions: type,
        showUpload: false,
        showRemove: false,
        showPreview: true,
        dropZoneTitle: 'Drag & drop or',
        dropZoneClickTitle: '<br>Click here to select files',
        minFileCount: 0,
        maxFileCount: 5,
        autoReplace: true,
        autoOrientImage: true,
        maxFileSize: 30720,
        //initialPreviewCount: 3,
        browseOnZoneClick: true,
        initialPreviewAsData: true,
        overwriteInitial: false,
        initialPreview: IntPreview,
        initialPreviewConfig: IntConfig,
        uploadExtraData: { img_key: 1 }
    }).on("filebatchselected", function (event, data) {
        let totalFileSize = 0;
        $.each(data, function (index, file) {
            totalFileSize += file.size;
        });
        if (totalFileSize > 30720000) {
            toastr["error"]("Total file size must not exceed 30 MB");
            id.fileinput('clear');
            return false;
        }
    }).on("filebeforedelete", function (event, data) {

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning", showCancelButton: !0,
            confirmButtonColor: "#34c38f",
            cancelButtonColor: "#f46a6a",
            confirmButtonText: "Yes, delete it!"
        }).then(function (t) {

            if (t.isConfirmed) {
                if (urlid == 1) {
                    let delurl = '/IncidentNotification/DeletePhoto';
                    DelPhotoList(data, id, urlid, delurl);
                }
                else {
                    let delurl = '/IncidentNotification/DeleteINVPhoto';
                    DelPhotoList(data, id, urlid, delurl);
                }

            }
        });
        return true;
    }); 
    //$(id).("filebeforedelete", function (event, data) {
    //    alert(data);
    //    return true;
    //});
}
function View_Report(url, Data, List_ID, Loader_ID) {
    $(Loader_ID).show();
    $(List_ID).hide();
    $.post(url, Data, function (data) {
        if (data == "404") {
            ToastrMessage("error", "Error While processing your request.");
        }
        else {
            window.open(data);
        }
    }).always(function () {
        $(List_ID).show(100);
        $(Loader_ID).hide();
    });
}

function DisAbleSubmitbtn(btn, Fid) {
    $submitButton = $(btn);
    $(Fid).data('validator').settings.submitHandler = function (form) {
        form.submit();
        $submitButton.prop('disabled', true);
    };
}