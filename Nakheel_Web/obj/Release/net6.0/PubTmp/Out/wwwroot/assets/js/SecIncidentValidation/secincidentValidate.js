$(function () {
    'use strict';
    debugger
    $("#Sec_Inc_Submit").click(function () {
        debugger
        $("#Sec_Incident_Report").validate({
            rules: {
                Sec_Inc_Cat: {
                    required: true,
                },
                Sec_Inc_Serv: {
                    required: true,
                },
                Incident_Date: {
                    required: true,
                },
                Incident_Time: {
                    required: true,
                },
                Sec_Complaint: {
                    required: true,
                },
                Sec_Method: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Building_Id: {
                    required:true,
                },
                Sec_Inc_Occur: {
                    required: true,
                },
                Sec_Report_By: {
                    required: true,
                },
                Sec_Report_To: {
                    required: true,
                },
                Sec_Assign_to: {
                    required: true,
                },
                Sec_DeptSection: {
                    required: true,
                },
                Sec_Action: {
                    required: true,
                },
                Sec_Comment: {
                    required: true,
                },
                Sec_Inc_Status: {
                    required: true,
                },
                Search_key: {
                    required: true,
                },
                Assistance: {
                    required: true,
                },
                Provider: {
                    required: true,
                },
                Enquiry: {
                    required: true,
                },
                Sec_Police: {
                    required: true,
                },
                Recovery: {
                    required: true,
                },
                Sec_CID: {
                    required: true,
                },
                Demage: {
                    required: true,
                },
                Details_Of_Demage: {
                    required: true,
                },
                Sec_Inc_Type: {
                    required: true,
                },
                Sec_Amb: {
                    required:true,
                },
                Injured: {
                    required:true,
                },
                Address: {
                    required:true,
                },
                Age: {
                    required:true,
                },
               
            },
            messages: {
                Sec_Inc_Cat: {
                    required: "Please Select Incident Category"
                },
                Incident_Date: {
                    required: "Please Select Incident Date"
                },
                Incident_Time: {
                    required: "Please Select Incident Time"
                },
                Sec_Inc_Serv: {
                    required: "Please Select Severity"
                },
                Sec_Complaint: {
                    required: "Please Enter Complaint"
                },
                Sec_Method: {
                    required: "Please Select Method of Report"
                },
                Zone_Id: {
                    required: "Please Select Zone"
                },
                Community: {
                    required: "Please Select Community"
                },
                Building_Id: {
                    required:"Please Select Building"
                },
                Sec_Inc_Occur: {
                    required: "Please Enter Incident Occur"
                },
                Sec_Report_By: {
                    required: "Please Enter Designation"
                },
                Sec_Report_To: {
                    required: "Please Select Reported To"
                },
                Sec_Assign_to: {
                    required: "Please Select Assign To"
                },
                Sec_DeptSection: {
                    required: "Please Select Department/Section"
                },
                Sec_Action: {
                    required: "Please Enter Action"
                },
                Sec_Comment: {
                    required: "Please Enter Comment"
                },
                Sec_Inc_Status: {
                    required: "Please Select Incident Status"
                },
                Search_key: {
                    required: "Please Enter HSE Mobile Number"
                },
                Assistance: {
                    required: "Please Choose Medical Assistance"
                },
                Provider: {
                    required: "Please Enter Provider"
                },
                Enquiry: {
                    required: "Please Choose Police Enquiry"
                },
                Sec_Police: {
                    required: "Required"
                },
                Recovery: {
                    required: "Required"
                },
                Sec_CID: {
                    required: "Required"
                },
                Demage: {
                    required: "Please Choose Demage"
                },
                Details_Of_Demage: {
                    required: "Please Enter Details of Demage"
                },
                Sec_Inc_Type: {
                    required: "Please Select Incident Type"
                },
                  Sec_Amb: {
                    required: "Please Choose Ambulance"
                },
                Injured: {
                    required: "Please Enter Name Injured Person"
                },
                Address: {
                    required: "Please Enter Address"
                },
                Age: {
                    required: "Please Enter Approximate Age"
                },
                Witnesses: {
                    required: "Please Enter Witnesses"
                }
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Sec_Inc_Cat-error").removeClass('mt-2');
                $("#Sec_Inc_Cat-error").addClass('m-100 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger;
                var Report_Emp_Id = $("#Sec_Report_To").val();
                var Assign_Emp_Id = $("#Sec_Assign_To").val();
                var varUploadedPhotos = $("#IncidentPhotos").val();
                var varUploadedVideos = $("#IncidentVideos").val();
                if (varUploadedPhotos != "" && varUploadedPhotos != null) {
                    var SecPhoto_List = [];
                    var val_UploadPhotos = varUploadedPhotos.split(',');
                    var Photo_File_Path;
                    var i;
                    for (i = 0; i < val_UploadPhotos.length; i++) {
                        var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                        if (x != "/") {
                            var Pdata = {};
                            Pdata.Photo_File_Path = x;
                            Pdata.Sec_Inc_Photo_Id = "0";
                            SecPhoto_List.push(Pdata);
                        }
                    }
                }
                if (varUploadedVideos != "" && varUploadedVideos != null) {
                    var SecVideo_List = [];
                    var val_UploadVideos = varUploadedVideos.split(',');
                    var Video_File_Path;
                    var i;
                    for (i = 0; i < val_UploadVideos.length; i++) {
                        var x = val_UploadVideos[i].replace(/[\(\)\[\]{}'"]/g, "");
                        if (x != "/") {
                            var Pdata = {};
                            Pdata.Video_File_Path = x;
                            Pdata.Sec_Inc_Video_Id = "0";
                            SecVideo_List.push(Pdata);
                        }
                    }
                }
                var SecFollowUp_ReportedEmp = [];
                var SecFollowUp_AssignedEmp = [];
                
                $(Report_Emp_Id).each(function (i, b) {
                    var item = {};
                    item.Emp_Id = b,
                        SecFollowUp_ReportedEmp.push(item);
                });
                $(Assign_Emp_Id).each(function (i, b) {
                    var item = {};
                    item.Emp_Id = b,
                        SecFollowUp_AssignedEmp.push(item);
                });
                var Obj = {
                    Sec_Inc_Report_Id: $("#Sec_Inc_Id").val(),
                    Sec_Inc_Category_Id: $("#Sec_Inc_Cat").val(),
                    Sec_Sub_Cat_Id: $("#Sec_Inc_Sub_Name").val(),
                    Sec_Inc_Severity: $("#Sec_Inc_Serv").val(),
                    Sec_Inc_Complaint: $("#Sec_Complaint").val(),
                    Sec_Inc_Method: $("#Sec_Method").val(),
                    Zone_Id: $("#CZone_Id").val(),
                    Community_Id: $("#CCommunity_Id").val(),
                    Building_Id: $("#CBuilding_Id").val(),
                    Location_Id: $("#Location").val(),
                    Sub_Location_Id: $("#Sub_Location").val(),
                    Sec_Inc_Occur: $(".ck-editor__editable").html(),
                    Sec_ReportedBy: $("#Reported_By").val(),
                    Incident_Date: $("#Incident_Date").val(),
                    Incident_Time: $("#Incident_Time").val(),
                    Sec_Related_Incident: $("#Sec_Related_Inc").val(),
                    L_Sec_Inc_Photos: SecPhoto_List, 
                    L_Sec_Inc_Videos: SecVideo_List,
                    L_M_Sec_FollowUp_ReportedTo: SecFollowUp_ReportedEmp, 
                    L_M_Sec_FollowUp_AssignedTo: SecFollowUp_AssignedEmp, 
                    Sec_Action: $("#Sec_ActionTaken").val(),
                    Sec_Comments: $("#Sec_Comment").val(),
                    Sec_Recommended: $("#Sec_Recommended").val(),
                    Sec_Escalation: $("input[type='radio'].Escalation:checked").val(),
                    Sec_Further_Action: $("input[type='radio'].Further_Action:checked").val(),
                    Sec_Inc_Status: $("#Sec_Inc_Status").val(),
                    Sec_Search_key: $("#Search_Key").val(), 
                    Sec_Medical_Assistance: $("input[type='radio'].Assistance:checked").val(),
                    Sec_Provider: $("#Provider").val(),
                    Sec_Police_Enquire: $("input[type='radio'].Enquiry:checked").val(),
                    Sec_Police: $("#Sec_Police").val(),
                    Sec_Recovery_Vechicle: $("#Recovery").val(),
                    Sec_CID: $("#Sec_CID").val(),
                    Sec_Ambulance: $("input[type='radio'].Sec_Amb:checked").val(),
                    Sec_Ambulance_no: $("#Ambulance_Vechicle").val(),
                    Sec_Injure_Person: $("#Injured").val(),
                    Sec_Address: $("#Address").val(),
                    Sec_Approxi_Age: $("#Age").val(),
                    Sec_Witnesses: $("#Witnesses").val(),
                    Sec_Damage: $("input[type='radio'].Damage:checked").val(),
                    Sec_Details_Damage: $("#Details_Of_Damage").val(),
                    Sec_DeptSection: $("#Sec_DeptSection").val(),
                };
                $.post("/SecurityIncident/AddSecurityIncReport", { model: Obj }, function (data) {
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
                });
                //$.ajax({
                //    url: "/SecurityIncident/AddSecurityIncReport",
                //    type: "POST",
                //    cache: false,
                //    data: { model: Obj },
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (data) {
                //        if (data == "200") {
                //            Swal.fire({
                //                position: 'top-end',
                //                icon: 'success',
                //                title: 'Added Successfully',
                //                showConfirmButton: false,
                //                timer: 1500
                //            }).then(function () {
                //                window.location.reload();
                //            });
                //        }
                //        else {
                //            Swal.fire({
                //                position: 'top-end',
                //                icon: 'Error',
                //                title: 'Error',
                //                showConfirmButton: false,
                //                timer: 1500
                //            });
                //        }
                //    },
                //})
            }
        })
    })
})