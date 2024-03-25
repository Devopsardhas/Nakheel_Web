
$(function () {
    'use strict';
    
    $(function () {
        
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Observation_Report").validate({
            rules: {
                Company_Name: {
                    required: true,
                },
                Contact_Name: {
                    required: true,
                },
                Health_Safety_Type_List: {
                    required: true,
                    minlength: 1
                },
                Obser_Date: {
                    required: true,
                },
                Zone: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Obser_Time: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                Business_Unit: {
                    required: true,
                },
                Category: {
                    required: true,
                    minlength: 1
                },
                Observation_Type: {
                    required: true,
                    minlength: 1
                },
                Obser_Details: {
                    required: true,
                },
                Imm_Corrective_Actions: {
                    required: true,
                },
                Environment_Type_List: {
                    required: true,
                    minlength: 1
                },
                Location: {
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
                Sel_Incident_Cat: {
                    required: "Please Select Incident Category",
                },
                Health_Safety_Type_List: {
                    required: "Please Select Health & Safety Type",
                },
                Environment_Type_List: {
                    required: "Please Select Environment Type",
                },
                Obser_Date: {
                    required: "Please Select Date",
                },
                Zone: {
                    required: "Please Select Zone",
                },
                Community: {
                    required: "Please Select Community",
                },
                Obser_Time: {
                    required: "Please Select Time",
                },
                Building_Id: {
                    required: "Please Select Building",
                },
                Business_Unit: {
                    required: "Please Select Business Unit",
                },
                Category: {
                    required: "Please Select Category",
                    minlength: 1
                },
                Observation_Type: {
                    required: "Please Select Observation Type",
                    minlength: 1
                },
                Obser_Details: {
                    required: "Please Enter Description Observation",
                    minlength: 1
                },
                Imm_Corrective_Actions: {
                    required: "Please Enter Immediate Action Taken",
                    minlength: 1
                },
                Location: {
                    required: "Please Enter Location Required",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-100 text-danger');

                $("#Zone-error").removeClass('mt-2');
                $("#Zone-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#Health_Safety_Type_List-error").removeClass('mt-2');
                $("#Health_Safety_Type_List-error").addClass('m-10 text-danger');

                $("#Environment_Type_List-error").removeClass('mt-2');
                $("#Environment_Type_List-error").addClass('m-10 text-danger');
                //Location
                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-101 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Health_Safety_Type_List = [];
                var Environment_Type_List = [];
                var Observation_Type = "";
                var Category = "";

                if ($("input[type='radio'].observation_Type").is(':checked')) {
                    Observation_Type = $("input[type='radio'].observation_Type:checked").val();
                }
                if ($("input[type='radio'].category").is(':checked')) {
                    Category = $("input[type='radio'].category:checked").val();
                }
                if (Observation_Type == "Health_Safety") {
                    $('.clsHealth_Safety').each(function () {
                        var varInc_Obser_Health_Id = "0";
                        var varHealth_Safety_Id = $(this).val();
                        
                        var data = {};
                        data.Inc_Obser_Health_Id = varInc_Obser_Health_Id;
                        data.Health_Safety_Id = varHealth_Safety_Id;
                        Health_Safety_Type_List.push(data);
                    });
                }
                else if (Observation_Type == "Environment") {

                    $('.clsEnvironment_Type').each(function () {
                        var varInc_Envir_ObserType_Id = "0";
                        var varEnvironment_Id = $(this).val();

                        var data = {};
                        data.Inc_Envir_ObserType_Id = varInc_Envir_ObserType_Id;
                        data.Environment_Id = varEnvironment_Id;
                        Environment_Type_List.push(data);
                    });
                }
                var lat = $("#Lat").val(); 
                var long = $("#Long").val();

                if ($("#Lat").val() != "" || $("#Long").val() != "") {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                }
                //debugger
                var Obj = {
                    Inc_Obser_Report_Id: $("#Inc_Obser_Report_Id").val(),
                    Obser_Date: $("#Obser_Date").val(),
                    Obser_Time: $("#Obser_Time").val(),
                    Zone_Id: $("#Zone").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Id: $("#Business_Unit").val(),
                    Community_Id: $("#Community_Id").val(),
                    Master_Community_Id: $("#Master_Community_Id").val(),
                    Company_Name: $("#Company_Name").val(),
                    Contact_Name: $("#Contact_Name").val(),
                    Mobile_Number: $("#Mobile_Number").val(),
                    Obser_Details: $("#Obser_Details").val(),
                    Category: Category,
                    Observation_Type: Observation_Type,
                    Description_Observation: $("#Description_Observation").val(),
                    Imm_Corrective_Actions: $("#Imm_Corrective_Actions").val(),
                    Loc_Latitude: lat,
                    Loc_Longitude: long,
                    Master_Community_Name: $("#search_input_1").val(), 
                    Business_Unit_Type_Name: $("#Obs_Business_Unit_Type").val(), 
                    L_Inc_Health_Observation_Type: Health_Safety_Type_List,
                    L_Inc_Environment_Observation_Type: Environment_Type_List
                };
                $.ajax({
                    url: "/Incident/AddObservationReport",
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