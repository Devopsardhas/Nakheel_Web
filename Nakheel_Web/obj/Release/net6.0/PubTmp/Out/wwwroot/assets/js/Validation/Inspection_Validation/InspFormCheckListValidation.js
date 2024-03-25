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
                Service_Provider_Attended: {
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
                Service_Provider_Attended: {
                    required: "Please Select Service Provider",
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
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#Date_of_Audit-error").removeClass('mt-2');
                $("#Date_of_Audit-error").addClass('m-100 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Zone_Emp-error").removeClass('mt-2');
                $("#Zone_Emp-error").addClass('m-100 text-danger');
                $("#Service_Provider_Attended-error").removeClass('mt-2');
                $("#Service_Provider_Attended-error").addClass('m-100 text-danger');

                $("#HandOver_Type-error").removeClass('mt-2');
                $("#HandOver_Type-error").addClass('m-10 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                //debugger
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
                    SP_Attended: $("#SP_Attended").val(),
                    Service_Provider: $("#Service_Provider").val(),
                    Others_SP: $("#Others_SP").val(),
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
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form
        $("#F_HealthSafety_Building").validate({
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
                Zone_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                Health_Safety_Type: {
                    required: true,
                },
                Service_Provider: {
                    required: true,
                    minlength: 1
                },
                Date_of_Inspection: {
                    required: true,
                },
                Zone_Manager: {
                    required: true,
                },
                Zone_Supervisor: {
                    required: true,
                },
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
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Building_Id: {
                    required: "Please Select Building",
                },
                Health_Safety_Type: {
                    required: "Please Select Health & Safety Type",
                },
                Service_Provider: {
                    required: "Please Select Service Provider",
                },
                Date_of_Inspection: {
                    required: "Select Date of Inspection",
                },
                Zone_Manager: {
                    required: "Please Select Zone Manager",
                },
                Zone_Supervisor: {
                    required: "Please Select Zone Supervisor",
                },
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

                $("#Health_Safety_Type-error").removeClass('mt-2');
                $("#Health_Safety_Type-error").addClass('m-10 text-danger');

                $("#Zone_Manager-error").removeClass('mt-2');
                $("#Zone_Manager-error").addClass('m-10 text-danger');

                $("#Zone_Supervisor-error").removeClass('mt-2');
                $("#Zone_Supervisor-error").addClass('m-10 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];
                var OBS_ARR = [];
                //var Photo_List_QN = [];
                //var Photo_List_UP = [];
                var subSno = 1;
                //let valid = $("#F_HealthSafety_Building").valid();
                //if (!valid) {
                //    return false;
                //}
                $('#tblHealthSafetyInspQns tbody > tr').each(function () {
                    debugger
                    var varInsp_Question_Id = $(this).closest('tr').find('.Insp_Question_Id').val();
                    var varQns_Action = $(this).closest('tr').find('input[name="Action_' + subSno + '"]:checked').val();
                    var varPhoto_File_Path = $(this).closest('tr').find('.QnPhoto').val();
                    var varHazard_Risk = $(this).closest('tr').find('.Hazard_Risk').val();
                    var varRequirements = $(this).closest('tr').find('.Requirements').val();
                    var varAction_Description = $(this).closest('tr').find('.QnDescription').val();
                    var varCategory = $(this).closest('tr').find('.Insp_Category').val();
                    var varSub_Category = $(this).closest('tr').find('.Sub_Category').val();
                    var varRisk_Level = $(this).closest('tr').find('.Insp_Risk').val();
                    if (varPhoto_File_Path != "" && varPhoto_File_Path != null) {
                        //Photo_List_QN = [];
                        var val_UploadPhotos = varPhoto_File_Path.split(',');
                        var varPhoto_File_Path_QnFinal;
                        var i;
                        for (i = 0; i < val_UploadPhotos.length; i++) {
                            var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                            if (x != "/") {
                                //var Pdata = {};
                                varPhoto_File_Path_QnFinal = x;
                                //Pdata.Insp_Sub_Photo_Id = "0";
                                //Photo_List_QN.push(Pdata);
                            }
                        }
                    }
                    var data = {};
                    data.Insp_Questionnaires_Id = varInsp_Question_Id;
                    data.Qns_Action = varQns_Action;
                    data.Photo_File_Path = varPhoto_File_Path_QnFinal;
                    data.Hazard_Risk = varHazard_Risk;
                    data.Requirements = varRequirements;
                    data.Description_Action = varAction_Description;
                    data.Category = varCategory;
                    data.Sub_Category = varSub_Category;
                    data.Risk_Level = varRisk_Level;
                    QNS_ARR.push(data);
                    subSno++;
                });
                $('#tbl_Temp_Insp_HSObser tbody > tr').each(function () {
                    //Photo_List_UP = [];
                    //debugger
                    var varInsp_Observation_Name = $(this).closest('tr').find('.HS_Observations').val();
                    var varHazard_Risk = $(this).closest('tr').find('.HS_HazardRisk').val();
                    var varRequirements = $(this).closest('tr').find('.HS_Requirements').val();
                    var varAction_Description = $(this).closest('tr').find('.HS_ActionRequired').val();
                    var varCategory = $(this).closest('tr').find('.HS_Category').val();
                    var varSub_Category = $(this).closest('tr').find('.HS_SubCategory').val();
                    var varRisk_Level = $(this).closest('tr').find('.HS_RiskLevel').val();
                    var varPhoto_File_Path = $(this).closest('tr').find('.UploadPhotos').val();
                    if (varPhoto_File_Path != "" && varPhoto_File_Path != null) {
                        //Photo_List_UP = [];
                        var val_UploadPhotos = varPhoto_File_Path.split(',');
                        var varPhoto_File_Path_Final;
                        var i;
                        for (i = 0; i < val_UploadPhotos.length; i++) {
                            var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                            if (x != "/") {
                                //var Pdata = {};
                                varPhoto_File_Path_Final = x;
                                //Pdata.Insp_Sub_Photo_Id = "0";
                                //Photo_List_UP.push(Pdata);
                            }
                        }
                    }

                    var data = {};
                    data.Insp_Questionnaires_Name = varInsp_Observation_Name;
                    data.Hazard_Risk = varHazard_Risk;
                    data.Requirements = varRequirements;
                    data.Description_Action = varAction_Description;
                    data.Category = varCategory;
                    data.Sub_Category = varSub_Category;
                    data.Risk_Level = varRisk_Level;
                    data.Photo_File_Path = varPhoto_File_Path_Final;
                    OBS_ARR.push(data);
                    subSno++;
                });
                var Obj = {
                    Insp_HealthSafety_Id: $("#Insp_HealthSafety_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Id: $("#Business_Unit").val(),
                    Community_Id: $("#Community_Id").val(),
                    Business_Unit_Type_Name: $("#Hs_Business_Unit_Type").val(),
                    Company_Name: $("#Company_Name").val(),
                    Inspected_By_Id: $("#Inspected_By_Id").val(),
                    Inspected_By_Name: $("#Inspected_By_Name").val(),
                    Date_of_Inspection: $("#Date_of_Inspection").val(),
                    Schedule_Type: $("#Schedule_Type").val(),
                    Health_Safety_Type: $("#Health_Safety_Type").val(),
                    Zone_Supervisor: $("#Zone_Supervisor").val(),
                    Zone_Manager: $("#Zone_Manager").val(),
                    Safety_Violation: $('input[name="Safety_Violation"]:checked').val(),
                    Description: $("#SV_Description").val(),
                    L_Insp_HealthSafety_Questionnaires: QNS_ARR,
                    L_Insp_HealthSafety_Observation: OBS_ARR
                };
                $.ajax({
                    url: "/HandOverInspection/AddHealthSafetyInspection",
                    type: "POST",
                    cache: false,
                    data: JSON.stringify(Obj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        if (data.STATUS_CODE == "200") {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Added Successfully',
                                showConfirmButton: false,
                                timer: 1500
                            }).then(function () {
                                if (data.Return_2 == 0) {
                                    $('.submitAdd').prop('disabled', false);
                                    window.location.reload();
                                } else {
                                    $('.submitAdd').prop('disabled', false);
                                    var url = "/Inspection/Insp_Safety_Violation_Temp?id=" + data.Return_2;
                                    window.location.href = url;
                                }
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
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form
        $("#F_Insp_Service_Provider").validate({
            //rules: {
            //    Business_Unit: {
            //        required: true,
            //    },
            //    Community: {
            //        required: true,
            //    },
            //    Master_Community_Id: {
            //        required: true,
            //    },
            //    Zone_Emp: {
            //        required: true,
            //    },
            //    Zone_Id: {
            //        required: true,
            //    },
            //    Building_Id: {
            //        required: true,
            //    },
            //    HandOver_Type: {
            //        required: true,
            //    },
            //    Service_Provider: {
            //        required: true,
            //        minlength: 1
            //    },
            //Schedule_Type: {
            //    required: true,
            //},
            //},
            //messages: {
            //    Business_Unit: {
            //        required: "Please Select Business Unit",
            //    },
            //    Community: {
            //        required: "Please Select Community",
            //    },
            //    Master_Community_Id: {
            //        required: "Please Select Master Community",
            //    },
            //    Zone_Emp: {
            //        required: "Please Select Zone Representative",
            //    },
            //    Zone_Id: {
            //        required: "Please Select Zone",
            //    },
            //    Building_Id: {
            //        required: "Please Select Building",
            //    },
            //    HandOver_Type: {
            //        required: "Please Select Handover Type",
            //    },
            //    Service_Provider: {
            //        required: "Please Select Service Provider",
            //    },
            //},
            //errorPlacement: function (label, element) {
            //    label.addClass('mt-2 text-danger');
            //    label.insertAfter(element);
            //    $("#Business_Unit-error").removeClass('mt-2');
            //    $("#Business_Unit-error").addClass('m-10 text-danger');

            //    $("#Community_Id-error").removeClass('mt-2');
            //    $("#Community_Id-error").addClass('m-10 text-danger');

            //    $("#Building_Id-error").removeClass('mt-2');
            //    $("#Building_Id-error").addClass('m-10 text-danger');

            //    $("#Date_of_Audit-error").removeClass('mt-2');
            //    $("#Date_of_Audit-error").addClass('m-10 text-danger');

            //    $("#Zone_Id-error").removeClass('mt-2');
            //    $("#Zone_Id-error").addClass('m-10 text-danger');

            //    $("#Zone_Emp-error").removeClass('mt-2');
            //    $("#Zone_Emp-error").addClass('m-10 text-danger');

            //    $("#Service_Provider-error").removeClass('mt-2');
            //    $("#Service_Provider-error").addClass('m-10 text-danger');

            //    $("#HandOver_Type-error").removeClass('mt-2');
            //    $("#HandOver_Type-error").addClass('m-10 text-danger');
            //},
            //highlight: function (element, errorClass) {
            //    $(element).parent().addClass('has-danger')
            //    $(element).addClass('form-control-danger')
            //},
            submitHandler: function () {
                debugger
                var Obj = {
                    Insp_Service_Provider_Id: $("#Insp_Service_Provider_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Id: $("#Business_Unit").val(),
                    Community_Id: $("#Community_Id").val(),
                    Business_Unit_Type_Name: $("#Hs_Business_Unit_Type").val(),
                    Company_Name: $("#Company_Name").val(),
                    Inspected_By_Id: $("#Inspected_By_Id").val(),
                    Inspected_By_Name: $("#Inspected_By_Name").val(),
                    Inspection_Date: $("#Inspection_Date").val(),
                    Schedule_Type: $("#Schedule_Type").val(),
                    Service_Provider_Type: $("#Service_Provider_Type").val(),
                    Zone_In_Charge_Id: $("#Zone_In_Charge_Id").val(),
                    Service_Provider_Id: $("#Service_Provider_Id").val(),
                    Description: $("#Description").val()
                };
                $.ajax({
                    url: "/HandOverInspection/AddServiceProviderInspection",
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

$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form - Fire & Life Safety
        $("#F_Insp_FireLifeSafety").validate({
            rules: {
                Business_Unit: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Master_Community_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                Zone_Rep_Id: {
                    required: true,
                },
                Service_Provider_Id: {
                    required: true,
                },
            },
            messages: {
                Business_Unit: {
                    required: "Please Select Business Unit",
                },
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Community: {
                    required: "Please Select Community",
                },
                Master_Community_Id: {
                    required: "Please Select Master Community",
                },
                Building_Id: {
                    required: "Please Select Building",
                },
                Zone_Rep_Id: {
                    required: "Please Select Zone Representative",
                },
                Service_Provider_Id: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-10 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-10 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-10 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-10 text-danger');

                $("#Zone_Rep_Id-error").removeClass('mt-2');
                $("#Zone_Rep_Id-error").addClass('m-10 text-danger');

                $("#Service_Provider_Id-error").removeClass('mt-2');
                $("#Service_Provider_Id-error").addClass('m-10 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger;
                var QNS_ARR = [];
                var OBS_ARR = [];
                var subSno = 1;
                //let valid = $("#F_Insp_FireLifeSafety_Qns").valid();
                //if (!valid) {
                //    return false;
                //}
                $('#tblFireLifeSafetyInspQns tbody > tr').each(function () {
                    var varInsp_Question_Id = $(this).closest('tr').find('.Insp_Question_Id').val();
                    var varQns_Action = $(this).closest('tr').find('input[name="Action_' + subSno + '"]:checked').val();
                    var varPhoto_File_Path = $(this).closest('tr').find('.QnPhoto').val();
                    var varDetails_of_Evidence = $(this).closest('tr').find('.Details_of_Evidence').val();
                    var varRisk_Level = $(this).closest('tr').find('.Insp_Risk').val();
                    var varRisk_Description = $(this).closest('tr').find('.Risk_Description').val();
                    var varAction_Description = $(this).closest('tr').find('.QnDescription').val();
                    var varCategory = $(this).closest('tr').find('.Insp_Category').val();
                    var varSubCategory = $(this).closest('tr').find('.Sub_Category').val();
                    if (varPhoto_File_Path != "" && varPhoto_File_Path != null) {
                        //Photo_List_QN = [];
                        var val_UploadPhotos = varPhoto_File_Path.split(',');
                        var varPhoto_File_Path_QnFinal;
                        var i;
                        for (i = 0; i < val_UploadPhotos.length; i++) {
                            var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                            if (x != "/") {
                                //var Pdata = {};
                                varPhoto_File_Path_QnFinal = x;
                                //Pdata.Insp_Sub_Photo_Id = "0";
                                //Photo_List_QN.push(Pdata);
                            }
                        }
                    }
                    var data = {};
                    data.Insp_Questionnaires_Id = varInsp_Question_Id;
                    data.Qns_Action = varQns_Action;
                    data.Photo_File_Path = varPhoto_File_Path_QnFinal;
                    data.Details_of_Evidence = varDetails_of_Evidence;
                    data.Risk_Level = varRisk_Level;
                    data.Risk_Description = varRisk_Description;
                    data.Description_Action = varAction_Description;
                    data.Category = varCategory;
                    data.Sub_Category = varSubCategory;
                    QNS_ARR.push(data);
                    subSno++;
                });
                $('#tbl_Temp_Insp_Obser_FL tbody > tr').each(function () {
                    //Photo_List_UP = [];
                    debugger
                    var varInsp_Observation_Name = $(this).closest('tr').find('.HS_Observations').val();
                    var varHazard_Risk = $(this).closest('tr').find('.HS_HazardRisk').val();
                    var varRequirements = $(this).closest('tr').find('.HS_Requirements').val();
                    var varAction_Description = $(this).closest('tr').find('.HS_ActionRequired').val();
                    var varCategory = $(this).closest('tr').find('.HS_Category').val();
                    var varSub_Category = $(this).closest('tr').find('.HS_SubCategory').val();
                    var varRisk_Level = $(this).closest('tr').find('.HS_RiskLevel').val();
                    var varPhoto_File_Path = $(this).closest('tr').find('.UploadPhotos').val();
                    if (varPhoto_File_Path != "" && varPhoto_File_Path != null) {
                        //Photo_List_UP = [];
                        var val_UploadPhotos = varPhoto_File_Path.split(',');
                        var varPhoto_File_Path_ObsFinal;
                        var i;
                        for (i = 0; i < val_UploadPhotos.length; i++) {
                            var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                            if (x != "/") {
                                //var Pdata = {};
                                varPhoto_File_Path_ObsFinal = x;
                                //Pdata.Insp_Sub_Photo_Id = "0";
                                //Photo_List_UP.push(Pdata);
                            }
                        }
                    }
                    var data = {};
                    data.Insp_Questionnaires_Name = varInsp_Observation_Name;
                    data.Hazard_Risk = varHazard_Risk;
                    data.Requirements = varRequirements;
                    data.Description_Action = varAction_Description;
                    data.Category = varCategory;
                    data.Sub_Category = varSub_Category;
                    data.Risk_Level = varRisk_Level;
                    data.Photo_File_Path = varPhoto_File_Path_ObsFinal;
                    OBS_ARR.push(data);
                    subSno++;
                });
                var Obj = {
                    Insp_Request_Id: $("#Insp_Request_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Id: $("#Business_Unit").val(),
                    Community_Id: $("#Community_Id").val(),
                    Business_Unit_Type_Name: $("#Hs_Business_Unit_Type").val(),
                    Company_Name: $("#Company_Name").val(),
                    Access_Schedule: $("#Inspected_By_Id").val(),
                    Inspected_By_Name: $("#Inspected_By_Name").val(),
                    Inspection_Date: $("#Inspection_Date").val(),
                    Schedule_Type: $("#Schedule_Type").val(),
                    Fire_Life_Type: $("#Fire_Life_Type").val(),
                    Zone_Rep_Attended: $("#Zone_Rep_Attended").val(),
                    Service_Provider_Attended: $("#Service_Provider_Attended").val(),
                    Zone_Rep_Id: $("#Zone_Rep_Id").val(),
                    Service_Provider_Id: $("#Service_Provider_Id").val(),
                    Description: $("#Description").val(),
                    L_Insp_FireLifeSafety_Qns: QNS_ARR,
                    L_Insp_FireLifeSafety_Observation: OBS_ARR
                };
                $.ajax({
                    url: "/HandOverInspection/AddFireLifeSafetyInspection",
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