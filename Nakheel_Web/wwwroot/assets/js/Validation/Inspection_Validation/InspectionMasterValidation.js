
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Add_Insp_Sub_Cat").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var form = $('#F_Add_Insp_Sub_Cat');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Insp_Sub_Cat_List = [];
                var varInsp_Category_Id = $("#Insp_Category_Id").val();
               
                $('#tblInspCatList tbody > tr.Modified').each(function () {
                        var varInsp_Sub_Category_Id = $(this).closest('tr').find('.Insp_Sub_Category_Id').val();
                        var varInsp_Sub_Category_Name = $(this).closest('tr').find('.Insp_Sub_Category_Name').val();
                        var data = {};
                        data.Insp_Category_Id = varInsp_Category_Id;
                        data.Insp_Sub_Category_Id = varInsp_Sub_Category_Id;
                        data.Insp_Sub_Category_Name = varInsp_Sub_Category_Name;
                        Insp_Sub_Cat_List.push(data);
                    });
                var len = Insp_Sub_Cat_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Inspection/Insp_Sub_Category_Add",
                        dataType: 'json',
                        data: { model: Insp_Sub_Cat_List },
                        headers: {
                            RequestVerificationToken: token
                        },
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
                } else {
                    toastr["error"]("Audit Sub-Topics should not be empty!.");
                }
            }
        });


        $("#F_Add_Insp_Topic").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var form = $('#F_Add_Insp_Topic');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Insp_Sub_Cat_List = [];
                var varInsp_Type_Id = $("#Insp_Type_Id").val();

                $('#tblInspCatList tbody > tr.Modified').each(function () {
                    var varInsp_Topic_Id = $(this).closest('tr').find('.Insp_Topic_Id').val();
                    var varInsp_Topic_Name = $(this).closest('tr').find('.Insp_Topic_Name').val();
                    var data = {};
                    data.Insp_Type_Id = varInsp_Type_Id;
                    data.Insp_Topic_Id = varInsp_Topic_Id;
                    data.Insp_Topic_Name = varInsp_Topic_Name;
                    Insp_Sub_Cat_List.push(data);
                });
                var len = Insp_Sub_Cat_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Inspection/Insp_Topic_Add",
                        dataType: 'json',
                        data: { model: Insp_Sub_Cat_List },
                        headers: {
                            RequestVerificationToken: token
                        },
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
                } else {
                    toastr["error"]("Audit Sub-Topics should not be empty!.");
                }
            }
        });

        $("#F_Add_Insp_Questionnaires").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var form = $('#F_Add_Insp_Questionnaires');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Insp_Questionnaires_List = [];
                var varInsp_Type_Id = $("#Insp_Type_Id").val();
                var varInsp_Topic_Id = $("#Insp_Topic_Id").val();

                $('#tbl_Insp_Questionnaires_List tbody > tr.Modified').each(function () {
                    var varInsp_Questionnaires_Id = $(this).closest('tr').find('.Insp_Questionnaires_Id').val();
                    var varInsp_Questionnaires_Name = $(this).closest('tr').find('.Insp_Questionnaires_Name').val();
                    var data = {};
                    data.Insp_Type_Id = varInsp_Type_Id;
                    data.Insp_Topic_Id = varInsp_Topic_Id;
                    data.Insp_Questionnaires_Id = varInsp_Questionnaires_Id;
                    data.Insp_Questionnaires_Name = varInsp_Questionnaires_Name;
                    Insp_Questionnaires_List.push(data);
                });
                var len = Insp_Questionnaires_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Inspection/Insp_Questionnaires_Add",
                        dataType: 'json',
                        data: { model: Insp_Questionnaires_List },
                        headers: {
                            RequestVerificationToken: token
                        },
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
                } else {
                    toastr["error"]("Inspection Questionnaires should not be empty!.");
                }
            }
        });

        $("#F_Add_Insp_Landscape_Sub_Cat").validate({
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
                var form = $('#F_Add_Insp_Landscape_Sub_Cat');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Landscape_Sub_Cat_List = [];
                var varInsp_Landscap_Mas_Id = $("#Insp_Landscap_Mas_Id").val();

                $('#tblInspCatList tbody > tr.Modified').each(function () {
                    var varInsp_Landscap_Sub_Mas_Id = $(this).closest('tr').find('.Insp_Landscap_Sub_Mas_Id').val();
                    var varInsp_Landscap_Sub_Mas_Name = $(this).closest('tr').find('.Insp_Landscap_Sub_Mas_Name').val();
                    var data = {};
                    data.Insp_Landscap_Mas_Id = varInsp_Landscap_Mas_Id;
                    data.Insp_Landscap_Sub_Mas_Id = varInsp_Landscap_Sub_Mas_Id;
                    data.Insp_Landscap_Sub_Mas_Name = varInsp_Landscap_Sub_Mas_Name;
                    Landscape_Sub_Cat_List.push(data);
                });
                var len = Landscape_Sub_Cat_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Inspection/Insp_Landscap_Sub_Cat_Add",
                        dataType: 'json',
                        data: { model: Landscape_Sub_Cat_List },
                        headers: {
                            RequestVerificationToken: token
                        },
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
                } else {
                    toastr["error"]("Landscape checklist should not be empty!.");
                }
            }
        });

        $("#F_Add_Insp_SoftService_Sub_Cat").validate({
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
                var form = $('#F_Add_Insp_SoftService_Sub_Cat');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Landscape_Sub_Cat_List = [];
                var varInsp_Landscap_Mas_Id = $("#Insp_Landscap_Mas_Id").val();

                $('#tblInspCatList tbody > tr.Modified').each(function () {
                    var varInsp_Landscap_Sub_Mas_Id = $(this).closest('tr').find('.Insp_Landscap_Sub_Mas_Id').val();
                    var varInsp_Landscap_Sub_Mas_Name = $(this).closest('tr').find('.Insp_Landscap_Sub_Mas_Name').val();
                    var data = {};
                    data.Insp_Landscap_Mas_Id = varInsp_Landscap_Mas_Id;
                    data.Insp_Landscap_Sub_Mas_Id = varInsp_Landscap_Sub_Mas_Id;
                    data.Insp_Landscap_Sub_Mas_Name = varInsp_Landscap_Sub_Mas_Name;
                    Landscape_Sub_Cat_List.push(data);
                });
                var len = Landscape_Sub_Cat_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Inspection/Insp_SoftService_Sub_Cat_Add",
                        dataType: 'json',
                        data: { model: Landscape_Sub_Cat_List },
                        headers: {
                            RequestVerificationToken: token
                        },
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
                } else {
                    toastr["error"]("SoftService checklist should not be empty!.");
                }
            }
        });
    });
});