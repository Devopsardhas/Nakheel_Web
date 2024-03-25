
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_Joint_Add_Insp_Request").validate({
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
                Building_Id: {
                    required: true,
                },
                Inspection_Date: {
                    required: true,
                },
                Category_Id: {
                    required: true,
                },
                Supervisor_Id: {
                    required: true,
                },
                Service_Provider_Id: {
                    required: true,
                },
                HSE_Representative_Id: {
                    required: true,
                },
                Schedule_Type: {
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
                Building_Id: {
                    required: "Please Select Building",
                },
                Category_Id: {
                    required: "Please Select Category Id",
                },
                Supervisor_Id: {
                    required: "Please Select Supervisor",
                },
                Service_Provider_Id: {
                    required: "Please Select Service Provider",
                },
                HSE_Representative_Id: {
                    required: "Please Select HSE Representative",
                },
                Schedule_Type: {
                    required: "Please Select Schedule",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit_Id-error").removeClass('mt-2');
                $("#Business_Unit_Id-error").addClass('m-100 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#Inspection_Date-error").removeClass('mt-2');
                $("#Inspection_Date-error").addClass('mt-2 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('mt-2 text-danger');

                $("#Category_Id-error").removeClass('mt-2');
                $("#Category_Id-error").addClass('m-100 text-danger');

                $("#Supervisor_Id-error").removeClass('mt-2');
                $("#Supervisor_Id-error").addClass('m-100 text-danger');

                $("#Service_Provider_Id-error").removeClass('mt-2');
                $("#Service_Provider_Id-error").addClass('m-100 text-danger');

                $("#HSE_Representative_Id-error").removeClass('mt-2');
                $("#HSE_Representative_Id-error").addClass('m-100 text-danger');

                $("#Schedule_Type-error").removeClass('mt-2');
                $("#Schedule_Type-error").addClass('m-100 text-danger');
            },
            submitHandler: function () {
                var DocArr = [];
                var rowCount = $('#Doc_Table tbody > tr').length;
                if (rowCount != 0) {
                    $('#Doc_Table tbody > tr').each(function (c) {

                        var varReq_Document_Review_Id = $(this).closest('tr').find('.Req_Document_Review_Id').val();
                        var varReq_Document_Name = $(this).closest('tr').find('.Req_Document_Name').val();
                        var varReq_Document_Description = $(this).closest('tr').find('.Req_Document_Description').val();
                        var varUploadPhotos = $(this).closest('tr').find('.UploadPhotos').val();
                        var var_val_UploadPhotos = "";
                        if (varUploadPhotos != "" && varUploadPhotos != null) {
                            var val_UploadPhotos = varUploadPhotos.split(',');
                            var i;
                            for (i = 0; i < val_UploadPhotos.length; i++) {
                                var x = val_UploadPhotos[i].replace(/[\(\)\[\]{}'"]/g, "");
                                if (x != "/") {
                                    var_val_UploadPhotos = x;
                                }
                            }
                        }
                        var data = {};
                        data.Req_Document_Review_Id = varReq_Document_Review_Id;
                        data.Req_Document_Name = varReq_Document_Name;
                        data.Req_Document_Description = varReq_Document_Description;
                        data.File_Path = var_val_UploadPhotos;
                        DocArr.push(data);
                    });
                }
                var Obj = {
                    Insp_Request_Id: $("#Insp_Request_Id").val(),
                    //Business_Unit_Id: $("#Business_Unit_Id").val(),
                    //Zone_Id: $("#Zone_Id").val(),
                    //Community_Id: $("#Community_Id").val(),
                    //Building_Id: $("#Building_Id").val(),
                    Supervisor_Id: $("#Supervisor_Id").val(),
                    Service_Provider_Id: $("#Service_Provider_Id").val(),
                    HSE_Representative_Id: $("#HSE_Representative_Id").val(),
                    /*Schedule_Type: $("#Schedule_Type").val(),*/
                   /* Insp_Type_Id: "Inspection",*/
                   /* Inspection_Date: $("#Inspection_Date").val(),*/
                    //Walk_In_Insp_Name: $("#Walk_In_Insp_Name").val(),
                    Req_Description: $("#Req_Description").val(),
                    Category_Id: $("#Category_Id").val(),
                //    Insp_Req_Doc_Review_List: DocArr
                };
                $.ajax({
                    url: "/Inspection/Joint_Insp_Request_Add",
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


        $("#F_Add_Insp_Findings").validate({
            rules: {
                Location: {
                    required: true,
                },
                Main_SubCategory: {
                    required: true,
                },
            },
            messages: {
                Location: {
                    required: "Please Enter Location",
                },
                Main_SubCategory: {
                    required: "Please Select Sub Category",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('mt-2 text-danger');
                $("#Main_SubCategory-error").removeClass('mt-2');
                $("#Main_SubCategory-error").addClass('mt-2 text-danger');
            },
            submitHandler: function () {
                var lat = "0.00";
                var long = "0.00";
                if ($("#Lat").val() != "" || $("#Long").val() != "") {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                }
                if (Loc_Add == "" && $("#Location_Address").val() != "") {
                    Loc_Add = $("#Location_Address").val();
                }

                var rowCount = $('#tbl_Temp_Insp_Find tbody > tr').length;
                var SNo = rowCount + 1;
                var htmlActionmarkup = ""
                var VarMain_Observations = $(".Main_Observations").val();
                var VarLocation_Address = Loc_Add;
                var VarInsp_Sub_Finding_Id = $(".Insp_Sub_Finding_Id").val();
                var VarMain_HazardRisk = $(".Main_HazardRisk").val();
                var VarMain_Requirements = $(".Main_Requirements").val();
                var VarMain_ActionRequired = $(".Main_ActionRequired").val();
                var VarMain_Category = $(".Main_Category").val();
                var VarMain_SubCategory = $(".Main_SubCategory").val();
                var VarMain_RiskLevel = $(".Main_RiskLevel").val();
                var VarUploadPhotos = $(".UploadPhotos").val();
                if (VarUploadPhotos == "/") {
                    VarUploadPhotos = "";
                    $(".UploadPhotos").val("")
                }

                htmlActionmarkup = '<tr>'
                htmlActionmarkup += '<td><input class="Insp_Sub_Finding_Id" type="hidden" value="' + VarInsp_Sub_Finding_Id + '" id="Insp_Sub_Finding_Id' + rowCount + '" /><label>' + SNo + '</label> <input class="form-control Lat" type="hidden" value="' + lat + '" id="Lat' + rowCount + '"><input class="form-control Long" type="hidden" value="' + long + '" id="Long' + rowCount + '"></td>'
                htmlActionmarkup += '<td><input class="form-control UploadPhotos UploadPhotos' + rowCount + '" type="hidden" id="UploadPhotos' + rowCount + '" value=' + VarUploadPhotos + ' /><textarea style="width: 220px;" name="Main_Observations"  id="Main_Observations' + rowCount + '" class="form-control Main_Observations Main_Observations' + rowCount + '" readonly placeholder="Enter Observations" rows="1">' + VarMain_Observations + '</textarea></td>'
                htmlActionmarkup += '<td><textarea style="width: 220px;" readonly id="Main_HazardRisk' + rowCount + '" class="form-control Main_HazardRisk Main_HazardRisk' + rowCount + '" placeholder = "Enter Hazard & Risk" rows = "1" > ' + VarMain_HazardRisk + ' </textarea></td>'
                htmlActionmarkup += '<td><textarea style="width: 220px;" readonly id="Main_Requirements' + rowCount + '" class="form-control Main_Requirements Main_Requirements' + rowCount + '" placeholder = "Enter Requirements" rows = "1" > ' + VarMain_Requirements + ' </textarea></td>'
                htmlActionmarkup += '<td><textarea style="width: 220px;" readonly id="Main_ActionRequired' + rowCount + '" class="form-control Main_ActionRequired Main_ActionRequired' + rowCount + '" placeholder = "Enter Description of Action Required" rows = "1" > ' + VarMain_ActionRequired + ' </textarea><input style="width: 220px;" readonly type="hidden" class="form-control Main_Category Main_Category' + rowCount + '" name = "Main_Category" value = "' + VarMain_Category + '" id = "Main_Category' + rowCount + '" /><input type="hidden" style="width: 220px;" readonly class="form-control Main_SubCategory Main_SubCategory' + rowCount + '"  value="' + VarMain_SubCategory + '" id = "Main_SubCategory' + rowCount + '" /></td>'
                htmlActionmarkup += '<td><textarea style="width: 220px;"  rows = "1" readonly class="form-control Main_RiskLevel Main_RiskLevel' + rowCount + '" name = "Main_RiskLevel" value = "' + VarMain_RiskLevel + '" id = "Main_RiskLevel' + rowCount + '">' + VarMain_RiskLevel + ' </textarea></td>'
                htmlActionmarkup += '<td><textarea style="width: 220px;"  rows = "1" readonly class="form-control Location_Address Location_Address' + rowCount + '" name = "Location_Address" value = "' + VarLocation_Address + '" id = "Location_Address' + rowCount + '">' + VarLocation_Address + ' </textarea></td>'
                htmlActionmarkup += '<td><i class="mdi mdi-delete TblDeleteButton font-size-18" style="color:red;"></i></td></tr>';
                htmlActionmarkup += '</tr>';
                $("#tbl_Temp_Insp_Find").append(htmlActionmarkup);
                var cls_Category = "." + "Main_Category" + rowCount;
                var cls_RiskLevel = "." + "Main_RiskLevel" + rowCount;
                $(cls_Category).val(VarMain_Category);
                $(cls_RiskLevel).val(VarMain_RiskLevel);
                Loc_Add = "";
                $("#Main_Observations").val("");
                $("#Lat").val("");
                $("#Long").val("");
                $("#Location_Address").val("");
                $("#UploadPhotos").val("");
                $("#Main_HazardRisk").val("");
                $("#Main_Requirements").val("");
                $("#Main_ActionRequired").val("");
                $("#Main_Category").val("");
                $('#file').val('');
                $('#Main_SubCategory').empty();
                $("#search_input").val("");
                $("#search_input_edit").val("");
                $("#Main_RiskLevel").val("");
                $(".Div_Bind_Images").empty();
                $(".Image_Bind_Div_Show_Hide").hide();
                $(".tbl_Temp_Insp_Find_Hide_Show").show();
                $(".Fn_Temp_Final_Submit_Div").show();
                //$(".Add_Map_Location").show();
                $(".Edit_Map_Location").show();
            }
        });


    });
});