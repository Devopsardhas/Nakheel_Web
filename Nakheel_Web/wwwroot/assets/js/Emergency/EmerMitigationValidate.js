$(function () {

    'use strict';
    debugger
    $("#Emer_Mitigationt_Form").validate({

        rules: {
            Emer_Potential: {
                required: true,
            },
            Community: {
                required: true,
            },
            Building_Id: {
                required: true,
            },
            Emer_Situation: {
                required: true,
            },
            Emergency_Level: {
                required: true,
            },
            Emergency_Title: {
                required: true,
            },
            Emer_Description: {
                required: true,
            },
        },
        messages: {
            Emer_Potential: {
                required: "Please Enter Potential Risk"
            },
            Community: {
                required: "Please Select Community"
            },
            Building_Id: {
                required: "Please Select Building"
            },
            Emer_Situation: {
                required: "Please Enter Current Situation"
            },
            Emergency_Level: {
                required: "Please Select Emergency Level"
            },
            Emergency_Title: {
                required: "Please Enter Emergency Title"
            },
            Emer_Description: {
                required: "Please Enter Emergency Description"
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);

            $("#Community_Id-error").removeClass('mt-2');
            $("#Community_Id-error").addClass('m-100 text-danger');

            $("#Building_Id-error").removeClass('mt-2');
            $("#Building_Id-error").addClass('m-100 text-danger');

            $("#Emergency_Level-error").removeClass('mt-2');
            $("#Emergency_Level-error").addClass('m-100 text-danger');

            $("#Emergency_Title-error").removeClass('mt-2');
            $("#Emergency_Title-error").addClass('m-11 text-danger');


            $("#Emer_Description-error").removeClass('mt-2');
            $("#Emer_Description-error").addClass('m-11 text-danger');

            $("#Emer_Potential-error").removeClass('mt-2');
            $("#Emer_Potential-error").addClass('m-11 text-danger');

            $("#Emer_Situation-error").removeClass('mt-2');
            $("#Emer_Situation-error").addClass('m-11 text-danger');
            
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        },
        submitHandler: function () {

            var Emergency_Level_Arr = [];
            var Emergency_Level_List = $("#Emergency_Level").val();


            $(Emergency_Level_List).each(function (i, b) {
                debugger;
                var item = {};
                item.Emergency_Level_List = b,
                    Emergency_Level_Arr.push(item);
            });
            let commaSeperated = Emergency_Level_Arr.map(x => x.Emergency_Level_List).join(",");

            var obj = {
                Emer_Miti_Id: $("#Emer_Miti_Id").val(),
                Emer_Company: $("#Emer_Company").val(),
                Business_Unit_Id: $("#Business_Unit").val(),
                Zone_Id: $("#Zone").val(),
                Community_Id: $("#Community_Id").val(),
                Building_Id: $("#Building_Id").val(),
                Emer_Description: $("#Emer_Description").val(),
                Emer_Risk: $("#Emer_Potential").val(),
                Emer_Situation: $("#Emer_Situation").val(),
                Emer_level: commaSeperated,
                Emer_Title: $("#Emergency_Title").val(),
                Status: "1",
                CreatedBy: $("#CreatedBy").val(),
            }
            $.ajax({
                url: "/Emergencymitigation/AddEmer_Mitigation",
                type: "POST",
                cache: false,
                data: JSON.stringify(obj),
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
    });

});
