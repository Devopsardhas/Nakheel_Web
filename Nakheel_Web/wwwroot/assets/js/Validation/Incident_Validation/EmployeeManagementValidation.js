
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Employee_Master").validate({
            rules: {
                Employee_Type: {
                    required: true,
                },
                First_Name: {
                    required: true,
                },
                //Mobile_Number: {
                //    required: true,
                //    minlength: 10
                //},
                Email_Id: {
                    required: true,
                    //minlength: 10
                },
                Health_Safety_Type_List: {
                    required: true,
                    minlength: 1
                },
                Zone_Name: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Building: {
                    required: true,
                },
                Business_Unit_Id: {
                    required: true,
                },
                Department: {
                    required: true,
                },
                Role_Name: {
                    required: true,
                },
                Designation_Name: {
                    required: true,
                },
                Line_Manager_Name: {
                    required: true,
                },
            },
            messages: {
                Employee_Type: {
                    required: "Please Select Employee Type",
                },
                First_Name: {
                    required: "Please Enter First Name",
                    minlength: "Name must consist of at least 5 characters"
                },
                //Mobile_Number: {
                //    required: "Please Enter Mobile Number",
                //    minlength: "Name must consist of at least 10 characters"
                //},
                Email_Id: {
                    required: "Please Enter Email ID",
                },
                Business_Unit_Id: {
                    required: "Please Select Business Unit",
                },
                Zone_Name: {
                    required: "Please Select Zone",
                },
                Department: {
                    required: "Please Select Department",
                },
                Role_Name: {
                    required: "Please Select Role",
                },
                Designation_Name: {
                    required: "Please Select Designation",
                },
                Line_Manager_Name: {
                    required: "Please Select Line Manager",
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
                var Community_List = [];
                var Building_List = [];
                var Master_Community_List = [];
                
                $('.clsHealth_Safety:checked').each(function () {
                    var varEmp_Community_Id = "0";
                    var varCommunity_Id = $(this).val();

                    var data = {};
                    data.Emp_Community_Id = varEmp_Community_Id;
                    data.Community_Id = varCommunity_Id;
                    Community_List.push(data);
                });
                $('.clsHealth_Safety_1:checked').each(function () {
                    var varEmp_Building_Id = "0";
                    var varBuilding_Id = $(this).val();
                    
                    var data = {};
                    data.Emp_Building_Id = varEmp_Building_Id;
                    data.Building_Id = varBuilding_Id;
                    Building_List.push(data);
                });
                $('.clsHealth_Safety_2:checked').each(function () {
                    var varEmp_Master_Community_Id = "0";
                    var varMaster_Community_Id = $(this).val();

                    var data = {};
                    data.Emp_Master_Community_Id = varEmp_Master_Community_Id;
                    data.Master_Community_Id = varMaster_Community_Id;
                    Master_Community_List.push(data);
                });
              
                var Obj = {
                    Employee_Identity_Id: $("#Employee_Identity_Id").val(),
                    Employee_Id: $("#EmployeeId").val(),
                    Employee_Type: $("#Employee_Type").val(),
                    First_Name: $("#First_Name").val(),
                    Last_Name: $("#Last_Name").val(),
                    Password: $("#Password").val(),
                    Email_Id: $("#Email_Id").val(),
                    Mobile_Number: $("#Mobile_Number").val(),
                    Address: $("#Address").val(),
                    State: $("#State").val(),
                    Country: $("#Country").val(),
                    Business_Unit_Id: $("#Business_Unit_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Department_Id: $("#Department").val(),
                    Role_Id: $("#Role_Name").val(),
                    Designation_Id: $("#Designation_Name").val(),
                    Line_Manager_Id: $("#Line_Manager_Name").val(),
                    L_Employee_Community_List: Community_List,
                    L_Employee_Building_List: Building_List,
                    L_Employee_Master_Community_List: Master_Community_List
                };
                $.ajax({
                    url: "/Master/AddEmployeeMaster",
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