$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Incident_Report").validate({
            rules: {
                Company_Name: {
                    required: true,
                },
                Contact_Name: {
                    required: true,
                },
                Mobile_Number: {
                    required: true,
                    minlength: 10
                },
                Sel_Incident_Cat: {
                    required: true,
                },
                Injury_Type_list: {
                    required: true,
                    minlength: 1
                },
                Injury_Illness_list: {
                    required: true,
                    minlength: 1
                },
                Inc_Date: {
                    required: true,
                },
                Zone: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Inc_Time: {
                    required: true,
                },
                Building: {
                    required: true,
                },
                Business_Unit: {
                    required: true,
                },
                Injuredillness: {
                    required: true,
                    minlength: 1
                },
                InjuredType: {
                    required: true,
                    minlength: 1
                },
                Inc_Type_Id: {
                    required: true,
                },
                Vechicle_Accident_List: {
                    required: true,
                },
                Master_Community_Id: {
                    required: true,
                },
                Injury_TypeRemarks: {
                    required: true,
                },
                Injury_IllnesseRemarks: {
                    required: true,
                },
                VehicleAccidentRemarks: {
                    required: true,
                },
                Injuredillness_yes: {
                    required: true,
                },
                PropertyDamaged: {
                    required: true,
                },
                Insuranceclaim: {
                    required: true,
                },
                Inc_Fire_Location: {
                    required: true,
                },
                Inc_Business_Unit_Type: {
                    required: true,
                },
                Immediate_Actions: {
                    required: true,
                },
                Followrequired: {
                    required: true,
                },
                Required_Actions: {
                    required: true,
                },
                Location: {
                    required: true,
                },
                ApplicableReports: {
                    required: true,
                },
                Justification: {
                    required: true,
                },
                M_UnsafeActList: {
                    required: true,
                    minlength: 1
                },
                M_UnsafeConditionList: {
                    required: true,
                    minlength: 1
                },
                PersonalFactorList: {
                    required: true,
                    minlength: 1
                },
                SystemFactorList: {
                    required: true,
                    minlength: 1
                },
                MechanismInjuryList: {
                    required: true,
                    minlength: 1
                },
                AgencySourceInjuryList: {
                    required: true,
                    minlength: 1
                },
                Incident_Party: {
                    required: true,
                },
                Description_of_Incident_Party: {
                    required: true,
                },
                N_Service_Provider: {
                    required: true,
                },
            },
            messages: {
                Company_Name: {
                    required: "Please Enter Company Name",
                },
                Contact_Name: {
                    required: "Please Enter Contact Name",
                    minlength: "Name must consist of at least 5 characters"
                },
                Mobile_Number: {
                    required: "Please Enter Mobile Number",
                    minlength: "Name must consist of at least 10 characters"
                },
                Sel_Incident_Cat: {
                    required: "Please Select Incident Category",
                },
                Injury_Type_list: {
                    required: "Please Select Injury Type",
                },
                Injury_Illness_list: {
                    required: "Please Select Injury Illness",
                },
                Inc_Date: {
                    required: "Please Select Date",
                },
                Zone: {
                    required: "Please Select Zone",
                },
                Community: {
                    required: "Please Select Community",
                },
                Inc_Time: {
                    required: "Please Select Time",
                },
                Building: {
                    required: "Please Select Building",
                },
                Business_Unit: {
                    required: "Please Select Business Unit",
                },
                Injuredillness: {
                    required: "Please Select Injured illness",
                    minlength: 1
                },
                InjuredType: {
                    required: "Please Select Injured Type",
                    minlength: 1
                },
                Inc_Type_Id: {
                    required: "Please Select Incident Type",
                },
                Vechicle_Accident_List: {
                    required: "Please Select Vechicle Accident Type",
                },
                Master_Community_Id: {
                    required: "Please Select Master Community",
                },

                Injury_TypeRemarks: {
                    required: "Please Enter other name",
                },
                Injury_IllnesseRemarks: {
                    required: "Please Enter other name",
                },
                VehicleAccidentRemarks: {
                    required: "Please Enter other name",
                },
                Injuredillness_yes: {
                    required: "Please Select provide more Information is Required",
                },
                PropertyDamaged: {
                    required: "Please Select  Nahkeel Property Damaged Required",
                },
                Insuranceclaim: {
                    required: "Please Select insurance claim Required",
                },
                Inc_Fire_Location: {
                    required: "Please Select Fire Location Required",
                },
                Inc_Business_Unit_Type: {
                    required: "Please Select Business Unit Type Required",
                },
                Immediate_Actions: {
                    required: "Please Enter Immediate Actions Taken Required",
                },
                Followrequired: {
                    required: "Please Select Follow up required Required",
                },
                Required_Actions: {
                    required: "Please Enter Follow Up Action Required",
                },
                Location: {
                    required: "Please Enter Location Required",
                },
                ApplicableReports: {
                    required: "Please Select Applicable Reports",
                },
                Justification: {
                    required: "Please Enter Justification / Comments ",
                },
                M_UnsafeActList: {
                    required: "Please Select Unsafe Act",
                },
                M_UnsafeConditionList: {
                    required: "Please Select Unsafe Condition",
                },
                PersonalFactorList: {
                    required: "Please Select Personal Factor",
                },
                SystemFactorList: {
                    required: "Please Select System Factor",
                },
                MechanismInjuryList: {
                    required: "Please Select Mechanism Injury",
                },
                AgencySourceInjuryList: {
                    required: "Please Select Agency / Source Of Injury / Illness",
                },
                UnsafeActRemarks: {
                    required: "Please enter other name",
                },
                UnsafeCondRemarks: {
                    required: "Please enter other name",
                },
                PFRemarks: {
                    required: "Please enter other name",
                },
                SFRemarks: {
                    required: "Please enter other name",
                },
                MechanismRemarks: {
                    required: "Please enter other name",
                },
                AgencyRemarks: {
                    required: "Please enter other name",
                },
                Incident_Party: {
                    required: "Please Select Incident Party",
                },
                Description_of_Incident_Party: {
                    required: "Please enter Other Name",
                },
                N_Service_Provider: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Injury_Illness_list-error").removeClass('mt-2');
                $("#Injury_Illness_list-error").addClass('m-10 text-danger');


                $("#Injury_Type_list-error").removeClass('mt-2');
                $("#Injury_Type_list-error").addClass('m-10 text-danger');

                $("#Vechicle_Accident_List-error").removeClass('mt-2');
                $("#Vechicle_Accident_List-error").addClass('m-10 text-danger');

                $("#Inc_Category_Id-error").removeClass('mt-2');
                $("#Inc_Category_Id-error").addClass('m-100 text-danger');

                $("#Inc_Type_Id-error").removeClass('mt-2');
                $("#Inc_Type_Id-error").addClass('m-100 text-danger');


                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-100 text-danger');

                $("#Zone-error").removeClass('mt-2');
                $("#Zone-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#Master_Community_Id-error").removeClass('mt-2');
                $("#Master_Community_Id-error").addClass('m-100 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-101 text-danger');
            },

            submitHandler: function () {
                debugger
                var InjurPerson_List = [];
                var Injury_Type_List = [];
                var Nature_of_Injury = [];
                var Vechicle_Accident_List = [];
                var Is_Injury_Illness = "";
                var Injured_Person = "";

                if ($("input[type='radio'].injuryillness").is(':checked')) {
                    Is_Injury_Illness = $("input[type='radio'].injuryillness:checked").val();
                }
                if ($("input[type='radio'].injuryillness_yes").is(':checked')) {
                    Injured_Person = $("input[type='radio'].injuryillness_yes:checked").val();
                }
                $('.clsInjury_Type:checked').each(function () {
                    var varInc_Injury_Type_Id = "0";
                    var varInc_Id = "0";
                    var varInjury_Type_Id = $(this).val();
                    var varRemarks = $("#txtInjury_Typeothers").val();

                    var data = {};
                    data.Inc_Injury_Type_Id = varInc_Injury_Type_Id;
                    data.InjuredPersonName = varInc_Id;
                    data.Injury_Type_Id = varInjury_Type_Id;
                    data.Remarks = varRemarks;
                    Injury_Type_List.push(data);
                });

                $('.clsInjury_Illness:checked').each(function () {
                    var varInc_Nature_Injury_Id = "0";
                    var varInc_Id = "0";
                    var varNature_Injury_Id = $(this).val();
                    var varRemarks = $("#txtInjury_Illnesseothers").val();

                    var data = {};
                    data.Inc_Nature_Injury_Id = varInc_Nature_Injury_Id;
                    data.InjuredPersonName = varInc_Id;
                    data.Nature_Injury_Id = varNature_Injury_Id;
                    data.Remarks = varRemarks;
                    Nature_of_Injury.push(data);
                });

                $('#tblIncidentInjuredPartsList tbody > tr').each(function () {
                    debugger
                    var varIs_Injury_Persons_Id = $(this).closest('tr').find('.Is_Injury_Persons_Id').text();
                    var varInjuredPersonName = $(this).closest('tr').find('.Par_InjuredPersonName').text();
                    var varInjuredPersonContactNo = $(this).closest('tr').find('.Par_InjuredPersonContactNo').text();
                    var varInjuredBodyParts = $(this).closest('tr').find('.InjuredPersonbodyList').text();
                    var varIs_Injury_PersonList = $(this).closest('tr').find('.Is_Injury_PersonList').text();
                    var data = {};
                    data.Persons_Name = varInjuredPersonName;
                    data.Persons_ContactNo = varInjuredPersonContactNo;
                    data.BodyParts_List = varInjuredBodyParts;
                    data.Persons_Id = varIs_Injury_Persons_Id;
                    data.Injured_Type = varIs_Injury_PersonList;
                    InjurPerson_List.push(data);
                });

                $('.clsVehicleAccident_Type:checked').each(function () {
                    var varInc_VehicleAccident_Id = "0";
                    var varInc_Id = "0";
                    var varVehicle_Accident_Id = $(this).val();
                    var varRemarks = $("#txtVehicleAccidentothers").val();

                    var data = {};
                    data.Inc_VehicleAccident_Id = varInc_VehicleAccident_Id;
                    data.Inc_Id = varInc_Id;
                    data.Vehicle_Accident_Id = varVehicle_Accident_Id;
                    data.Remarks = varRemarks;
                    Vechicle_Accident_List.push(data);
                });

                var lat = "0.00";
                var long = "0.00";


                if ($("#Lat").val() != "" || $("#Long").val() != "") {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                }

                var Obj = {
                    Inc_Id: $("#Inc_Id").val(),
                    Inc_Category_Id: $("#Inc_Category_Id").val(),
                    Inc_Type_Id: $("#Inc_Type_Id").val(),
                    Inc_Date: $("#Inc_Date").val(),
                    Inc_Time: $("#Inc_Time").val(),
                    Zone: $("#Zone").val(),
                    Building: $("#Building_Id").val(),
                    Business_Unit: $("#Business_Unit").val(),
                    Company_Name: $("#Company_Name").val(),
                    Contact_Name: $("#Contact_Name").val(),
                    Mobile_Number: $("#Mobile_Number").val(),
                    Inc_Description: $("#Description").val(),
                    Community_Id: $("#Community_Id").val(),
                    Master_Community_Id: $("#Master_Community_Id").val(),
                    Loc_Latitude: lat,
                    Loc_Longitude: long,
                    Is_Injury_Illness: Is_Injury_Illness,
                    Injured_Person: Injured_Person,
                    Is_PropertyDamaged: $("input[type='radio'].PropertyDamaged:checked").val(),
                    Is_InsuranceClaim: $("input[type='radio'].Insuranceclaim:checked").val(),
                    Inc_Business_Unit_Type: $("#Inc_Business_Unit_Type").val(),
                    Inc_Fire_Location: $("#Inc_Fire_Location").val(),
                    Immediate_Action: $("#Immediate_Actions").val(),
                    Is_Follow_required: $('input[name=Followrequired]:checked').val(),
                    Actions_Taken_Description: $("#Required_Actions").val(),
                    Incident_Party: $("#Incident_Party").val(),
                    Description_of_Incident_Party: $("#Description_of_Incident_Party").val(),
                    Service_Provider_Id: $("#N_Service_Provider").val(),
                    Inc_Remarks_1: $("#Inc_Remarks_1").val(),
                    Inc_Remarks_2: $("#Inc_Remarks_2").val(),
                    Inc_Remarks_3: $("#Inc_Remarks_3").val(),
                    L_M_Inc_Persons_Injured: InjurPerson_List,
                    L_Inc_Inve_Injury_Type: Injury_Type_List,
                    L_Inc_Inve_Nature_Injury: Nature_of_Injury,
                    L_M_Inc_Vehicle_Accident: Vechicle_Accident_List
                };
                $.ajax({
                    url: "/Incident/AddIncidentReporting",
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

        $("#F_AddBuilding").validate({
            rules: {
                Zone_Id: {
                    required: true,
                },
                Community_Id: {
                    required: true,
                },
                Building_Name: {
                    required: true,
                    minlength: 2
                },
                Building_Description: {
                    required: true,
                },
            },
            messages: {
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Community_Id: {
                    required: "Please  Select Community",
                },
                Building_Name: {
                    required: "Please Enter Building Name",
                },
                Building_Description: {
                    required: "Please Enter Building Description",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Building_List = [];

                $('#tblBuildingNameList tbody > tr').each(function () {
                    debugger
                    var varBuildingId = $(this).closest('tr').find('.BuildingId').val();
                    var varSubBuildingId = $(this).closest('tr').find('.SubBuildingId').val();
                    var varBuildingName = $(this).closest('tr').find('.BuildingName').val();
                    var varBuildingDescription = $(this).closest('tr').find('.BuildingDescription').val();
                    var data = {};
                    data.Building_Id = varBuildingId;
                    data.Sub_Building_Id = varSubBuildingId;
                    data.Building_Name = varBuildingName;
                    data.Building_Description = varBuildingDescription;
                    Building_List.push(data);
                });

                var Obj = {
                    Building_Id: $("#Building_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    L_M_Building: Building_List
                };
                $.ajax({
                    url: "/Master/AddBuilding",
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

        $("#F_AddMasterCommunity").validate({
            rules: {
                Zone_Id: {
                    required: true,
                },
                Master_Community_Name: {
                    required: true,
                    minlength: 2
                },
                Master_Community_Name_Description: {
                    required: true,
                },
            },
            messages: {
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Master_Community_Name: {
                    required: "Please Enter Master Community Name",
                },
                Master_Community_Name_Description: {
                    required: "Please Enter Master Community Description",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Master_Community_List = [];

                $('#tblMasterCommNameList tbody > tr').each(function () {
                    debugger
                    var varMasterCommunity_Id = $(this).closest('tr').find('.MasterCommunity_Id').val();
                    var varSubMasterCommunity_Id = $(this).closest('tr').find('.Sub_MasterCommunity_Id').val();
                    var varMaster_Community_Name = $(this).closest('tr').find('.Master_Community_Name').val();
                    var varMaster_Community_Name_Description = $(this).closest('tr').find('.Master_Community_Name_Description').val();
                    var data = {};
                    data.MasterCommunity_Id = varMasterCommunity_Id;
                    data.Sub_MasterCommunity_Id = varSubMasterCommunity_Id;
                    data.MasterCommunity_Name = varMaster_Community_Name;
                    data.MasterCommunity_Description = varMaster_Community_Name_Description;
                    Master_Community_List.push(data);
                });

                var Obj = {
                    MasterCommunity_Id: $("#MasterCommunity_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    L_M_MasterCommunity_List: Master_Community_List
                };
                $.ajax({
                    url: "/Master/AddMasterCommunity",
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

        $("#F_Incident_Investigation_Req").validate({
            rules: {
                InvestigationReq: {
                    required: true,
                },
            },
            messages: {
                InvestigationReq: {
                    required: "Please Select Investigation required",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Is_Investigation_req= $("input[type='radio'].Investigationval:checked").val();

                var Obj = {
                    Inc_Id: $("#Inv_Inc_Id").val(),
                    Status: "2",
                    Is_Investigation_Req: Is_Investigation_req
                };
                $.ajax({
                    url: "/Incident/UpdateIncStatus",
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
                                title: 'Update Successfully',
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

        $("#F_AddLocation").validate({
            
            rules: {
                Zone_Id: {
                    required: true,
                },
                Community_Id: {
                    required: true,
                },
                Building_Id: {
                    required:true,
                },
                Sub_Location_Name: {
                    required: true,
                   
                },
                Location_Description: {
                    required: true,
                },
            },
            messages: {
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Community_Id: {
                    required: "Please  Select Community",
                },
                Building_Id: {
                    required: "Please  Select Building",
                },
                Sub_Location_Name: {
                    required: "Please Enter Location Name",
                },
                Location_Description: {
                    required: "Please Enter Location Description",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Location_List = [];

                $('#tbllocationNameList tbody > tr').each(function () {
                    debugger
                    var varLocationId = $(this).closest('tr').find('.LocationId').val();
                    var varSubLocationId = $(this).closest('tr').find('.SubLocationId').val();
                    var varLocationName = $(this).closest('tr').find('.SubLocationName').val();
                    var varLocationDescription = $(this).closest('tr').find('.LocationDescription').val();
                    var data = {};
                    data.Location_Id = varLocationId;
                    data.Sub_Location_Id = varSubLocationId;
                    data.Sub_Location_Name = varLocationName;
                    data.Description = varLocationDescription;
                    Location_List.push(data);
                });

                var Obj = {
                    Location_Id: $("#Location_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Building_Id:$("#Building_Id").val(),
                    Location_Name: $("#Location_name").val(),
                    L_M_Location: Location_List
                };
                $.ajax({
                    url: "/Master/AddLocation",
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