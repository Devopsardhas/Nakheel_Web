
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form
        $("#F_AM_Sp_Add_Scheduled_Initiated").validate({
            rules: {
                Audit_Emp_Id: {
                    required: true,
                },
                Service_Prov_Emp_Id: {
                    required: true,
                },
            },
            message: {
                Audit_Team_Id_0: {
                    required: "Please Select Audit Team",
                },
                SP_Rep_Id_0: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-10 text-danger');
                label.insertAfter(element);
                $("#Audit_Emp_Id-error").removeClass('mt-2');
                $("#Audit_Emp_Id-error").addClass('m-10 text-danger');

                $("#Service_Prov_Emp_Id-error").removeClass('mt-2');
                $("#Service_Prov_Emp_Id-error").addClass('m-10 text-danger');
            },
            submitHandler: function () {
                debugger;
                var varAuditTeamList = [];
                var varServiceProviderList = [];

                var varInternal_Audit_Id = $("#Internal_Audit_Id").val();
                var varReported_by = $("#Reported_by").text();
                var varDesignation = $("#Designation").text();


                $('#tbl_Internal_Audit_Team tbody > tr').each(function () {
                    var varAuditEmpId = $(this).closest('tr').find('.clsInt_Audit_Team').val();
                    var data = {};
                    data.Auditor_Id = varAuditEmpId;
                    varAuditTeamList.push(data);
                });
                $('#tbl_Internal_Service_Provider tbody > tr').each(function () {
                    var varServiceProvider = $(this).closest('tr').find('.clsInt_Service_Provider_Rep').val();
                    var data1 = {}
                    data1.Service_Provider_Id = varServiceProvider;
                    varServiceProviderList.push(data1);
                });
                var Obj = {
                    Internal_Audit_Id: varInternal_Audit_Id,
                    Reported_by: varReported_by,
                    Designation: varDesignation,
                    Status: "1",
                    L_Aud_Internal_Audit_Team: varAuditTeamList,
                    L_Aud_Internal_Service_Provider_Team: varServiceProviderList
                };
                $.ajax({
                    url: "/Audit/Add_Internal_Aud_Schedule",
                    type: "POST",
                    async: false,
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