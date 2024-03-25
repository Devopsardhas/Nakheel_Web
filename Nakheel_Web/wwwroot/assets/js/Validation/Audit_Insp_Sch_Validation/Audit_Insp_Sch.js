$(function () {
    'use strict';
    $("#Confined_Submit").click(function () {
        debugger
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
                Building: {
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
                Building: {
                    required: "Please Select Building"
                },
                C_Company_Name: {
                    required: "Please Enter Company"
                },
                C_Tittle_No: {
                    required: "Please Enter Tittle No"
                },
                C_Competent: {
                    required: "Please Enter Competent"
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

            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building-error").removeClass('mt-2');
                $("#Building-error").addClass('m-100 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var QNS_ARR = [];


                $('#Tbl_CSP tbody > tr').each(function () {
                    debugger
                    var varHSEWorkId = $(this).closest('tr').find('.major_HSE_Work_Id').val();
                    var varConfine_Ques_Ques = $(this).closest('tr').find('.Question_Name').text();
                    var RADIOVal = $(this).closest('tr').find("input:radio.Compliant:checked").val();
                    var Standard_Remark = $(this).closest('tr').find('.Remarks').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varHSEWorkId
                    data.CSP_Ques_Name = varConfine_Ques_Ques;
                    data.CSP_Ques_Radio_Btn = RADIOVal;
                    data.CSP_Remarks = Standard_Remark;
                    QNS_ARR.push(data);
                });
                var varDateTime = $("#C_Date_Time_Obs").val().replace("T", " ");
                var varDateTimeFrom = $("#C_From_Date").val().replace("T", " ");
                var varDateTimeTo = $("#C_To_Date").val().replace("T", " ");
                var lat = $("#Lat").val();
                var long = $("#Long").val();
                var Loc_Address = $("#search_input").val()

                var History_ARR = [];
                var His_Data = {};
               
                His_Data.CSP_Id = $("#Confined_ID").val(),
                    His_Data.History_Id = $("#History_Id").val(),
                    His_Data.Emp_Id = $("#Emp_Id").val(),
                    His_Data.Role_Id = $("#Role_Id").val(),
                    His_Data.Updated_DateTime = $("#His_Date_Time").val(),
                    History_ARR.push(His_Data);
                    

                if ($("#Lat").val() != "" || $("#Long").val() != "") {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                    
                }
                var Obj = {
                    CSP_Id: $("#Confined_ID").val(),
                    Date_Time_of_Observation: varDateTime,
                    Business_Unit_Id: $("#Business_Unit_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Type: $("#Con_Business_Unit_Type").val(),
                    Latitude: lat,
                    Longitude: long,
                    Company_Name: $("#C_Company_Name").val(),
                    Contrator_Title_No: $("#C_Tittle_No").val(),
                    Competent_Person: $("#C_Competent").val(),
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
                    _Add_CSP_Ques: QNS_ARR,
                    _Ptw_History_List: History_ARR
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
  




})