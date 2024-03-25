$(function () {
    'use strict';
    $("#Confined_Submit").click(function () {
        $("#Confined_Space_Permit").validate({
            rules: {
                Business_Unit_Id: {
                    required: true,
                },
                Community_Id: {
                    required: true,
                },
                Con_Business_Unit_Type: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                C_Company_Name: {
                    required: true,
                },
                C_Tittle_No: {
                    required: true,
                },
                C_Competent: {
                    required: true,
                },
                C_Description: {
                    required: true,
                },
                C_Approve: {
                    required: true,
                },
                Confined_Ques: {
                    required: true,
                },
                C_From_Date: {
                    required: true,
                },
                C_Night_Shedule: {
                    required: true,
                },
                C_To_Date: {
                    required: true,
                },
                C_Validate: {
                    required: true,
                },
                //Contractor_name: {
                //    required: true,
                //},
                Con_Start: {
                    required: true,
                },
                Con_End: {
                    required: true,
                },
                Contractor_Id: {
                    required: true,
                },
                Method_files: {
                    required: true,
                },
                Risk_files: {
                    required: true,
                },
                Emr_files: {
                    required: true,
                },
                Hse_files: {
                    required: true,
                },
                files: {
                    required: true,
                },
                C_Additional_Pre: {
                    required: true,
                },
                Hse_Name: {
                    required: true,
                },
                Hse_Number: {
                    required: true,
                },
                Signed_files: {
                    required: true,
                },
                Accept_Name: {
                    required: true,
                },
                Accept_Desig: {
                    required: true,
                },
               
            },
            messages: {
                Business_Unit_Id: {
                    required: "Please select BusinessUnit"
                },
                Community_Id: {
                    required: "Please Select Community"
                },
                Con_Business_Unit_Type: {
                    required: "Please Select Business Unit Type"
                },
                Zone_Id: {
                    required: "Please Select Zone"
                },
                Building_Id: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Contractor Title/No"
                },
                C_Competent: {
                    required: "Please Select Competent Person"
                },
                C_Description: {
                    required: "Please Enter Description"
                },
                C_Approve: {
                    required: "Please Choose Approve Method"
                },
                Confined_Ques: {
                    required: "Please Choose Questionnaries"
                },
                C_From_Date: {
                    required: "Please Choose From date"
                },
                C_Night_Shedule: {
                    required: "Please Choose Night shedule"
                },
                C_To_Date: {
                    required: "Please Choose To date"
                },
                C_Validate: {
                    required: "Please Choose Validate"
                },
                //Contractor_name: {
                //    required: "Please Choose Contractor Name",
                //},
                Con_Start: {
                    required: "Please Select Contract Start Date",
                },
                Con_End: {
                    required: "Please Select Contract End Date",
                },
                Contractor_Id: {
                    required: "Please Select DMS Number",
                },
                Method_files: {
                    required: "Please Choose File",
                },
                Risk_files: {
                    required: "Please Choose File",
                },
                Emr_files: {
                    required: "Please Choose File",
                },
                Hse_files: {
                    required: "Please Choose File",
                },
                files: {
                    required: "Please Choose File",
                },
                C_Additional_Pre: {
                    required: "Please Enter Additional Precautions",
                },
                Hse_Name: {
                    required: "Please Enter HSE Officer Name",
                },
                Hse_Number: {
                    required: "Please Enter HSE Mobile Number",
                },
                Signed_files: {
                    required: "Please Choose File",
                },
                Accept_Name: {
                    required: "Please Enter Name",
                },
                Accept_Desig: {
                    required: "Please Enter Designation",
                },
               
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#C_Competent-error").removeClass('mt-2');
                $("#C_Competent-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#search_input_1-error").removeClass('mt-2');
                $("#search_input_1-error").addClass('m-txt-req_11 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-txt-req text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];
                var Method_file_ARR = [];
                var Emr_file_ARR = [];
                var Risk_file_ARR = [];
                var HSE_file_ARR = [];
                var Staff_file_ARR = [];
                var Signed_file_ARR = [];

                $('#Tbl_CSP tbody > tr').each(function () {
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varHSEWorkId
                    data.CSP_Ques_Name = varConfine_Ques_Ques;
                    data.CSP_Ques_Radio_Btn = RADIOVal;
                    data.CSP_Remarks = Standard_Remark;
                    data.CreatedBy = $("#CreatedBy").val();
                    QNS_ARR.push(data);
                });
                //var varDateTime = $("#C_Date_Time_Obs").val().replace("T", " ");
                var varDateTimeFrom = $("#C_From_Date").val();
                var varDateTimeTo = $("#C_To_Date").val();
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var Loc_Address = $("#search_input").val();
                var ExLocAddress = $("#search_input_1").val();
                var varMethod_file = $(".MethodUploadPhotos").val();
                var varRisk_file = $(".RiskUploadPhotos").val();
                var varEmr_file = $(".EmrUploadPhotos").val();
                var varHse_file = $(".HSEUploadPhotos").val();
                var varstaff_file = $(".UploadPhotos").val();
                var varSigned_file = $(".SignedUploadPhotos").val();
                var varcon_startDate = $("#Con_Start").val().replace("T", " ");
                var varcon_endDate = $("#Con_End").val().replace("T", " ");
                var varDms_Number = $("#Contractor_Id").val();
                // var varmethodPdf = $(".Method_Pdf_Files").val();

                if ((varMethod_file != "") && (varMethod_file != undefined) && (varMethod_file != null)) {
                    var valNew = varMethod_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Method_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Method_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Method_file_ARR.push(File_data);
                    }
                }
                if ((varRisk_file != "") && (varRisk_file != undefined) && (varRisk_file != null)) {
                    var valNew = varRisk_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Risk_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Risk_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Risk_file_ARR.push(File_data);
                    }
                }
                if ((varEmr_file != "") && (varEmr_file != undefined) && (varEmr_file != null)) {
                    var valNew = varEmr_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Emr_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Emr_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Emr_file_ARR.push(File_data);
                    }
                }
                if ((varHse_file != "") && (varHse_file != undefined) && (varHse_file != null)) {
                    var valNew = varHse_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Hse_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Hse_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        HSE_file_ARR.push(File_data);
                    }
                }
                if ((varstaff_file != "") && (varstaff_file != undefined) && (varstaff_file != null)) {
                    var valNew = varstaff_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Staff_file_ARR.push(File_data);
                    }
                }
                if ((varSigned_file != "") && (varSigned_file != undefined) && (varSigned_file != null)) {
                    var valNew = varSigned_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Sign_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Sign_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Signed_file_ARR.push(File_data);
                    }
                }

                var History_ARR = [];
                var His_Data = {};

                His_Data.CSP_Id = $("#Confined_ID").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);


                if ($("#Lat").val() != "" || $("#Long").val() != "" || $("#search_input").val() || $("#search_input_1").val()) {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                    Loc_Address = $("#search_input").val();
                    ExLocAddress = $("#search_input_1").val();

                }
                //if (Loc_Address == "" || Loc_Address == null) {
                //    toastr.error("Please select the location name!", "Error");
                //}
                //if (ExLocAddress == "" || ExLocAddress == null) {
                //    toastr.error("Please enter the exact location!", "Error");
                //}
                var Declare_chk = $("input[type='checkbox'].Declare_Chk:checked").val()
                if (Declare_chk == "" || Declare_chk == null) {
                    toastr.error("Please Select Service Providers Declaration Checkbox!", "Error");
                    return false;
                }
                var Obj = {
                    CSP_Id: $("#Confined_ID").val(),
                    //Date_Time_of_Observation: varDateTime,
                    Business_Unit_Id: $("#Business_Unit_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Type: $("#Con_Business_Unit_Type").val(),
                    Latitude: lat,
                    Longitude: long,
                    Location_Address: Loc_Address,
                    Exact_Loc_Address: ExLocAddress,
                    Company_Name: $("#C_Company_Name").val(),
                    Contrator_Title_No: $("#C_Tittle_No").val(),
                    Competent_Person: $("#C_Competent").val(),
                    Contractor_Name: $("#Contractor_name").val(),
                    Name: $("#C_Name").val(),
                    Position: $("#C_Position").val(),
                    Contact: $("#C_Contact").val(),
                    Email_Id: $("#C_Email_Id").val(),
                    Date_and_Time: $("#C_Date_Time").val(),
                    Description_of_Work: $("#C_Description").val(),
                    Approved_Method: $("input[type='radio'].C_Approve:checked").val(),
                    Work_Duration_From_Date: varDateTimeFrom,
                    Work_Duration_To_Date: varDateTimeTo,
                    Night_Schedule: $("input[type='radio'].C_Night_Shedule:checked").val(),
                    Additional_Precautions: $("#C_Additional_Pre").val(),
                    Status: "1",
                    Contractor_Start_Date: varcon_startDate,
                    Contractor_End_Date: varcon_endDate,
                    DMS_No_Id: varDms_Number,
                    Competent_Number: $("#Comp_Number").val(),
                    HSE_Officer_Name: $("#Hse_Name").val(),
                    HSE_Mobile_Number: $("#Hse_Number").val(),
                    Declare_Name: $("#Accept_Name").val(),
                    Declare_Designation: $("#Accept_Desig").val(),
                    Declare_Chk: $("input[type='checkbox'].Declare_Chk:checked").val(),
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR,
                    _Method_Statement_Files: Method_file_ARR,
                    _Risk_Assess_Files: Risk_file_ARR,
                    _Emr_Plan_Files: Emr_file_ARR,
                    _HSE_Plan_Files: HSE_file_ARR,
                    _Staff_Comptency_Files: Staff_file_ARR,
                    _Sp_Signed_Files: Signed_file_ARR
                };
                $.ajax({
                    url: "/ControlOfWork/AddConfinedSpaceWorkPermit",
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
                })
            }
        })
    })

    //Electrical Permit Work
    $("#Electrical_Submit").click(function () {
        $("#Electrical_Work_Permit").validate({
            rules: {
                Ewp_Business_Unit_Id: {
                    required: true,
                },
                Ewp_Community_Id: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Ewp_Building_Id: {
                    required: true,
                },
                C_Company_Name: {
                    required: true,
                },
                C_Tittle_No: {
                    required: true,
                },
                C_Competent: {
                    required: true,
                },
                C_Description: {
                    required: true,
                },
                C_Approve: {
                    required: true,
                },
                Confined_Ques: {
                    required: true,
                },
                C_From_Date: {
                    required: true,
                },
                C_Night_Shedule: {
                    required: true,
                },
                C_To_Date: {
                    required: true,
                },
                C_Validate: {
                    required: true,
                },
                //Contractor_name: {
                //    required: true,
                //},
                Con_Start: {
                    required: true,
                },
                Con_End: {
                    required: true,
                },
                Contractor_Id: {
                    required: true,
                },
                Method_files: {
                    required: true,
                },
                Risk_files: {
                    required: true,
                },
                Emr_files: {
                    required: true,
                },
                Hse_files: {
                    required: true,
                },
                files: {
                    required: true,
                },
                C_Additional_Pre: {
                    required: true,
                },
                Hse_Name: {
                    required: true,
                },
                Hse_Number: {
                    required: true,
                },
                Signed_files: {
                    required: true,
                },
                Accept_Name: {
                    required: true,
                },
                Accept_Desig: {
                    required: true,
                },
            },
            messages: {
                Ewp_Business_Unit_Id: {
                    required: "Please Select BusinessUnit"
                },
                Ewp_Community_Id: {
                    required: "Please Select Community"
                },

                Zone_Id: {
                    required: "Please Select Zone"
                },
                Ewp_Building_Id: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Contractor Title/No"
                },
                C_Competent: {
                    required: "Please Select Competent Person"
                },
                C_Description: {
                    required: "Please Enter Description"
                },
                C_Approve: {
                    required: "Please Choose Approve Method"
                },
                Confined_Ques: {
                    required: "Please Choose Questionnaries"
                },
                C_From_Date: {
                    required: "Please Choose From date"
                },
                C_Night_Shedule: {
                    required: "Please Choose Night shedule"
                },
                C_To_Date: {
                    required: "Please Choose To date"
                },
                C_Validate: {
                    required: "Please Choose Validate"
                },
                //Contractor_name: {
                //    required: "Please Choose Contractor Name",
                //},
                Con_Start: {
                    required: "Please Select Contract Start Date",
                },
                Con_End: {
                    required: "Please Select Contract End Date",
                },
                Contractor_Id: {
                    required: "Please Select DMS Number",
                },
                Method_files: {
                    required: "Please Choose File",
                },
                Risk_files: {
                    required: "Please Choose File",
                },
                Emr_files: {
                    required: "Please Choose File",
                },
                Hse_files: {
                    required: "Please Choose File",
                },
                files: {
                    required: "Please Choose File",
                },
                C_Additional_Pre: {
                    required: "Please Enter Additional Precautions",
                },
                Hse_Name: {
                    required: "Please Enter HSE Officer Name",
                },
                Hse_Number: {
                    required: "Please Enter HSE Mobile Number",
                },
                Signed_files: {
                    required: "Please Choose File",
                },
                Accept_Name: {
                    required: "Please Enter Name",
                },
                Accept_Desig: {
                    required: "Please Enter Designation",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Ewp_Community_Id-error").removeClass('mt-2');
                $("#Ewp_Community_Id-error").addClass('m-100 text-danger');
          
                $("#C_Competent-error").removeClass('mt-2');
                $("#C_Competent-error").addClass('m-100 text-danger');

                $("#Ewp_Building_Id-error").removeClass('mt-2');
                $("#Ewp_Building_Id-error").addClass('m-100 text-danger');

                $("#search_input_1-error").removeClass('mt-2');
                $("#search_input_1-error").addClass('m-txt-req_11 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-txt-req text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];
                var Method_file_ARR = [];
                var Emr_file_ARR = [];
                var Risk_file_ARR = [];
                var HSE_file_ARR = [];
                var Staff_file_ARR = [];
                var Signed_file_ARR = [];
                $('#Tbl_Electrical tbody > tr').each(function () {
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varHSEWorkId
                    data.EWP_Ques_Name = varConfine_Ques_Ques;
                    data.EWP_Ques_Radio_Btn = RADIOVal;
                    data.EWP_Remarks = Standard_Remark;
                    data.CreatedBy = $("#CreatedBy").val();
                    QNS_ARR.push(data);
                });
                //var varDateTime = $("#C_Date_Time_Obs").val().replace("T", " ");
                var varDateTimeFrom = $("#C_From_Date").val();
                var varDateTimeTo = $("#C_To_Date").val();
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var Loc_Address = $("#search_input").val();
                var ExLocAddress = $("#search_input_1").val();
                var varMethod_file = $(".MethodUploadPhotos").val();
                var varRisk_file = $(".RiskUploadPhotos").val();
                var varEmr_file = $(".EmrUploadPhotos").val();
                var varHse_file = $(".HSEUploadPhotos").val();
                var varstaff_file = $(".UploadPhotos").val();
                var varSigned_file = $(".SignedUploadPhotos").val();
                var varcon_startDate = $("#Con_Start").val().replace("T", " ");
                var varcon_endDate = $("#Con_End").val().replace("T", " ");
                var varDms_Number = $("#Contractor_Id").val();
                // var varmethodPdf = $(".Method_Pdf_Files").val();

                if ((varMethod_file != "") && (varMethod_file != undefined) && (varMethod_file != null)) {
                    var valNew = varMethod_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Method_File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.Method_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Method_file_ARR.push(File_data);
                    }
                }
                if ((varRisk_file != "") && (varRisk_file != undefined) && (varRisk_file != null)) {
                    var valNew = varRisk_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Risk_File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.Risk_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Risk_file_ARR.push(File_data);
                    }
                }
                if ((varEmr_file != "") && (varEmr_file != undefined) && (varEmr_file != null)) {
                    var valNew = varEmr_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Emr_File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.Emr_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Emr_file_ARR.push(File_data);
                    }
                }
                if ((varHse_file != "") && (varHse_file != undefined) && (varHse_file != null)) {
                    var valNew = varHse_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Hse_File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.Hse_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        HSE_file_ARR.push(File_data);
                    }
                }
                if ((varstaff_file != "") && (varstaff_file != undefined) && (varstaff_file != null)) {
                    var valNew = varstaff_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Staff_file_ARR.push(File_data);
                    }
                }
                if ((varSigned_file != "") && (varSigned_file != undefined) && (varSigned_file != null)) {
                    var valNew = varSigned_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Sign_File_Id = "0";
                        File_data.EWP_Id = $("#Electrical_ID").val();
                        File_data.Sign_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Signed_file_ARR.push(File_data);
                    }
                }

                var History_ARR = [];
                var His_Data = {};

                His_Data.EWP_Id = $("#Electrical_ID").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);

                var Declare_chk = $("input[type='checkbox'].Declare_Chk:checked").val()
                if (Declare_chk == "" || Declare_chk == null) {
                    toastr.error("Please Select Service Providers Declaration Checkbox!", "Error");
                    return false;
                }
             
                var Obj = {
                    EWP_Id: $("#Electrical_ID").val(),
                    Business_Unit_Id: $("#Ewp_Business_Unit_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Ewp_Community_Id").val(),
                    Building_Id: $("#Ewp_Building_Id").val(),
                    Latitude: $("#Lat").val(),
                    Longitude: $("#Long").val(),
                    Location_Address: $("#search_input").val(),
                    Exact_Loc_Address: $("#search_input_1").val(),
                    Company_Name: $("#C_Company_Name").val(),
                    Contrator_Title_No: $("#C_Tittle_No").val(),
                    Competent_Person: $("#C_Competent").val(),
                    Contractor_Name: $("#Contractor_name").val(),
                    Name: $("#C_Name").val(),
                    Position: $("#C_Position").val(),
                    Contact: $("#C_Contact").val(),
                    Email_Id: $("#C_Email_Id").val(),
                    Date_and_Time: $("#C_Date_Time").val(),
                    Description_of_Work: $("#C_Description").val(),
                    Work_Duration_From_Date: varDateTimeFrom,
                    Work_Duration_To_Date: varDateTimeTo,
                    Additional_Precautions: $("#C_Additional_Pre").val(),
                    Status: "1",
                    Contractor_Start_Date: varcon_startDate,
                    Contractor_End_Date: varcon_endDate,
                    DMS_No_Id: varDms_Number,
                    Competent_Number: $("#Comp_Number").val(),
                    HSE_Officer_Name: $("#Hse_Name").val(),
                    HSE_Mobile_Number: $("#Hse_Number").val(),
                    Declare_Name: $("#Accept_Name").val(),
                    Declare_Designation: $("#Accept_Desig").val(),
                    Declare_Chk: $("input[type='checkbox'].Declare_Chk:checked").val(),
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR,
                    _Method_Statement_Files: Method_file_ARR,
                    _Risk_Assess_Files: Risk_file_ARR,
                    _Emr_Plan_Files: Emr_file_ARR,
                    _HSE_Plan_Files: HSE_file_ARR,
                    _Staff_Comptency_Files: Staff_file_ARR,
                    _Sp_Signed_Files: Signed_file_ARR
                };
                $.ajax({
                    url: "/ControlOfWork/Add_Electrical_Work_Req",
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
                })
            }
        })
    })

    //Hot Work Permit
    $("#Hot_Work_Submit").click(function () {
        $("#Form_Hot_Work_Permit").validate({
            rules: {
                Hw_Business_Unit_Id: {
                    required: true,
                },
                Hw_Community_Id: {
                    required: true,
                },
                Hw_Zone_Id: {
                    required: true,
                },
                Hw_Building_Id: {
                    required: true,
                },
                C_Company_Name: {
                    required: true,
                },
                C_Tittle_No: {
                    required: true,
                },
                C_Competent: {
                    required: true,
                },
                C_Description: {
                    required: true,
                },
                C_Approve: {
                    required: true,
                },
                Confined_Ques: {
                    required: true,
                },
                C_From_Date: {
                    required: true,
                },
                C_Night_Shedule: {
                    required: true,
                },
                C_To_Date: {
                    required: true,
                },
                C_Validate: {
                    required: true,
                },
                //Contractor_name: {
                //    required: true,
                //},
                Con_Start: {
                    required: true,
                },
                Con_End: {
                    required: true,
                },
                Contractor_Id: {
                    required: true,
                },
                Method_files: {
                    required: true,
                },
                Risk_files: {
                    required: true,
                },
                Emr_files: {
                    required: true,
                },
                Hse_files: {
                    required: true,
                },
                files: {
                    required: true,
                },
                C_Additional_Pre: {
                    required: true,
                },
                Hse_Name: {
                    required: true,
                },
                Hse_Number: {
                    required: true,
                },
                Signed_files: {
                    required: true,
                },
                Accept_Name: {
                    required: true,
                },
                Accept_Desig: {
                    required: true,
                },
            },
            messages: {
                Hw_Business_Unit_Id: {
                    required: "Please select BusinessUnit"
                },
                Hw_Community_Id: {
                    required: "Please Select Community"
                },

                Hw_Zone_Id: {
                    required: "Please Select Zone"
                },
                Hw_Building_Id: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Contractor Title/No"
                },
                C_Competent: {
                    required: "Please Select Competent Person"
                },
                C_Description: {
                    required: "Please Enter Description"
                },
                C_Approve: {
                    required: "Please Choose Approve Method"
                },
                Confined_Ques: {
                    required: "Please Choose Questionnaries"
                },
                C_From_Date: {
                    required: "Please Choose From date"
                },
                C_Night_Shedule: {
                    required: "Please Choose Night shedule"
                },
                C_To_Date: {
                    required: "Please Choose To date"
                },
                C_Validate: {
                    required: "Please Choose Validate"
                },
                //Contractor_name: {
                //    required: "Please Choose Contractor Name",
                //},
                Con_Start: {
                    required: "Please select Contract Start Date",
                },
                Con_End: {
                    required: "Please select Contract End Date",
                },
                Contractor_Id: {
                    required: "Please select DMS Number",
                },
                Method_files: {
                    required: "Please Choose File",
                },
                Risk_files: {
                    required: "Please Choose File",
                },
                Emr_files: {
                    required: "Please Choose File",
                },
                Hse_files: {
                    required: "Please Choose File",
                },
                files: {
                    required: "Please Choose File",
                },
                C_Additional_Pre: {
                    required: "Please Enter Additional Precautions",
                },
                Hse_Name: {
                    required: "Please Enter HSE Officer Name",
                },
                Hse_Number: {
                    required: "Please Enter HSE Mobile Number",
                },
                Signed_files: {
                    required: "Please Choose File",
                },
                Accept_Name: {
                    required: "Please Enter Name",
                },
                Accept_Desig: {
                    required: "Please Enter Designation",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Hw_Zone_Id-error").removeClass('mt-2');
                $("#Hw_Zone_Id-error").addClass('m-100 text-danger');

                $("#Hw_Community_Id-error").removeClass('mt-2');
                $("#Hw_Community_Id-error").addClass('m-100 text-danger');

                $("#C_Competent-error").removeClass('mt-2');
                $("#C_Competent-error").addClass('m-100 text-danger');

                $("#Hw_Building_Id-error").removeClass('mt-2');
                $("#Hw_Building_Id-error").addClass('m-100 text-danger');


                $("#search_input_1-error").removeClass('mt-2');
                $("#search_input_1-error").addClass('m-txt-req_11 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-txt-req text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var QNS_ARR = [];
                var Method_file_ARR = [];
                var Emr_file_ARR = [];
                var Risk_file_ARR = [];
                var HSE_file_ARR = [];
                var Staff_file_ARR = [];
                var Signed_file_ARR = [];
                $('#Tbl_Hot_Work_Ques tbody > tr').each(function () {
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varHSEWorkId
                    data.CSP_Ques_Name = varConfine_Ques_Ques;
                    data.CSP_Ques_Radio_Btn = RADIOVal;
                    data.CSP_Remarks = Standard_Remark;
                    data.CreatedBy = $("#CreatedBy").val();
                    QNS_ARR.push(data);
                });
                //var varDateTime = $("#C_Date_Time_Obs").val().replace("T", " ");
                var varDateTimeFrom = $("#C_From_Date").val();
                var varDateTimeTo = $("#C_To_Date").val();
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var Loc_Address = $("#search_input").val();
                var ExLocAddress = $("#search_input_1").val();
                var varMethod_file = $(".MethodUploadPhotos").val();
                var varRisk_file = $(".RiskUploadPhotos").val();
                var varEmr_file = $(".EmrUploadPhotos").val();
                var varHse_file = $(".HSEUploadPhotos").val();
                var varstaff_file = $(".UploadPhotos").val();
                var varSigned_file = $(".SignedUploadPhotos").val();
                var varcon_startDate = $("#Con_Start").val().replace("T", " ");
                var varcon_endDate = $("#Con_End").val().replace("T", " ");
                var varDms_Number = $("#Contractor_Id").val();
                // var varmethodPdf = $(".Method_Pdf_Files").val();

                if ((varMethod_file != "") && (varMethod_file != undefined) && (varMethod_file != null)) {
                    var valNew = varMethod_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Method_File_Id = "0";
                        File_data.CSP_Id = $("#Hot_Work_ID").val();
                        File_data.Method_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Method_file_ARR.push(File_data);
                    }
                }
                if ((varRisk_file != "") && (varRisk_file != undefined) && (varRisk_file != null)) {
                    var valNew = varRisk_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Risk_File_Id = "0";
                        File_data.CSP_Id = $("#Hot_Work_ID").val();
                        File_data.Risk_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Risk_file_ARR.push(File_data);
                    }
                }
                if ((varEmr_file != "") && (varEmr_file != undefined) && (varEmr_file != null)) {
                    var valNew = varEmr_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Emr_File_Id = "0";
                        File_data.CSP_Id = $("#Hot_Work_ID").val();
                        File_data.Emr_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Emr_file_ARR.push(File_data);
                    }
                }
                if ((varHse_file != "") && (varHse_file != undefined) && (varHse_file != null)) {
                    var valNew = varHse_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Hse_File_Id = "0";
                        File_data.CSP_Id = $("#Hot_Work_ID").val();
                        File_data.Hse_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        HSE_file_ARR.push(File_data);
                    }
                }
                if ((varstaff_file != "") && (varstaff_file != undefined) && (varstaff_file != null)) {
                    var valNew = varstaff_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.File_Id = "0";
                        File_data.CSP_Id = $("#Hot_Work_ID").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Staff_file_ARR.push(File_data);
                    }
                }
                if ((varSigned_file != "") && (varSigned_file != undefined) && (varSigned_file != null)) {
                    var valNew = varSigned_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Sign_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Sign_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Signed_file_ARR.push(File_data);
                    }
                }

                var History_ARR = [];
                var His_Data = {};

                His_Data.CSP_Id = $("#Hot_Work_ID").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);


                if ($("#Lat").val() != "" || $("#Long").val() != "" || $("#search_input").val() || $("#search_input_1").val()) {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                    Loc_Address = $("#search_input").val();
                    ExLocAddress = $("#search_input_1").val();

                }
            

                var Obj = {
                    CSP_Id: $("#Hot_Work_ID").val(),
                    Business_Unit_Id: $("#Hw_Business_Unit_Id").val(),
                    Zone_Id: $("#Hw_Zone_Id").val(),
                    Community_Id: $("#Hw_Community_Id").val(),
                    Building_Id: $("#Hw_Building_Id").val(),
                    Latitude: lat,
                    Longitude: long,
                    Location_Address: Loc_Address,
                    Exact_Loc_Address: ExLocAddress,
                    Company_Name: $("#C_Company_Name").val(),
                    Contrator_Title_No: $("#C_Tittle_No").val(),
                    Competent_Person: $("#C_Competent").val(),
                    Contractor_Name: $("#Contractor_name").val(),
                    Name: $("#C_Name").val(),
                    Position: $("#C_Position").val(),
                    Contact: $("#C_Contact").val(),
                    Email_Id: $("#C_Email_Id").val(),
                    Date_and_Time: $("#C_Date_Time").val(),
                    Description_of_Work: $("#C_Description").val(),
                    Work_Duration_From_Date: varDateTimeFrom,
                    Work_Duration_To_Date: varDateTimeTo,
                    Additional_Precautions: $("#C_Additional_Pre").val(),
                    Status: "1",
                    Contractor_Start_Date: varcon_startDate,
                    Contractor_End_Date: varcon_endDate,
                    DMS_No_Id: varDms_Number,
                    Competent_Number: $("#Comp_Number").val(),
                    HSE_Officer_Name: $("#Hse_Name").val(),
                    HSE_Mobile_Number: $("#Hse_Number").val(),
                    Declare_Name: $("#Accept_Name").val(),
                    Declare_Designation: $("#Accept_Desig").val(),
                    Declare_Chk: $("input[type='checkbox'].Declare_Chk:checked").val(),
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR,
                    _Method_Statement_Files: Method_file_ARR,
                    _Risk_Assess_Files: Risk_file_ARR,
                    _Emr_Plan_Files: Emr_file_ARR,
                    _HSE_Plan_Files: HSE_file_ARR,
                    _Staff_Comptency_Files: Staff_file_ARR,
                    _Sp_Signed_Files: Signed_file_ARR
                };
                $.ajax({
                    url: "/ControlOfWork/Add_Hot_Work_Req",
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
                })
            }
        })

    })

    //Work At Height
    $("#WAH_Submit").click(function () {
        $("#Form_WAH_Permit").validate({
            rules: {
                Wah_Business_Unit_Id: {
                    required: true,
                },
                Wah_Community_Id: {
                    required: true,
                },
                Wah_Zone_Id: {
                    required: true,
                },
                Wah_Building_Id: {
                    required: true,
                },
                C_Company_Name: {
                    required: true,
                },
                C_Tittle_No: {
                    required: true,
                },
                C_Competent: {
                    required: true,
                },
                C_Description: {
                    required: true,
                },
                C_Approve: {
                    required: true,
                },
                Confined_Ques: {
                    required: true,
                },
                C_From_Date: {
                    required: true,
                },
                C_Night_Shedule: {
                    required: true,
                },
                C_To_Date: {
                    required: true,
                },
                C_Validate: {
                    required: true,
                },
                //Contractor_name: {
                //    required: true,
                //},
                Con_Start: {
                    required: true,
                },
                Con_End: {
                    required: true,
                },
                Contractor_Id: {
                    required: true,
                },
                Method_files: {
                    required: true,
                },
                Risk_files: {
                    required: true,
                },
                Emr_files: {
                    required: true,
                },
                Hse_files: {
                    required: true,
                },
                files: {
                    required: true,
                },
                C_Additional_Pre: {
                    required: true,
                },
                Hse_Name: {
                    required: true,
                },
                Hse_Number: {
                    required: true,
                },
                Signed_files: {
                    required: true,
                },
                Accept_Name: {
                    required: true,
                },
                Accept_Desig: {
                    required: true,
                },
            },
            messages: {
                Wah_Business_Unit_Id: {
                    required: "Please select BusinessUnit"
                },
                Wah_Community_Id: {
                    required: "Please Select Community"
                },

                Wah_Zone_Id: {
                    required: "Please Select Zone"
                },
                Wah_Building_Id: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Contractor Title/No"
                },
                C_Competent: {
                    required: "Please Select Competent Person"
                },
                C_Description: {
                    required: "Please Enter Description"
                },
                C_Approve: {
                    required: "Please Choose Approve Method"
                },
                Confined_Ques: {
                    required: "Please Choose Questionnaries"
                },
                C_From_Date: {
                    required: "Please Choose From date"
                },
                C_Night_Shedule: {
                    required: "Please Choose Night shedule"
                },
                C_To_Date: {
                    required: "Please Choose To date"
                },
                C_Validate: {
                    required: "Please Choose Validate"
                },
                //Contractor_name: {
                //    required: "Please Choose Contractor Name",
                //},
                Con_Start: {
                    required: "Please select Contract Start Date",
                },
                Con_End: {
                    required: "Please select Contract End Date",
                },
                Contractor_Id: {
                    required: "Please select DMS Number",
                },
                Method_files: {
                    required: "Please Choose File",
                },
                Risk_files: {
                    required: "Please Choose File",
                },
                Emr_files: {
                    required: "Please Choose File",
                },
                Hse_files: {
                    required: "Please Choose File",
                },
                files: {
                    required: "Please Choose File",
                },
                C_Additional_Pre: {
                    required: "Please Enter Additional Precautions",
                },
                Hse_Name: {
                    required: "Please Enter HSE Officer Name",
                },
                Hse_Number: {
                    required: "Please Enter HSE Mobile Number",
                },
                Signed_files: {
                    required: "Please Choose File",
                },
                Accept_Name: {
                    required: "Please Enter Name",
                },
                Accept_Desig: {
                    required: "Please Enter Designation",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Wah_Zone_Id-error").removeClass('mt-2');
                $("#Wah_Zone_Id-error").addClass('m-100 text-danger');

                $("#Wah_Community_Id-error").removeClass('mt-2');
                $("#Wah_Community_Id-error").addClass('m-100 text-danger');

                $("#C_Competent-error").removeClass('mt-2');
                $("#C_Competent-error").addClass('m-100 text-danger');

                $("#Wah_Building_Id-error").removeClass('mt-2');
                $("#Wah_Building_Id-error").addClass('m-100 text-danger');

                $("#search_input_1-error").removeClass('mt-2');
                $("#search_input_1-error").addClass('m-txt-req_11 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-txt-req text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];
                var Method_file_ARR = [];
                var Emr_file_ARR = [];
                var Risk_file_ARR = [];
                var HSE_file_ARR = [];
                var Staff_file_ARR = [];
                var Signed_file_ARR = [];
                $('#Tbl_Work_At_Height_Ques tbody > tr').each(function () {
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varHSEWorkId
                    data.CSP_Ques_Name = varConfine_Ques_Ques;
                    data.CSP_Ques_Radio_Btn = RADIOVal;
                    data.CSP_Remarks = Standard_Remark;
                    data.CreatedBy = $("#CreatedBy").val();
                    QNS_ARR.push(data);
                });

                var varDateTimeFrom = $("#C_From_Date").val();
                var varDateTimeTo = $("#C_To_Date").val();
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var Loc_Address = $("#search_input").val();
                var ExLocAddress = $("#search_input_1").val();
                var varMethod_file = $(".MethodUploadPhotos").val();
                var varRisk_file = $(".RiskUploadPhotos").val();
                var varEmr_file = $(".EmrUploadPhotos").val();
                var varHse_file = $(".HSEUploadPhotos").val();
                var varstaff_file = $(".UploadPhotos").val();
                var varSigned_file = $(".SignedUploadPhotos").val();
                var varcon_startDate = $("#Con_Start").val().replace("T", " ");
                var varcon_endDate = $("#Con_End").val().replace("T", " ");
                var varDms_Number = $("#Contractor_Id").val();

                if ((varMethod_file != "") && (varMethod_file != undefined) && (varMethod_file != null)) {
                    var valNew = varMethod_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Method_File_Id = "0";
                        File_data.CSP_Id = $("#Work_Height_ID").val();
                        File_data.Method_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Method_file_ARR.push(File_data);
                    }
                }
                if ((varRisk_file != "") && (varRisk_file != undefined) && (varRisk_file != null)) {
                    var valNew = varRisk_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Risk_File_Id = "0";
                        File_data.CSP_Id = $("#Work_Height_ID").val();
                        File_data.Risk_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Risk_file_ARR.push(File_data);
                    }
                }
                if ((varEmr_file != "") && (varEmr_file != undefined) && (varEmr_file != null)) {
                    var valNew = varEmr_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Emr_File_Id = "0";
                        File_data.CSP_Id = $("#Work_Height_ID").val();
                        File_data.Emr_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Emr_file_ARR.push(File_data);
                    }
                }
                if ((varHse_file != "") && (varHse_file != undefined) && (varHse_file != null)) {
                    var valNew = varHse_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Hse_File_Id = "0";
                        File_data.CSP_Id = $("#Work_Height_ID").val();
                        File_data.Hse_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        HSE_file_ARR.push(File_data);
                    }
                }
                if ((varstaff_file != "") && (varstaff_file != undefined) && (varstaff_file != null)) {
                    var valNew = varstaff_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.File_Id = "0";
                        File_data.CSP_Id = $("#Work_Height_ID").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Staff_file_ARR.push(File_data);
                    }
                }
                if ((varSigned_file != "") && (varSigned_file != undefined) && (varSigned_file != null)) {
                    var valNew = varSigned_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Sign_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Sign_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Signed_file_ARR.push(File_data);
                    }
                }

                var History_ARR = [];
                var His_Data = {};

                His_Data.CSP_Id = $("#Work_Height_ID").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);


                if ($("#Lat").val() != "" || $("#Long").val() != "" || $("#search_input").val() || $("#search_input_1").val()) {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                    Loc_Address = $("#search_input").val();
                    ExLocAddress = $("#search_input_1").val();

                }
            
                var Obj = {
                    CSP_Id: $("#Work_Height_ID").val(),
                    Business_Unit_Id: $("#Wah_Business_Unit_Id").val(),
                    Zone_Id: $("#Wah_Zone_Id").val(),
                    Community_Id: $("#Wah_Community_Id").val(),
                    Building_Id: $("#Wah_Building_Id").val(),
                    Latitude: lat,
                    Longitude: long,
                    Location_Address: Loc_Address,
                    Exact_Loc_Address: ExLocAddress,
                    Company_Name: $("#C_Company_Name").val(),
                    Contrator_Title_No: $("#C_Tittle_No").val(),
                    Competent_Person: $("#C_Competent").val(),
                    Contractor_Name: $("#Contractor_name").val(),
                    Name: $("#C_Name").val(),
                    Position: $("#C_Position").val(),
                    Contact: $("#C_Contact").val(),
                    Email_Id: $("#C_Email_Id").val(),
                    Date_and_Time: $("#C_Date_Time").val(),
                    Description_of_Work: $("#C_Description").val(),
                    Work_Duration_From_Date: varDateTimeFrom,
                    Work_Duration_To_Date: varDateTimeTo,
                    Additional_Precautions: $("#C_Additional_Pre").val(),
                    Status: "1",
                    Contractor_Start_Date: varcon_startDate,
                    Contractor_End_Date: varcon_endDate,
                    DMS_No_Id: varDms_Number,
                    Competent_Number: $("#Comp_Number").val(),
                    HSE_Officer_Name: $("#Hse_Name").val(),
                    HSE_Mobile_Number: $("#Hse_Number").val(),
                    Declare_Name: $("#Accept_Name").val(),
                    Declare_Designation: $("#Accept_Desig").val(),
                    Declare_Chk: $("input[type='checkbox'].Declare_Chk:checked").val(),
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR,
                    _Method_Statement_Files: Method_file_ARR,
                    _Risk_Assess_Files: Risk_file_ARR,
                    _Emr_Plan_Files: Emr_file_ARR,
                    _HSE_Plan_Files: HSE_file_ARR,
                    _Staff_Comptency_Files: Staff_file_ARR,
                    _Sp_Signed_Files: Signed_file_ARR
                };
                $.ajax({
                    url: "/ControlOfWork/Add_Work_Height_Req",
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
                })
            }
        })
    })

    //Fire & Safety Permit
    $("#Fire_Submit").click(function () {
        $("#Form_FS_Permit").validate({
            rules: {
                Business_Unit_Id: {
                    required: true,
                },
                Community_Id: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                C_Company_Name: {
                    required: true,
                },
                C_Tittle_No: {
                    required: true,
                },
                C_Competent: {
                    required: true,
                },
                C_Description: {
                    required: true,
                },
                C_Approve: {
                    required: true,
                },
                Confined_Ques: {
                    required: true,
                },
                C_From_Date: {
                    required: true,
                },
                C_Night_Shedule: {
                    required: true,
                },
                C_To_Date: {
                    required: true,
                },
                C_Validate: {
                    required: true,
                },
                //Contractor_name: {
                //    required: true,
                //},
                Con_Start: {
                    required: true,
                },
                Con_End: {
                    required: true,
                },
                Contractor_Id: {
                    required: true,
                },
                Method_files: {
                    required: true,
                },
                Risk_files: {
                    required: true,
                },
                Emr_files: {
                    required: true,
                },
                Hse_files: {
                    required: true,
                },
                files: {
                    required: true,
                },
                FS_Description: {
                    required: true,
                },
                FS_Isolation: {
                    required: true,
                },
                Isolation: {
                    required: true,
                },
                During_Work_Hours: {
                    required: true,
                },
                Out_Work_Hours: {
                    required: true,
                },
                Alt_Fire_Fight: {
                    required: true,
                },
                Fire_Watcher_Details: {
                    required: true,
                },
                FS_Start_Date: {
                    required: true,
                },
                FS_End_Date: {
                    required: true,
                },
                FS_Reason_Isolation: {
                    required: true,
                },
                FS_Additional: {
                    required: true,
                },
                Hse_Name: {
                    required: true,
                },
                Hse_Number: {
                    required: true,
                },
                Signed_files: {
                    required: true,
                },
                Accept_Name: {
                    required: true,
                },
                Accept_Desig: {
                    required: true,
                },
                FS_Competent: {
                    required: true,
                },
                FS_Building_Id: {
                    required: true,
                },
            },
            messages: {
                Business_Unit_Id: {
                    required: "Please select BusinessUnit"
                },
                Community_Id: {
                    required: "Please Select Community"
                },

                Zone_Id: {
                    required: "Please Select Zone"
                },
                Building_Id: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Contractor Title/No"
                },
                C_Competent: {
                    required: "Please Select Competent Person"
                },
                C_Description: {
                    required: "Please Enter Description"
                },
                C_Approve: {
                    required: "Please Choose Approve Method"
                },
                Confined_Ques: {
                    required: "Please Choose Questionnaries"
                },
                C_From_Date: {
                    required: "Please Choose From date"
                },
                C_Night_Shedule: {
                    required: "Please Choose Night shedule"
                },
                C_To_Date: {
                    required: "Please Choose To date"
                },
                C_Validate: {
                    required: "Please Choose Validate"
                },
                //Contractor_name: {
                //    required: "Please Choose Contractor Name",
                //},
                Con_Start: {
                    required: "Please select Contract Start Date",
                },
                Con_End: {
                    required: "Please select Contract End Date",
                },
                Contractor_Id: {
                    required: "Please select DMS Number",
                },
                Method_files: {
                    required: "Please Choose File",
                },
                Risk_files: {
                    required: "Please Choose File",
                },
                Emr_files: {
                    required: "Please Choose File",
                },
                Hse_files: {
                    required: "Please Choose File",
                },
                files: {
                    required: "Please Choose File",
                },
                FS_Description: {
                    required: "Please Enter Description",
                },
                FS_Isolation: {
                    required: "Please Enter Isolation or Impairment Coordinator",
                },
                Isolation: {
                    required: "Please Select The Degree Of Isolation",
                },
                During_Work_Hours: {
                    required: "Please Enter During Working Hours",
                },
                Out_Work_Hours: {
                    required: "Please Enter Out Working Hours",
                },
                Alt_Fire_Fight: {
                    required: "Please Enter Alterate FireFighting Option",
                },
                Fire_Watcher_Details: {
                    required: "Please Enter Fire Watcher Details 24*7 ",
                },
                FS_Start_Date: {
                    required: "Please Select From Date & Time",
                },
                FS_End_Date: {
                    required: "Please Select To Date & Time",
                },
                FS_Reason_Isolation: {
                    required: "Please Enter Reason For Isolation",
                },
                FS_Additional: {
                    required: "Please Enter Additional Precautions",
                },
                Hse_Name: {
                    required: "Please Enter HSE Officer Name",
                },
                Hse_Number: {
                    required: "Please Enter HSE Mobile Number",
                },
                Signed_files: {
                    required: "Please Choose File",
                },
                Accept_Name: {
                    required: "Please Enter Name",
                },
                Accept_Desig: {
                    required: "Please Enter Designation",
                },
                FS_Competent: {
                    required: "Please Select Competent Person",
                },
                FS_Building_Id: {
                    required:"Please Select Building"
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#FS_Competent-error").removeClass('mt-2');
                $("#FS_Competent-error").addClass('m-100 text-danger');

                $("#FS_Building_Id-error").removeClass('mt-2');
                $("#FS_Building_Id-error").addClass('m-100 text-danger');

                $("#search_input_1-error").removeClass('mt-2');
                $("#search_input_1-error").addClass('m-txt-req_11 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-txt-req text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];
                var Method_file_ARR = [];
                var Emr_file_ARR = [];
                var Risk_file_ARR = [];
                var HSE_file_ARR = [];
                var Staff_file_ARR = [];
                var Extent_Areas = [];
                var Personnel_Affected = [];
                var Method_Iso = [];
                var Signed_file_ARR = [];
                $('#Tbl_FIRE_SAFETY tbody > tr').each(function () {
                    debugger;
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Ques_CSP_ID = "0";
                    data.Major_HSE_Work_Id = varHSEWorkId;
                    data.CSP_Ques_Name = varConfine_Ques_Ques;
                    data.CSP_Ques_Radio_Btn = RADIOVal;
                    data.CSP_Remarks = Standard_Remark;
                    data.CreatedBy = $("#CreatedBy").val();
                    QNS_ARR.push(data);
                });

                $('#tblAreaAffect tbody > tr').each(function () {
                    var varAreatxt = $(this).closest('tr').find('.AreaName').val();
                    var data = {};
                    data.FS_Area_Affect_Id = "0"
                    data.CSP_Id = $("#FS_FireSafe_Id").val();
                    data.Extent_Area_Affected = varAreatxt;
                    Extent_Areas.push(data);
                });

                $('#tblPersonnelAffect tbody > tr').each(function () {
                    var varPersoneltxt = $(this).closest('tr').find('.Personnel_name').val();
                    var data = {};
                    data.FS_Personnel_Affect_Id = "0"
                    data.CSP_Id = $("#FS_FireSafe_Id").val();
                    data.Extent_Personnel_Affected = varPersoneltxt;
                    Personnel_Affected.push(data);
                });

                $('#tblMethodAffect tbody > tr').each(function () {
                    var varMethodtxt = $(this).closest('tr').find('.Isolation_name').val();
                    var data = {};
                    data.FS_Method_Isolation_Id = "0"
                    data.CSP_Id = $("#varMethodtxt").val();
                    data.Method_Isolation = varMethodtxt;
                    Method_Iso.push(data);
                });
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var varDateTimeFrom = $("#FS_Start_Date").val();
                var varDateTimeTo = $("#FS_End_Date").val();
                var Loc_Address = $("#search_input").val();
                var ExLocAddress = $("#search_input_1").val();
                var varMethod_file = $(".MethodUploadPhotos").val();
                var varRisk_file = $(".RiskUploadPhotos").val();
                var varEmr_file = $(".EmrUploadPhotos").val();
                var varHse_file = $(".HSEUploadPhotos").val();
                var varstaff_file = $(".UploadPhotos").val();
                var varSigned_file = $(".SignedUploadPhotos").val();
                var varcon_startDate = $("#FS_Con_Start_Date").val().replace("T", " ");
                var varcon_endDate = $("#FS_Con_End_Date").val().replace("T", " ");
                var varDms_Number = $("#FireSaFeDms_Id").val();
                var FireAlarmChk = $("input[type='checkbox'].FireAlarmId:checked").val();
                var FireProtectionChk = $("input[type='checkbox'].FireProId:checked").val();
                var PAVAChk = $("input[type='checkbox'].cls_Pava:checked").val();


                if ((varMethod_file != "") && (varMethod_file != undefined) && (varMethod_file != null)) {
                    var valNew = varMethod_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Method_File_Id = "0";
                        File_data.CSP_Id = $("#FS_FireSafe_Id").val();
                        File_data.Method_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Method_file_ARR.push(File_data);
                    }
                }
                if ((varRisk_file != "") && (varRisk_file != undefined) && (varRisk_file != null)) {
                    var valNew = varRisk_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Risk_File_Id = "0";
                        File_data.CSP_Id = $("#FS_FireSafe_Id").val();
                        File_data.Risk_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Risk_file_ARR.push(File_data);
                    }
                }
                if ((varEmr_file != "") && (varEmr_file != undefined) && (varEmr_file != null)) {
                    var valNew = varEmr_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Emr_File_Id = "0";
                        File_data.CSP_Id = $("#FS_FireSafe_Id").val();
                        File_data.Emr_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Emr_file_ARR.push(File_data);
                    }
                }
                if ((varHse_file != "") && (varHse_file != undefined) && (varHse_file != null)) {
                    var valNew = varHse_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Hse_File_Id = "0";
                        File_data.CSP_Id = $("#FS_FireSafe_Id").val();
                        File_data.Hse_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        HSE_file_ARR.push(File_data);
                    }
                }
                if ((varstaff_file != "") && (varstaff_file != undefined) && (varstaff_file != null)) {
                    var valNew = varstaff_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.File_Id = "0";
                        File_data.CSP_Id = $("#FS_FireSafe_Id").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Staff_file_ARR.push(File_data);
                    }
                }
                if ((varSigned_file != "") && (varSigned_file != undefined) && (varSigned_file != null)) {
                    var valNew = varSigned_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Sign_File_Id = "0";
                        File_data.CSP_Id = $("#Confined_ID").val();
                        File_data.Sign_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Signed_file_ARR.push(File_data);
                    }
                }

                var History_ARR = [];
                var His_Data = {};

                His_Data.CSP_Id = $("#FS_FireSafe_Id").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);


                if ($("#Lat").val() != "" || $("#Long").val() != "" || $("#search_input").val() || $("#search_input_1").val()) {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                    Loc_Address = $("#search_input").val();
                    ExLocAddress = $("#search_input_1").val();

                }

                if (Loc_Address == "" || Loc_Address == null) {
                    toastr.error("Please select the location name!", "Error");
                }
                if (ExLocAddress == "" || ExLocAddress == null) {
                    toastr.error("Please enter the exact location!", "Error");
                }

                var Obj = {
                    CSP_Id: $("#FS_FireSafe_Id").val(),
                    Business_Unit_Id: $("#FS_Business_Unit_Id").val(),
                    Zone_Id: $("#FS_Zone_Id").val(),
                    Community_Id: $("#FS_Community_Id").val(),
                    Building_Id: $("#FS_Building_Id").val(),
                    Latitude: lat,
                    Longitude: long,
                    Location_Address: Loc_Address,
                    Exact_Loc_Address: ExLocAddress,
                    Company_Name: $("#FS_Company_Name").val(),
                    Contrator_Title_No: $("#FS_Tittle_No").val(),
                    Competent_Person: $("#FS_Competent").val(),
                    Contractor_Name: $("#Fs_Inchar").val(),
                    Name: $("#Fire_Safe_Name").val(),
                    Position: $("#Fire_Position").val(),
                    Contact: $("#Fire_Contact").val(),
                    Email_Id: $("#Fire_Email_Id").val(),
                    Date_and_Time: $("#FS_Date_Time").val(),
                    Description_of_Work: $("#FS_Description").val(),
                    Work_Duration_From_Date: varDateTimeFrom,
                    Work_Duration_To_Date: varDateTimeTo,
                    Additional_Precautions: $("#FS_Additional").val(),
                    Status: "1",
                    Contractor_Start_Date: varcon_startDate,
                    Contractor_End_Date: varcon_endDate,
                    Isolation_Impair_Coordinate: $("#FS_Isolation").val(),
                    Fire_Alarm_Chk: FireAlarmChk,
                    Fire_Protection_Chk: FireProtectionChk,
                    PAVA_Chk: PAVAChk,
                    CreatedBy: $("#CreatedBy").val(),
                    Degree_Isolation: $("input[type='radio'].Cls_Isolation:checked").val(),
                    Reason_Isolation: $("#FS_Reason_Isolation").val(),
                    During_Work_Hrs: $("#During_Work_Hours").val(),
                    Out_Work_Hrs: $("#Out_Work_Hours").val(),
                    alter_fire_fight_option: $("#Alt_Fire_Fight").val(),
                    Fire_watcher_Detail: $("#Fire_Watcher_Details").val(),
                    DMS_No_Id: varDms_Number,
                    Competent_Number: $("#Comp_Number").val(),
                    HSE_Officer_Name: $("#Hse_Name").val(),
                    HSE_Mobile_Number: $("#Hse_Number").val(),
                    Declare_Name: $("#Accept_Name").val(),
                    Declare_Designation: $("#Accept_Desig").val(),
                    Declare_Chk: $("input[type='checkbox'].Declare_Chk:checked").val(),
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR,
                    _Method_Statement_Files: Method_file_ARR,
                    _Risk_Assess_Files: Risk_file_ARR,
                    _Emr_Plan_Files: Emr_file_ARR,
                    _HSE_Plan_Files: HSE_file_ARR,
                    _Staff_Comptency_Files: Staff_file_ARR,
                    _Area_Affected: Extent_Areas,
                    _Personnel_Affected: Personnel_Affected,
                    _Method_Isolation: Method_Iso,
                    _Sp_Signed_Files: Signed_file_ARR
                };
                $.ajax({
                    url: "/ControlOfWork/Add_Fire_Safety_Req",
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
                })
            }
        })
    });
})