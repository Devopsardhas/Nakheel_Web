$(function () {
    'use strict';

    $("#SignUpForm").validate({
        rules: {
            Business_Unit: {
                required: true,
            },
            Zone: {
                required: true,
            },
            Community: {
                required: true,
            },
            Building: {
                required: true,
            },

            Inc_Business_Unit_Type: {
                required: true,
            },
            Con_Name: {
                required: true,
            },
            Manager_Inc: {
                required: true,
            },
            Designation: {
                required: true,
            },
            Manager_Num: {
                required: true,
            },
            OfficialEmail_ID: {
                required: true,
            },
            Po_Num: {
                required: true,
            },
            DMS_Num: {
                required: true,
            },
            HSE_Office: {
                required: true,
            },
            Hse_Officer_Mail: {
                required: true,
            },
            Hse_Mob_Num: {
                required: true,
            },
            Con_Start_Date: {
                required: true,
            },
            Con_End_Date: {
                required: true,
            },
            StudImage: {
                required: true,
            },
            StudImage1: {
                required: true,
            },
            StudImage2: {
                required: true,
            },
            StudImage3: {
                required: true,
            },
            StudImage4: {
                required: true,
            },
            StudImage5: {
                required: true,
            },
            StudImage6: {
                required: true,
            },
            StudImage8: {
                required: true,
            },
            Doc_Date: {
                required: true,
            },
            Doc_Date1: {
                required: true,
            },
            Doc_Date2: {
                required: true,
            },
            Doc_Date3: {
                required: true,
            },
            Doc_Date4: {
                required: true,
            },
            Doc_Date5: {
                required: true,
            },
            Doc_Date6: {
                required: true,
            },
            OtherDoc: {
                required: true,
            },
            G_Name_Position: {
                required: true,
            },
            G_Contact: {
                required: true,
            },
            G_Email: {
                required: true,
            },
            G_Work_Description: {
                required: true,
            },
            G_Specific_Requirements: {
                required: true,
            },
            G_From_Date: {
                required: true,
            },
            G_To_Date: {
                required: true,
            },
            G_Night_Shedule: {
                required: true,
            },
            G_Valid_Date: {
                required: true,
            },
            Scope_of_work: {
                required: true,
            },
            Risk_Major_List: {
                required: true,
            },
            Project_Title: {
                required: true,
            },
            Contract_Type: {
                required: true,
            },
            StudImage10: {
                required: true,
            },
        },
        messages: {
            Business_Unit: {
                required: "Please select Business Unit"
            },
            Zone: {
                required: "Please Select Zone"
            },
            Community: {
                required: "Please Select Community"
            },
            Building: {
                required: "please Select Building"
            },

            Inc_Business_Unit_Type: {
                required: "Please Select Business Unit Type"
            },
            Con_Name: {
                required: "Please Enter Company Name"
            },
            Manager_Inc: {
                required: "Please Enter Manager Incharge"
            },
            Designation: {
                required: "Please Enter Designation"
            },
            Manager_Num: {
                required: "Please Enter Manager Number"
            },
            OfficialEmail_ID: {
                required: "Please Enter official Mail Id"
            },
            Po_Num: {
                required: "Please Enter Purchase Order Number / DMS Number"
            },
            DMS_Num: {
                required: "Please Enter DMS Number"
            },
            HSE_Office: {
                required: "please Enter HSE Officer"
            },
            Hse_Officer_Mail: {
                required: "Please Enter Hse Officer Mail"
            },
            Hse_Mob_Num: {
                required: "Please Enter HSE Mobile Number"
            },
            Con_Start_Date: {
                required: "Please Select Start Date"
            },
            Con_End_Date: {
                required: "Please Select End Date"
            },
            StudImage: {
                required: "Please Upload Document"
            },
            StudImage1: {
                required: "Please Upload Document"
            },
            StudImage2: {
                required: "Please Upload Document"
            },
            StudImage3: {
                required: "Please Upload Document"
            },
            StudImage4: {
                required: "Please Upload Document"
            },
            StudImage5: {
                required: "Please Upload Document"
            },
            StudImage6: {
                required: "Please Upload Document"
            },
            StudImage8: {
                required: "Please Upload Document"
            },
            StudImage10: {
                required: "Please Upload Document"
            },
            Doc_Date: {
                required: "Please select Date"
            },
            Doc_Date1: {
                required: "Please select Date"
            },
            Doc_Date2: {
                required: "Please select Date"
            },
            Doc_Date3: {
                required: "Please select Date"
            },
            Doc_Date4: {
                required: "Please select Date"
            },
            Doc_Date5: {
                required: "Please select Date"
            },
            Doc_Date6: {
                required: "Please select Date"
            },
            OtherDoc: {
                required: "Please enter other document"
            },
            G_Name_Position: {
                required: "Please enter Name & Position"
            },
            G_Contact: {
                required: "Please enter Contact"
            },
            G_Email: {
                required: "Please Enter Emailid"
            },
            G_Work_Description: {
                required: "Please Enter Work Description"
            },
            G_Specific_Requirements: {
                required: "Please Enter Specific Requirements"
            },
            G_From_Date: {
                required: "Please Select Date"
            },
            G_To_Date: {
                required: "Please Select Date"
            },
            G_Night_Shedule: {
                required: "Please Select Night Shedule"
            },
            G_Valid_Date: {
                required: "Please Select Date"
            },
            Scope_of_work: {
                required: "Please Select Scope of work"
            },
            Risk_Major_List: {
                required: "Please Select Major HSE Risk",
            },
            Project_Title: {
                required: "Please Enter Project Title",
            },
            Contract_Type: {
                required: "Please Select Contract Type",
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);

            $("#Business_Unit-error").removeClass('mt-2');
            $("#Business_Unit-error").addClass('m-100 text-danger');

            $("#Zone_Id-error").removeClass('mt-2');
            $("#Zone_Id-error").addClass('m-100 text-danger');

            $("#Community_Id-error").removeClass('mt-2');
            $("#Community_Id-error").addClass('m-100 text-danger');

            $("#Business_Unit_Id-error").removeClass('mt-2');
            $("#Business_Unit_Id-error").addClass('m-100 text-danger');

            $("#Building_Id-error").removeClass('mt-2');
            $("#Building_Id-error").addClass('m-100 text-danger');

            $("#Scope_of_work-error").removeClass('mt-2');
            $("#Scope_of_work-error").addClass('m-100 text-danger');

            $("#Risk_Major_List-error").removeClass('mt-2');
            $("#Risk_Major_List-error").addClass('m-100 text-danger');

            $("#Risk_Major_List-error").removeClass('mt-2');
            $("#Risk_Major_List-error").addClass('m-105 text-danger');
        },
        submitHandler: function () {
            debugger;
            //var status = confirm("Click OK to continue?");
            //if (status == false) {
            //    return false;
            //}
            //else {
            //    return true;
            //}
            var RequiredAttachments_List = [];
            var MajorRisk_List = [];
            var Work_Superviosor_List = [];
            var Building_Arr = [];

            var Building_id = $("#Building_Id").val();

            $('#tbl_Corrective_Action tbody > tr').each(function () {
                var varName_Position = $(this).closest('tr').find('.Name_Position').val();
                var varContact = $(this).closest('tr').find('.Contact').val();
                var varEmail_Id = $(this).closest('tr').find('.Email_Id').val();
                var data = {};
                data.Name_Position = varName_Position;
                data.Contact = varContact;
                data.Email_Id = varEmail_Id;
                Work_Superviosor_List.push(data);
            });

            $('#tblRequiredAttachments tbody > tr').each(function () {
                var varDocument_Type = $(this).closest('tr').find('.clsTitle').val();
                var varUpload_Documents = $(this).closest('tr').find('.APPENDIX_A_Viewhide').val();
                var varExpiryDate = $(this).closest('tr').find('.ExpiryDate').val();
                var varDesc = $(this).closest('tr').find('.clsDesc').val();

                if (varDocument_Type == "") {
                    varDocument_Type = null;
                }
                if ((varUpload_Documents != "") && (varUpload_Documents != undefined) && (varUpload_Documents != null)) {

                    var valNew = varUpload_Documents.split(',');
                    for (var i = 0; i < valNew.length; i++) {

                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        //alert(t);
                        var Photodata = {};
                        Photodata.Document_Type = varDocument_Type;
                        Photodata.Required_File_Path = t;
                        Photodata.Expiry_Date = varExpiryDate;
                        Photodata.Description = varDesc;
                        RequiredAttachments_List.push(Photodata);
                    }
                }

                //data.Document_Type = varDocument_Type;
                //data.Required_File_Path = varUpload_Documents;
                //data.Expiry_Date = varExpiryDate;
                //RequiredAttachments_List.push(data);
            });

            $('.clsMajor_HSE_Work:checked').each(function () {
                var varMajor_HSE_Risk_Id = "0";
                var varSignUp_Id = "0";
                var varMajor_HSE_Work_Id = $(this).val();

                var data = {};
                data.Major_HSE_Risk_Id = varMajor_HSE_Risk_Id;
                data.SignUp_Id = varSignUp_Id;
                data.Major_HSE_Work_Id = varMajor_HSE_Work_Id;
                MajorRisk_List.push(data);
            });

            $(Building_id).each(function (i, b) {
                debugger;
                var item = {};
                item.Building_Id = b,
                    Building_Arr.push(item);
            });

            var Obj = {
                SignUp_Id: $("#SignUp_Id").val(),
                Business_Unit_Id: $("#Business_Unit_Id").val(),
                Zone_Id: $("#Zone_Id").val(),
                Community_Id: $("#Community_Id").val(),
                Building_Id: "0",
                Company_Name: $("#Company_Name").val(),
                Inc_Business_Unit_Type: $("#Inc_Business_Unit_Type").val(),
                Manager_Incharge: $("#Manager_Incharge").val(),
                Designation: $("#Designation").val(),
                Mobile_No: $("#Mobile_No").val(),
                Official_Email_Id: $("#Official_Email_Id").val(),
                Purchase_Order_Number: $("#Purchase_Order_Number").val(),
                DMS_Number: $("#DMS_Number").val(),
                HSE_Officer: $("#HSE_Officer").val(),
                HSE_Officer_Email: $("#HSE_Officer_Email").val(),
                HSE_Officer_Mobile_Number: $("#HSE_Officer_Mobile_Number").val(),
                Contract_Start_Date: $("#Contract_Start_Date").val(),
                Contract_End_Date: $("#Contract_End_Date").val(),
                W_Name_Position: $("#W_Name_Position").val(),
                W_Contact: $("#W_Contact").val(),
                W_Email_Id: $("#W_Email_Id").val(),
                W_Work_Description: $("#W_Work_Description").val(),
                W_Specific_Requirements: $("#W_Specific_Requirements").val(),
                W_From_Date: $("#W_From_Date").val(),
                W_To_date: $("#W_To_date").val(),
                W_Night_Schedule: $("input[type='radio'].G_Night_Shedule:checked").val(),
                W_Permit_Validity: $("#W_Permit_Validity").val(),
                W_Scope_of_work: $("#Scope_of_work").val(),
                Company_Id: $("#Company_Details").val(),
                W_Name_Position: $("#Project_Title").val(),
                W_Contact: $("#Contract_Type").val(),
                L_M_Work_Superviosor_List: Work_Superviosor_List,
                L_M_RequiredAttachments_List: RequiredAttachments_List,
                L_M_Major_HSE_Risk_List: MajorRisk_List,
                L_M_Building_List: Building_Arr
            };
            $.ajax({
                url: "/Account/Add_Service_Provider_Sign_Up",
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
                            //window.location.reload();
                            //window.location.href = "/Account/Login";
                            LOAD_BUSINESS_UNIT();
                            LOAD_ZONE();
                            $('#Community_Id').empty();
                            $('#Building_Id').empty();
                            $('#Scope_of_work').val('0');
                            $('#tbody_Corrective_Action_Emp').html('');
                            Work_SuperviosorCP_AddMore();

                            $('input[type=checkbox]').prop('checked', false);
                            $('input[type=text],textarea').each(function () {
                                $(this).val('');
                            });
                            $('input[type=email]').each(function () {
                                $(this).val('');
                            });
                            $('input[type=date]').each(function () {
                                $(this).val('');
                            });
                            $('input[type=file]').each(function () {
                                $(this).val('');
                            });
                            $(".file-caption-name").val('');
                            Load_Service_Provider($("#Company_Details").val());
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


    $("#UpdateSignUpForm").validate({
        rules: {
            Business_Unit: {
                required: true,
            },
            Zone: {
                required: true,
            },
            Community: {
                required: true,
            },
            Building: {
                required: true,
            },

            Inc_Business_Unit_Type: {
                required: true,
            },
            Con_Name: {
                required: true,
            },
            Manager_Inc: {
                required: true,
            },
            Designation: {
                required: true,
            },
            Manager_Num: {
                required: true,
            },
            OfficialEmail_ID: {
                required: true,
            },
            Po_Num: {
                required: true,
            },
            DMS_Num: {
                required: true,
            },
            HSE_Office: {
                required: true,
            },
            Hse_Officer_Mail: {
                required: true,
            },
            Hse_Mob_Num: {
                required: true,
            },
            Con_Start_Date: {
                required: true,
            },
            Con_End_Date: {
                required: true,
            },
            StudImage: {
                required: true,
            },
            StudImage1: {
                required: true,
            },
            StudImage2: {
                required: true,
            },
            StudImage3: {
                required: true,
            },
            StudImage4: {
                required: true,
            },
            StudImage5: {
                required: true,
            },
            StudImage6: {
                required: true,
            },
            StudImage8: {
                required: true,
            },
            Doc_Date: {
                required: true,
            },
            Doc_Date1: {
                required: true,
            },
            Doc_Date2: {
                required: true,
            },
            Doc_Date3: {
                required: true,
            },
            Doc_Date4: {
                required: true,
            },
            Doc_Date5: {
                required: true,
            },
            Doc_Date6: {
                required: true,
            },
            OtherDoc: {
                required: true,
            },
            G_Name_Position: {
                required: true,
            },
            G_Contact: {
                required: true,
            },
            G_Email: {
                required: true,
            },
            G_Work_Description: {
                required: true,
            },
            G_Specific_Requirements: {
                required: true,
            },
            G_From_Date: {
                required: true,
            },
            G_To_Date: {
                required: true,
            },
            G_Night_Shedule: {
                required: true,
            },
            G_Valid_Date: {
                required: true,
            },
            Scope_of_work: {
                required: true,
            },
            Risk_Major_List: {
                required: true,
            },
            Project_Title: {
                required: true,
            },
            Contract_Type: {
                required: true,
            },
            StudImage10: {
                required: true,
            },
        },
        messages: {
            Business_Unit: {
                required: "Please select Business Unit"
            },
            Zone: {
                required: "Please Select Zone"
            },
            Community: {
                required: "Please Select Community"
            },
            Building: {
                required: "please Select Building"
            },

            Inc_Business_Unit_Type: {
                required: "Please Select Business Unit Type"
            },
            Con_Name: {
                required: "Please Enter Company Name"
            },
            Manager_Inc: {
                required: "Please Enter Manager Incharge"
            },
            Designation: {
                required: "Please Enter Designation"
            },
            Manager_Num: {
                required: "Please Enter Manager Number"
            },
            OfficialEmail_ID: {
                required: "Please Enter official Mail Id"
            },
            Po_Num: {
                required: "Please Enter Purchase Order Number / DMS Number"
            },
            DMS_Num: {
                required: "Please Enter DMS Number"
            },
            HSE_Office: {
                required: "please Enter HSE Officer"
            },
            Hse_Officer_Mail: {
                required: "Please Enter Hse Officer Mail"
            },
            Hse_Mob_Num: {
                required: "Please Enter HSE Mobile Number"
            },
            Con_Start_Date: {
                required: "Please Select Start Date"
            },
            Con_End_Date: {
                required: "Please Select End Date"
            },
            StudImage: {
                required: "Please Upload Document"
            },
            StudImage1: {
                required: "Please Upload Document"
            },
            StudImage2: {
                required: "Please Upload Document"
            },
            StudImage3: {
                required: "Please Upload Document"
            },
            StudImage4: {
                required: "Please Upload Document"
            },
            StudImage5: {
                required: "Please Upload Document"
            },
            StudImage6: {
                required: "Please Upload Document"
            },
            StudImage8: {
                required: "Please Upload Document"
            },
            StudImage10: {
                required: "Please Upload Document"
            },
            Doc_Date: {
                required: "Please select Date"
            },
            Doc_Date1: {
                required: "Please select Date"
            },
            Doc_Date2: {
                required: "Please select Date"
            },
            Doc_Date3: {
                required: "Please select Date"
            },
            Doc_Date4: {
                required: "Please select Date"
            },
            Doc_Date5: {
                required: "Please select Date"
            },
            Doc_Date6: {
                required: "Please select Date"
            },
            OtherDoc: {
                required: "Please enter other document"
            },
            G_Name_Position: {
                required: "Please enter Name & Position"
            },
            G_Contact: {
                required: "Please enter Contact"
            },
            G_Email: {
                required: "Please Enter Emailid"
            },
            G_Work_Description: {
                required: "Please Enter Work Description"
            },
            G_Specific_Requirements: {
                required: "Please Enter Specific Requirements"
            },
            G_From_Date: {
                required: "Please Select Date"
            },
            G_To_Date: {
                required: "Please Select Date"
            },
            G_Night_Shedule: {
                required: "Please Select Night Shedule"
            },
            G_Valid_Date: {
                required: "Please Select Date"
            },
            Scope_of_work: {
                required: "Please Select Scope of work"
            },
            Risk_Major_List: {
                required: "Please Select Major HSE Risk",
            },
            Project_Title: {
                required: "Please Enter Project Title",
            },
            Contract_Type: {
                required: "Please Select Contract Type",
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);

            $("#Business_Unit-error").removeClass('mt-2');
            $("#Business_Unit-error").addClass('m-100 text-danger');

            $("#Zone_Id-error").removeClass('mt-2');
            $("#Zone_Id-error").addClass('m-100 text-danger');

            $("#Community_Id-error").removeClass('mt-2');
            $("#Community_Id-error").addClass('m-100 text-danger');

            $("#Business_Unit_Id-error").removeClass('mt-2');
            $("#Business_Unit_Id-error").addClass('m-100 text-danger');

            $("#Building_Id-error").removeClass('mt-2');
            $("#Building_Id-error").addClass('m-100 text-danger');

            $("#Scope_of_work-error").removeClass('mt-2');
            $("#Scope_of_work-error").addClass('m-100 text-danger');

            $("#Risk_Major_List-error").removeClass('mt-2');
            $("#Risk_Major_List-error").addClass('m-100 text-danger');

            $("#Risk_Major_List-error").removeClass('mt-2');
            $("#Risk_Major_List-error").addClass('m-105 text-danger');
        },
        submitHandler: function () {
            debugger;
            //var status = confirm("Click OK to continue?");
            //if (status == false) {
            //    return false;
            //}
            //else {
            //    return true;
            //}
            var RequiredAttachments_List = [];
            var MajorRisk_List = [];
            var Work_Superviosor_List = [];
            var Building_Arr = [];

            var Building_id = $("#Building_Id").val();

            $('#tbl_Corrective_Action tbody > tr').each(function () {
                var varName_Position = $(this).closest('tr').find('.Name_Position').val();
                var varContact = $(this).closest('tr').find('.Contact').val();
                var varEmail_Id = $(this).closest('tr').find('.Email_Id').val();
                var data = {};
                data.Name_Position = varName_Position;
                data.Contact = varContact;
                data.Email_Id = varEmail_Id;
                Work_Superviosor_List.push(data);
            });

            $('#tblRequiredAttachments tbody > tr').each(function () {
                var varDocument_Type = $(this).closest('tr').find('.clsTitle').val();
                var varUpload_Documents = $(this).closest('tr').find('.APPENDIX_A_Viewhide').val();
                var varExpiryDate = $(this).closest('tr').find('.ExpiryDate').val();
                var varDesc = $(this).closest('tr').find('.clsDesc').val();

                if (varDocument_Type == "") {
                    varDocument_Type = null;
                }
                if ((varUpload_Documents != "") && (varUpload_Documents != undefined) && (varUpload_Documents != null)) {

                    var valNew = varUpload_Documents.split(',');
                    for (var i = 0; i < valNew.length; i++) {

                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        //alert(t);
                        var Photodata = {};
                        Photodata.Document_Type = varDocument_Type;
                        Photodata.Required_File_Path = t;
                        Photodata.Expiry_Date = varExpiryDate;
                        Photodata.Description = varDesc;
                        RequiredAttachments_List.push(Photodata);
                    }
                }

                //data.Document_Type = varDocument_Type;
                //data.Required_File_Path = varUpload_Documents;
                //data.Expiry_Date = varExpiryDate;
                //RequiredAttachments_List.push(data);
            });

            $('.clsMajor_HSE_Work:checked').each(function () {
                var varMajor_HSE_Risk_Id = "0";
                var varSignUp_Id = "0";
                var varMajor_HSE_Work_Id = $(this).val();

                var data = {};
                data.Major_HSE_Risk_Id = varMajor_HSE_Risk_Id;
                data.SignUp_Id = varSignUp_Id;
                data.Major_HSE_Work_Id = varMajor_HSE_Work_Id;
                MajorRisk_List.push(data);
            });

            $(Building_id).each(function (i, b) {
                debugger;
                var item = {};
                item.Building_Id = b,
                    Building_Arr.push(item);
            });

            var Obj = {
                SignUp_Id: $("#SignUp_Id").val(),
                Business_Unit_Id: $("#Business_Unit_Id").val(),
                Zone_Id: $("#Zone_Id").val(),
                Community_Id: $("#Community_Id").val(),
                Building_Id: "0",
                Company_Name: $("#Company_Name").val(),
                Inc_Business_Unit_Type: $("#Inc_Business_Unit_Type").val(),
                Manager_Incharge: $("#Manager_Incharge").val(),
                Designation: $("#Designation").val(),
                Mobile_No: $("#Mobile_No").val(),
                Official_Email_Id: $("#Official_Email_Id").val(),
                Purchase_Order_Number: $("#Purchase_Order_Number").val(),
                DMS_Number: $("#DMS_Number").val(),
                HSE_Officer: $("#HSE_Officer").val(),
                HSE_Officer_Email: $("#HSE_Officer_Email").val(),
                HSE_Officer_Mobile_Number: $("#HSE_Officer_Mobile_Number").val(),
                Contract_Start_Date: $("#Contract_Start_Date").val(),
                Contract_End_Date: $("#Contract_End_Date").val(),
                W_Name_Position: $("#W_Name_Position").val(),
                W_Contact: $("#W_Contact").val(),
                W_Email_Id: $("#W_Email_Id").val(),
                W_Work_Description: $("#W_Work_Description").val(),
                W_Specific_Requirements: $("#W_Specific_Requirements").val(),
                W_From_Date: $("#W_From_Date").val(),
                W_To_date: $("#W_To_date").val(),
                W_Night_Schedule: $("input[type='radio'].G_Night_Shedule:checked").val(),
                W_Permit_Validity: $("#W_Permit_Validity").val(),
                W_Scope_of_work: $("#Scope_of_work").val(),
                Company_Id: $("#Company_Details").val(),
                W_Name_Position: $("#Project_Title").val(),
                W_Contact: $("#Contract_Type").val(),
                L_M_Work_Superviosor_List: Work_Superviosor_List,
                L_M_RequiredAttachments_List: RequiredAttachments_List,
                L_M_Major_HSE_Risk_List: MajorRisk_List,
                L_M_Building_List: Building_Arr
            };
            $.ajax({
                url: "/Account/Add_Service_Provider_Sign_Up",
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
                            title: 'Updated Successfully',
                            showConfirmButton: false,
                            timer: 1500
                        }).then(function () {
                            window.location.href = "/Account/Login";
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


    $("#F_AddCrisis").validate({
        rules: {
            LevelM_Id: {
                required: true,
            },
            Business_Unit_Id: {
                required: true,
            },
            Zone_Id: {
                required: true,
            },
            Emp_Id: {
                required: true,
            },
        },
        messages: {
            LevelM_Id: {
                required: "Please Select Level",
            },
            Business_Unit_Id: {
                required: "Please Select Business Unit",
            },
            Zone_Id: {
                required: "Please Select Zone",
            },
            Emp_Id: {
                required: "Please Select Employee",
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);

            $("#Business_Unit_Id-error").removeClass('mt-2');
            $("#Business_Unit_Id-error").addClass('m-100 text-danger');

            $("#Zone_Id-error").removeClass('mt-2');
            $("#Zone_Id-error").addClass('m-100 text-danger');

            $("#LevelM_Id-error").removeClass('mt-2');
            $("#LevelM_Id-error").addClass('m-100 text-danger');

            $("#Business_Unit_Id-error").removeClass('mt-2');
            $("#Business_Unit_Id-error").addClass('m-100 text-danger');

            $("#Emp_Id-error").removeClass('mt-2');
            $("#Emp_Id-error").addClass('m-100 text-danger');
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        },
        submitHandler: function () {
            debugger
            var EMP_ARR = [];
            var emp = $("#Emp_Id").val();

            $(emp).each(function (i, b) {
                debugger;
                var item = {};

                item.Crisis_Sub_Id = "0",
                    item.Crisis_Master_Id = "0",
                    item.Zone_Id = "0",
                    item.LevelM_Id = "0",
                    item.Emp_Id = b,
                EMP_ARR.push(item);
            });

            var Obj = {
                Crisis_Master_Id: "0",
                LevelM_Id: $("#LevelM_Id").val(),
                Business_Unit_Id: $("#Business_Unit_Id").val(),
                Zone_Id: $("#Zone_Id").val(),
                L_Crisis_SubEmp_Master_Details: EMP_ARR,
            };
            $.ajax({
                url: "/Master/AddCrisis_Master",
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

    function Load_Service_Provider(val) {
        debugger;
        var Hzurl = "";
        $(UI_Fields.VIEW_SERVICEPROVIDER_LIST).show(100);
        Hzurl = '/Account/_View_ServiceProvider_list';
        Hzurl += '/?Comp_Id=' + val;
        $(UI_Fields.VIEW_SERVICEPROVIDER_LIST).load(Hzurl);
    }


    $("#F_AddEmergencyAlert").validate({
        rules: {
            Emergency_Category_Id: {
                required: true,
            },
            Description: {
                required: true,
            },
            Zone_Id: {
                required: true,
            },
            Emergency_Alert_Date: {
                required: true,
            },
        },
        messages: {
            Emergency_Category_Id: {
                required: "Please Select Emergency Category",
            },
            Description: {
                required: "Please Enter Description",
            },
            Zone_Id: {
                required: "Please Select Zone",
            },
            Emergency_Alert_Date: {
                required: "Please Select Emergency Alert Date",
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);

            $("#Emergency_Category_Id-error").removeClass('mt-2');
            $("#Emergency_Category_Id-error").addClass('m-100 text-danger');

            $("#Zone_Id-error").removeClass('mt-2');
            $("#Zone_Id-error").addClass('m-100 text-danger');

            $("#Description-error").removeClass('mt-2');
            $("#Description-error").addClass('m-112 text-danger');

            $("#Emergency_Alert_Date-error").removeClass('mt-2');
            $("#Emergency_Alert_Date-error").addClass('m-100 text-danger');
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        },
        submitHandler: function () {
            debugger
            var Zone_ARR = [];
            var Zone = $("#Zone_Id").val();

            $(Zone).each(function (i, b) {
                debugger;
                var item = {};
                item.Emergency_SubZoneAlert_Id = "0",
                item.Emergency_Alert_Id = "0",
                item.Zone_Id = b,
                    Zone_ARR.push(item);
            });

            var Obj = {
                Emergency_Alert_Id: "0",
                Emergency_Category_Id: $("#Emergency_Category_Id").val(),
                Description: $("#Description").val(),
                Emergency_Date: $("#Emergency_Alert_Date").val(),
                L_Emergency_SubZone_Alert_Master: Zone_ARR,
            };
            $.ajax({
                url: "/Emergencymitigation/AddEmergencyAlert_Master",
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