$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#InvestigationForm").validate({
            rules: {
                IncidentReferenceStatus: {
                    required: true,
                },
                Total_Man_Days: {
                    required: true,
                },
                Involved_Department_Contractor: {
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
            },
            messages: {
                IncidentReferenceStatus: {
                    required: "Please Select Interim or Final",
                },
                Total_Man_Days: {
                    required: "Please enter total man days name",
                },
                Involved_Department_Contractor: {
                    required: "Please Select Department/Contractor",
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
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
               
            },
            submitHandler: function () {
                debugger
                var OtherParties = [];
                var Unsafe_Act = [];
                var Unsafe_Conditions = [];
                var Personal_Factor = [];
                var System_Factor = [];
                var Mechanism_of_Injury = [];
                var Agency_Source_of_Injury = [];

                $('#tblOtherparties tbody > tr').each(function () {
                    debugger
                    var varRole = $(this).closest('tr').find('.clsRole').val();
                    var varName = $(this).closest('tr').find('.clsname').val();
                    var varContactno = $(this).closest('tr').find('.clsContactno').val();

                    var data = {};
                    data.Inves_Other_Parties_Id = "0";
                    data.Inc_Id = $("#Inv_Inc_Id").val();
                    data.Role_Position = varRole;
                    data.Other_Name = varName;
                    data.Other_Contact_no = varContactno;
                    OtherParties.push(data);
                });

                $('.clsUnsafeAct:checked').each(function () {
                    var varInves_Unsafe_Act_Id = "0";
                    var varInvestigation_Id = "0";
                    var varInc_Id = $("#Inv_Inc_Id").val(),
                    var varM_Unsafe_Act_Id = $(this).val();

                    var data = {};
                    data.Inves_Unsafe_Act_Id = varInves_Unsafe_Act_Id;
                    data.Investigation_Id = varInvestigation_Id;
                    data.Inc_Id = varInc_Id;
                    data.M_Unsafe_Act_Id = varM_Unsafe_Act_Id;
                    Unsafe_Act.push(data);
                });


                var Obj = {
                    Inc_Id: $("#Inv_Inc_Id").val(),
                    Is_Ref: $("input[type='radio'].clsReference:checked").val(),
                    Total_Man_Days: $("#Total_Man_Days").val(),
                    Involved_Department_Contractor: $("#Involved_Department_Contractor").val(),
                    Description_circumstances: $("#Description_circumstances").val(),
                    Applicable_Reports: $("input[type='radio'].radioBtnClass:checked").val(),
                    Justification_Comments: $("#Justification_Comments").val(),
                    Additional_Information: $("#Additional_Information").val(),
                    Risk_Assessment_Environmental_Impact: $("#Risk_Assessment_Environmental_Impact").val(),
                    L_Inves_Other_Parties_Involved: OtherParties,
                    L_Inves_Immediate_Cause_Unsafe_Act: Unsafe_Act,

                };
                $.ajax({
                    url: "/Incident/AddIncidentInvestigation",
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
                                title: 'Visa Apply Successfully',
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
                                title: 'Visa Apply Error',
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