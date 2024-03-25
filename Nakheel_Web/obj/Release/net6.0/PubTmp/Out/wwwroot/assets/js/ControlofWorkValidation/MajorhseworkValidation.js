$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");

        $("#F_Add_Major_HSE_Work").validate({
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
                var form = $('#F_Add_Major_HSE_Work');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Insp_Sub_Cat_List = [];
                var varMajor_HSE_Work_Id = $("#Major_HSE_Work_Id").val();

                $('#tblMajorHSEList tbody > tr.Modified').each(function () {
                    var varMajor_HSE_Ques_Id = $(this).closest('tr').find('.Major_HSE_Ques_Id').val();
                    var varMajor_Questionnaires_Name = $(this).closest('tr').find('.Major_Questionnaires_Name').val();
                    var data = {};
                    data.Major_HSE_Work_Id = varMajor_HSE_Work_Id;
                    data.Major_HSE_Ques_Id = varMajor_HSE_Ques_Id;
                    data.Major_Questionnaires_Name = varMajor_Questionnaires_Name;
                    Insp_Sub_Cat_List.push(data);
                });
                var len = Insp_Sub_Cat_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/ControlOfWork/Major_HSE_Question_Add",
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
    });
});