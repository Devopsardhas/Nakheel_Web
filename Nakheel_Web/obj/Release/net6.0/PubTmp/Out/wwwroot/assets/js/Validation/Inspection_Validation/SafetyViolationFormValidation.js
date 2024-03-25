
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Add_Insp_Safety_Violation").validate({
            rules: {
                Responsible_Dept: {
                    required: true,
                },
                Description_Violation: {
                    required: true,
                },
                Requirement: {
                    required: true,
                },
                Type_Violation_Mas_Id: {
                    required: true,
                    minlength: 1
                },
                Type_Violation_Others_Text: {
                    required: true,
                },
                Emp_Service_provider: {
                    required: true,
                },
            },
            messages: {
                Responsible_Dept: {
                    required: "Please Select Responsible Department",
                },
                Description_Violation: {
                    required: "Please Enter Description of Violation",
                },
                Requirement: {
                    required: "Please Enter Requirement",
                },
                Type_Violation_Mas_Id: {
                    required: "Please Select Type of Violation",
                },
                Type_Violation_Others_Text: {
                    required: "Please Enter Other Type of Violation",
                },
                Emp_Service_provider: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Responsible_Dept-error").removeClass('mt-2');
                $("#Responsible_Dept-error").addClass('m-100 text-danger');

                $("#Description_Violation-error").removeClass('mt-2');
                $("#Description_Violation-error").addClass('m-100 text-danger');

                $("#Requirement-error").removeClass('mt-2');
                $("#Requirement-error").addClass('m-100 text-danger');

                $("#Type_Violation_Mas_Id-error").removeClass('mt-2');
                $("#Type_Violation_Mas_Id-error").addClass('m-10 text-danger');

                $("#Emp_Service_provider-error").removeClass('mt-2');
                $("#Emp_Service_provider-error").addClass('m-100 text-danger');
            },

            submitHandler: function () {
                $('.Safe_btn_Submit').prop('disabled', true);
                var Type_Violation_List = [];
                var varSafety_Violation_Id = $("#Safety_Violation_Id").val();
                $('.Type_Violation_Mas_Id:checked').each(function () {
                    var varType_of_Violation_Id = "0";
                    var varType_Master_Id = $(this).val();
                    var varOthers_Name = $("#Type_Violation_Others_Text").val();

                    var data = {};
                    data.Type_of_Violation_Id = varType_of_Violation_Id;
                    data.Safety_Violation_Id = varSafety_Violation_Id;
                    data.Type_Master_Id = varType_Master_Id;
                    data.Others_Name = varOthers_Name;
                    Type_Violation_List.push(data);
                });

                var Obj = {
                    Insp_Request_Id: $("#Insp_Request_Id").val(),
                    Safety_Violation_Id: varSafety_Violation_Id,
                    Responsible_Dept: $("input[name='Responsible_Dept']:checked").val(),
                    Description_Violation: $("#Description_Violation").val(),
                    Requirement: $("#Requirement").val(),
                    Emp_Service_provider: $("#Emp_Service_provider").val(),
                    Safety_Violation_Sub_Type_List: Type_Violation_List,

                };
                $.ajax({
                    url: "/Inspection/Insp_Safety_Violation_Add",
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
                                $('.Safe_btn_Submit').prop('disabled', false);
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
                            $('.Safe_btn_Submit').prop('disabled', false);
                        }
                        $('.Safe_btn_Submit').prop('disabled', false);
                    },
                });
            }
        });


        $("#F_Add_Create_Safety_Vio").validate({
            rules: {
                Business_Unit_Id: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Community_Id: {
                    required: true,
                },
                Inspection_Date: {
                    required: true,
                },
                Responsible_Dept: {
                    required: true,
                },
                Description_Violation: {
                    required: true,
                },
                Requirement: {
                    required: true,
                },
                Type_Violation_Mas_Id: {
                    required: true,
                    minlength: 1
                },
                Type_Violation_Others_Text: {
                    required: true,
                },
                Emp_Service_provider: {
                    required: true,
                },
            },
            messages: {
                Business_Unit_Id: {
                    required: "Please Select Business Unit",
                },
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Community_Id: {
                    required: "Please Select Community",
                },
                Responsible_Dept: {
                    required: "Please Select Inspection Date",
                },
                Responsible_Dept: {
                    required: "Please Select Responsible Department",
                },
                Description_Violation: {
                    required: "Please Enter Description of Violation",
                },
                Requirement: {
                    required: "Please Enter Requirement",
                },
                Type_Violation_Mas_Id: {
                    required: "Please Select Type of Violation",
                },
                Type_Violation_Others_Text: {
                    required: "Please Enter Other Type of Violation",
                },
                Emp_Service_provider: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit_Id-error").removeClass('mt-2');
                $("#Business_Unit_Id-error").addClass('text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('text-danger');

                $("#Responsible_Dept-error").removeClass('mt-2');
                $("#Responsible_Dept-error").addClass('m-11 text-danger');

                $("#Description_Violation-error").removeClass('mt-2');
                $("#Description_Violation-error").addClass('m-100 text-danger');

                $("#Requirement-error").removeClass('mt-2');
                $("#Requirement-error").addClass('m-100 text-danger');

                $("#Type_Violation_Mas_Id-error").removeClass('mt-2');
                $("#Type_Violation_Mas_Id-error").addClass('m-10 text-danger');

                $("#Emp_Service_provider-error").removeClass('mt-2');
                $("#Emp_Service_provider-error").addClass('m-100 text-danger');

                ApplySelect2(".Drop_Search");
            },

            submitHandler: function () {
                $('.Safe_btn_Submit').prop('disabled', true);
                var Type_Violation_List = [];
                var varSafety_Violation_Id = $("#Safety_Violation_Id").val();
                $('.Type_Violation_Mas_Id:checked').each(function () {
                    var varType_of_Violation_Id = "0";
                    var varType_Master_Id = $(this).val();
                    var varOthers_Name = $("#Type_Violation_Others_Text").val();

                    var data = {};
                    data.Type_of_Violation_Id = varType_of_Violation_Id;
                    data.Safety_Violation_Id = varSafety_Violation_Id;
                    data.Type_Master_Id = varType_Master_Id;
                    data.Others_Name = varOthers_Name;
                    Type_Violation_List.push(data);
                });

                var Obj = {
                    Business_Unit_Id: $("#Business_Unit_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Inspection_Date: $("#Inspection_Date").val(),
                    Insp_Request_Id: "0",
                    Safety_Violation_Id: "0",
                    Responsible_Dept: $("input[name='Responsible_Dept']:checked").val(),
                    Description_Violation: $("#Description_Violation").val(),
                    Requirement: $("#Requirement").val(),
                    Emp_Service_provider: $("#Emp_Service_provider").val(),
                    Safety_Violation_Sub_Type_List: Type_Violation_List,

                };
                $.ajax({
                    url: "/Inspection/Insp_Safety_Violation_Create",
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
                                $('.Safe_btn_Submit').prop('disabled', false);
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
                            $('.Safe_btn_Submit').prop('disabled', false);
                        }
                        $('.Safe_btn_Submit').prop('disabled', false);
                    },
                });
            }
        });

    });
});