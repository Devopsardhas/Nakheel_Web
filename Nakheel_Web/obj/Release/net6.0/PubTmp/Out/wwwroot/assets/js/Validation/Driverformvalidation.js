$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form
        $("#DriverMaster").validate({
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
                var Vec_Documents = [];
                $('#tblDirectContact tbody > tr').each(function () {
                    debugger
                    var varDocumentsID = $(this).closest('tr').find('.DocumentsID').val();
                    var varVehicle_Documents = $(this).closest('tr').find('.Vehicle_Documents').val();
                    var varVehicle_Documents_File = $(this).closest('tr').find('.hdncls_Vehicle_Documents_File').val();
                    var varEx_date = $(this).closest('tr').find('.Ex_date').val();

                    var data = {};
                    data.Vec_Doc_Id = varDocumentsID;
                    data.Vehicle_Type_Id = varVehicle_Documents;
                    data.Vehicle_Doc_File_Path = varVehicle_Documents_File;
                    data.Expire_Date = varEx_date;
                    Menu.push(data);
                });

                var Obj = {
                    Driver_Id: $("#Driver_Id").val(),
                    First_Name: $("#First_Name").val(),
                    Vehicle_Documents_Details: Vec_Documents
                };
                $.ajax({
                    url: '@Html.Raw(@Url.Action("AddDriver", "Driver"))',
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