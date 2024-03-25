
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Hse_Bulletin").validate({
            rules: {
                HSE_Bulletin_Name: {
                    required: true,
                },

            },
            messages: {
                HSE_Bulletin_Name: {
                    required: "Please Enter Name",
                },
                

            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#HSE_Bulletin_Name-error").removeClass('mt-2');
                $("#HSE_Bulletin_Name-error").addClass('m-100 text-danger');
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var Bulletin_File_Upl_Arr = [];
                var varFile_Upload = $(".UploadEvidence").val();
             
                if ((varFile_Upload != "") && (varFile_Upload != undefined) && (varFile_Upload != null)) {
                    var valNew = varFile_Upload.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.Bulletin_File_Upl_Id = "0";
                        File_data.HSE_Bulletin_Id = $("#HSE_Bulletin_Id").val();
                        File_data.File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Bulletin_File_Upl_Arr.push(File_data);
                    }
                }
                var model = {
                    HSE_Bulletin_Id: $("#HSE_Bulletin_Id").val(),
                    HSE_Bulletin_Name: $("#HSE_Bulletin_Name").val(),
                    CreatedBy: $("#CreatedBy").val(),
                    File_Upl_List: Bulletin_File_Upl_Arr,
                };

                $.ajax({
                    url: "/Master/AddHseBulletin",
                    type: "POST",
                    cache: false,
                    data: JSON.stringify(model),
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