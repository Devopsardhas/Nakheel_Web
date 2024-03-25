
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Service_Monthly").validate({
            rules: {
                Business_Unit: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Business_Unit_Type: {
                    required: true,
                },
                Zone_Id: {
                    required: true,
                },
                Building_Id: {
                    required: true,
                },
                Year_Id: {
                    required: true,
                },
                Month_Id: {
                    required: true,
                },
                Emp_ID: {
                    required: true,
                },
                Tot_Work: {
                    required: true,
                },
                Tr_Conduct: {
                    required: true,
                },   
                Contractor_Id: {
                    required: true,
                },
                Legal_Files: {
                    required: true,
                },
                Other_Files: {
                    required: true,
                },
            },
            messages: {
                Business_Unit: {
                    required: "Please Select The Business Unit",
                },
                Community: {
                    required: "Please Select Community",
                },
                Business_Unit_Type: {
                    required: "Please Select Business Unit Type",
                },
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Building_Id: {
                    required: "Please Select Building",
                },
                Year_Id: {
                    required: "Please Select Year",
                },
                Month_Id: {
                    required: "Please Select  Month",
                },
                Emp_ID: {
                    required: "Please Enter Average Daily Man Power",
                },
                Tot_Work: {
                    required: "Please Enter Man Hours",
                },
                Tr_Conduct: {
                    required: "Please Enter Training Conducted",
                },
                Contractor_Id: {
                    required: "Please select DMS Number",
                },
                Legal_Files: {
                    required: "Please Choose File",
                },
                Other_Files: {
                    required: "Please Choose File",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-100 text-danger');

                $("#Community-error").removeClass('mt-2');
                $("#Community-error").addClass('m-100 text-danger');

                $("#Business_Unit_Type-error").removeClass('mt-2');
                $("#Business_Unit_Type-error").addClass('m-100 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                //$("#Year_Id-error").removeClass('mt-2');
                //$("#Year_Id-error").addClass('m-100 text-danger');

                //$("#Month_Id-error").removeClass('mt-2');
                //$("#Month_Id-error").addClass('m-100 text-danger');

                //$("#Emp_ID-error").removeClass('mt-2');
                //$("#Emp_ID-error").addClass('m-100 text-danger');

                //$("#Tot_Work-error").removeClass('mt-2');
                //$("#Tot_Work-error").addClass('m-100 text-danger');

                //$("#Tr_Conduct-error").removeClass('mt-2');
                //$("#Tr_Conduct-error").addClass('mt-2 text-danger');
              
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger
                var Legal_Arr = [];
                var Other_Arr = [];
                var Training_ARR = [];
                var Training_Other_ARR = [];
                var varLegal_file = $(".UploadLegal_Files").val();
                var varOther_file = $(".UploadOther_Files").val();


                if ((varLegal_file != "") && (varLegal_file != undefined) && (varLegal_file != null)) {
                    var valNew = varLegal_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.TR_Legal_File_Id = "0";
                        File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                        File_data.Legal_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Legal_Arr.push(File_data);
                    }
                }

                if ((varOther_file != "") && (varOther_file != undefined) && (varOther_file != null)) {
                    var valNew = varOther_file.split(',');
                    for (var i = 0; i < valNew.length; i++) {
                        var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                        var File_data = {};
                        File_data.TR_Other_File_Id = "0";
                        File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                        File_data.Other_File_Path = t;
                        File_data.CreatedBy = $("#CreatedBy").val();
                        Other_Arr.push(File_data);
                    }
                }

                $('#tblTrainingList tbody > tr').each(function () {
                    debugger;
                    var Training_file_ARR = [];
                    
                    var vartrainingId = $(this).closest('tr').find('.Service_Training_Id').val();
                    var vartrainingName = $(this).closest('tr').find('.Training_Name').val();
                    var vartrainingRemarks = $(this).closest('tr').find('.Training_Remarks').val();
                    var varclosure = $(this).closest('tr').find('.UploadTraining_Files').val();
                    var varfileedit = $(this).closest('tr').find('.TR_File_Upload_Pdf').val();

                    
                    if ((varclosure != "") && (varclosure != undefined) && (varclosure != null)) {
                        debugger;
                        var valNew = varclosure.split(',');
                        for (var i = 0; i < valNew.length; i++) {
                            var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                            var File_data = {};
                            File_data.TR_File_Id = "0";
                            File_data.Service_Training_Id = vartrainingId;
                            File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                            File_data.Training_File_Path = t;
                            File_data.CreatedBy = $("#CreatedBy").val();
                            Training_file_ARR.push(File_data);
                        }
                    }
                    else {
                        if (varfileedit != null) {
                            debugger;
                            $(this).closest('tr').find('.TR_File_Upload_Pdf').each(function (i, e) {
                            /*$(varfileedit).each(function (i, e) {*/
                                debugger;
                                var File_data = {};
                                File_data.TR_File_Id = "0";
                                File_data.Service_Training_Id = vartrainingId;
                                File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                                File_data.Training_File_Path = e.defaultValue;
                                File_data.CreatedBy = $("#CreatedBy").val();
                                Training_file_ARR.push(File_data);
                            });
                        }
                    }
                    var data = {};
                    data.Service_Training_Id = vartrainingId;
                    data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                    data.Service_Training_Name = vartrainingName;
                    data.Service_Remarks = vartrainingRemarks;
                    data.CreatedBy = $("#CreatedBy").val();
                    data._Training_Files = Training_file_ARR;
                    Training_ARR.push(data);
                });

                $('#tblTrainingListOther tbody > tr').each(function () {
                    debugger;
                    var Training_file_Other_ARR = [];

                    var varothertrainingId = $(this).closest('tr').find('.Service_Other_Training_Id').val();
                    var varothertrainingName = $(this).closest('tr').find('.Training_Des').val();
                    var varothertrainingRemarks = $(this).closest('tr').find('.Other_Remarks').val();
                    var varotherclosure = $(this).closest('tr').find('.UploadTraining_FilesOther').val();
                    var varotherfileedit = $(this).closest('tr').find('.TR_File_Upload_Pdf_Other').val();


                    if ((varotherclosure != "") && (varotherclosure != undefined) && (varotherclosure != null)) {
                        debugger;
                        var valNew = varotherclosure.split(',');
                        for (var i = 0; i < valNew.length; i++) {
                            var t = valNew[i].replace(/[\(\)\[\]{}'"]/g, "");
                            var File_data = {};
                            File_data.TR_Other_File_Id = "0";
                            File_data.Service_Other_Training_Id = varothertrainingId;
                            File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                            File_data.Training_Other_File_Path = t;
                            File_data.CreatedBy = $("#CreatedBy").val();
                            Training_file_Other_ARR.push(File_data);
                        }
                    }
                    else {
                        if (varotherfileedit != null) {
                            debugger;
                            $(this).closest('tr').find('.TR_File_Upload_Pdf_Other').each(function (i, e) {
                                debugger;
                                var File_data = {};
                                File_data.TR_Other_File_Id = "0";
                                File_data.Service_Other_Training_Id = varothertrainingId;
                                File_data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                                File_data.Training_Other_File_Path = e.defaultValue;
                                File_data.CreatedBy = $("#CreatedBy").val();
                                Training_file_Other_ARR.push(File_data);
                            });
                        }
                    }
                    var data = {};
                    data.Service_Other_Training_Id = varothertrainingId;
                    data.Service_Monthly_Id = $("#Service_Monthly_Id").val();
                    data.Service_Other_Training_Name = varothertrainingName;
                    data.Service_Other_Remarks = varothertrainingRemarks;
                    data.CreatedBy = $("#CreatedBy").val();
                    data._Training_Other_Files = Training_file_Other_ARR;
                    Training_Other_ARR.push(data);
                });


                var Obj = {
                    Service_Monthly_Id: $("#Service_Monthly_Id").val(),
                    Service_Year: $("#Year_Id").val(),
                    Service_Month: $("#Month_Id").val(),
                    Service_Total_Employees: $("#Emp_ID").val(),
                    Service_Man_of_Hours: $("#Tot_Work").val(),
                    Service_Training_Conducted: $("#Tr_Conduct").val(),
                    CreatedBy: $("#CreatedBy").val(),   
                    Business_Unit_Id: $("#Business_Unit_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Building_Id: $("#Building_Id").val(),
                    Business_Unit_Type: $("#Business_Unit_Type").val(),
                    DMS_Number_Id: $("#Contractor_Id").val(),
                    _Legal_Files: Legal_Arr,
                    _Other_Files: Other_Arr,
                    _Training_List: Training_ARR,
                    _Training_Other_List: Training_Other_ARR,
                };

                $.ajax({
                    url: "/ServiceMonthlyStatistics/Add_Service_Monthly_Statics",
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