$(document).ready(function () {
    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    var Gr_current_fs, Gr_next_fs, Gr_previous_fs;
    var Gr_opacity;

    $(".next").click(function () {
        debugger;
        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        var cat_id = $("#txt_Inc_Category_Id").val();
        var inj_illn = $(".clsInjury_Illness").val();

        if (cat_id == "Environment") {
            /*alert(cat_id);*/
            $("#Is_Environmental_Impact").show();
        }
        if (cat_id == "Security") {
          /*  alert(cat_id);*/
            $("#Is_Security_Impact").show();
        }

        var incat_id = $("#Inc_Category_Id option:selected").text();
        var Inc_injval = $('input[name="Injuredillness"]:checked').val();

        if (incat_id == "Environment") {
            /*alert(cat_id);*/
            $("#Is_Environmental_Impact").show();
            $("#divbodyLocation").hide();
        }
        if (incat_id == "Security") {
            /*  alert(cat_id);*/
            $("#Is_Security_Impact").show();
            $("#divbodyLocation").hide();
        }

        if (Inc_injval == "No") {
            if (incat_id == "Environment") {
                $("#NO_Injury_Type").hide(100);
                $("#Is_Environmental_Impact").show();
                $("#divbodyLocation").hide();
            }
            else if (incat_id == "Security") {
                $("#NO_Injury_Type").hide(100);
                $("#Is_Security_Impact").show();
                $("#divbodyLocation").hide();
            }
            else {
                $("#NO_Injury_Type").show(100);
                $("#Is_Environmental_Impact").hide();
                $("#Is_Security_Impact").hide();
                $("#divbodyLocation").hide();
            }
        } 

        var injval = $('input[name="Injuredillness"]:checked').val();
        if (injval == "No") {
            $(UI_Fields2.BODY_LOCATION).hide(100);
            $("#NO_Injury_Type").show(100);
        }
        else if (injval == "Yes") {
            $("#NO_Injury_Type").hide(100);
            $(UI_Fields2.BODY_LOCATION).show(100);
        }


        var injval_1 = $('input[name="Injuredillness"]:checked').val();
        if (injval_1 == "No") {
            $(UI_Fields2.BODY_LOCATION).hide(100);
            $("#NO_Injury_Type").show(100);
        }
        else if (injval_1 == "Yes") {
            $("#NO_Injury_Type").hide(100);
            $(UI_Fields2.BODY_LOCATION).show(100);
        }
        var wouldulikemoreinfo = $("#Add_Description_3").val();
        if (wouldulikemoreinfo == "No") {
            var Unsafe_Act = [];
            $('.clsUnsafeAct:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#UnsafeActname_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsUnsafeCond:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#UNSCID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsPersonalFactor:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#PF_ID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsSystemFactor:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#SF_ID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).html('');
            $.each(Unsafe_Act, function (key, value) {
                var hdn_filename = "hdncls_UploadEvidence_Documents_File_" + key;
                var html = "";
                html += '<tr><td><input type="hidden" class="BuildingID" value="0"/><input type="text" class="form-control clsDesCorrectiveAction" name="Building_Name" value="' + value.M_Unsafe_Act_Id + '"/></td>';
                html += '<td><textarea class="form-control clsAction_Taken" id="" spellcheck="false"></textarea></td>';
                html += '<td><select class="form-control clsRiskMatrix" name="choices-single-default" placeholder="This is a search placeholder"> <option selected>--Select--</option><option value="High">High</option><option value="Low">Low</option><option value="Medium">Medium</option></select></td>';
                html += '<td><input type="date" class="form-control clsCorrectiveActionDate" name=Building_Description/></td>';
                html += '<td><input type="hidden" id="hdnService_Appendix_A" class="' + hdn_filename + ' Corrective_Actions_APPENDIX_A_Viewhide" /><input type="file" class="form-control" id="Invs_File_Path" name="Invs_File_Path" onchange="Applicalbe_Documents_File(this,' + "'" + hdn_filename + "'" + ')" accept=".jpg,.jpeg,.png,.doc,.docx,.pdf" /></td>';
                html += '<td><button type="button" class="btn btn-danger TblCorrActionDeleteButton">Remove</button></td></tr>';
                $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).append(html);
            });
            Unsafe_Act = [];
        }

        if (wouldulikemoreinfo == undefined) {
            var Unsafe_Act = [];
            $('.clsUnsafeAct:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#UnsafeActname_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsUnsafeCond:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#UNSCID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsPersonalFactor:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#PF_ID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $('.clsSystemFactor:checked').each(function () {
                var varM_Unsafe_Act_Id = $(this).val();
                var varcheckname = $("#SF_ID_" + varM_Unsafe_Act_Id).text();

                var data = {};
                data.M_Unsafe_Act_Id = varcheckname;
                Unsafe_Act.push(data);
            });
            $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).html('');
            $.each(Unsafe_Act, function (key, value) {
                var hdn_filename = "hdncls_UploadEvidence_Documents_File_" + key;
                var html = "";
                html += '<tr><td><input type="hidden" class="BuildingID" value="0"/><input type="text" class="form-control clsDesCorrectiveAction" name="Building_Name" value="' + value.M_Unsafe_Act_Id + '"/></td>';
                html += '<td><textarea class="form-control clsAction_Taken" id="" spellcheck="false"></textarea></td>';
                html += '<td><select class="form-control clsRiskMatrix" name="choices-single-default" placeholder="This is a search placeholder"> <option selected>--Select--</option><option value="High">High</option><option value="Low">Low</option><option value="Medium">Medium</option></select></td>';
                html += '<td><input type="date" class="form-control clsCorrectiveActionDate" name=Building_Description/></td>';
                html += '<td><input type="hidden" id="hdnService_Appendix_A" class="' + hdn_filename +' Corrective_Actions_APPENDIX_A_Viewhide" /><input type="file" class="form-control" id="Invs_File_Path" name="Invs_File_Path" onchange="Applicalbe_Documents_File(this,' + "'" + hdn_filename + "'" + ')" accept=".jpg,.jpeg,.png,.doc,.docx,.pdf" /></td>';
                html += '<td><button type="button" class="btn btn-danger TblCorrActionDeleteButton">Remove</button></td></tr>';
                $(UI_Fields2.ADDMORE_CORRECTIVE_ACTION).append(html);
            });
            Unsafe_Act = [];
        }

        $("#msform").validate({
            rules: {
                IncidentReferenceStatus: {
                    required: true,
                },
                Total_Man_Days: {
                    required: true,
                },
                Involved_Department_Contractor: {
                    required: true,
                },
                ApplicableReports: {
                    required: true,
                },
                Justification: {
                    required: true,
                },
                M_UnsafeActList: {
                    required: true,
                    minlength: 1
                },
                M_UnsafeConditionList: {
                    required: true,
                    minlength: 1
                },
                PersonalFactorList: {
                    required: true,
                    minlength: 1
                },
                SystemFactorList: {
                    required: true,
                    minlength: 1
                },
                MechanismInjuryList: {
                    required: true,
                    minlength: 1
                },
                AgencySourceInjuryList: {
                    required: true,
                    minlength: 1
                },
                UnsafeActRemarks: {
                    required: true
                },
                UnsafeCondRemarks: {
                    required: true
                },
                PFRemarks: {
                    required: true
                },
                SFRemarks: {
                    required: true
                },
                MechanismRemarks: {
                    required: true
                },
                AgencyRemarks: {
                    required: true
                },
                Justification: {
                    required: true
                },
                Injury_Type_list: {
                    required: true,
                    minlength: 1
                },
                Injury_Illness_list: {
                    required: true,
                    minlength: 1
                },
                Injuredillness: {
                    required: true,
                },
                Total_man_days_Req: {
                    required: true,
                },
                txtPoliceReport1: {
                    required: true,
                },
                policeReport: {
                    required: true,
                },
                txtMedicalReport1: {
                    required: true,
                },
                txtTechnicalReport1: {
                    required: true,
                },
                txtDCDReport1: {
                    required: true,
                },
                txtWitness1: {
                    required: true,
                },
                txtIP1: {
                    required: true,
                },
                txtOthers1: {
                    required: true,
                },
                MedicalReport: {
                    required: true,
                },
                TechnicalReport: {
                    required: true,
                },
                DCDReport: {
                    required: true,
                },
                WitnessReport: {
                    required: true,
                },
                IPReport: {
                    required: true,
                },
                OthersReport: {
                    required: true,
                },

                Company_Name: {
                    required: true,
                },
                Contact_Name: {
                    required: true,
                },
                Mobile_Number: {
                    required: true,
                    minlength: 10
                },
                Sel_Incident_Cat: {
                    required: true,
                },
                Injury_Type_list: {
                    required: true,
                    minlength: 1
                },
                Injury_Illness_list: {
                    required: true,
                    minlength: 1
                },
                Inc_Date: {
                    required: true,
                },
                Zone: {
                    required: true,
                },
                Community: {
                    required: true,
                },
                Inc_Time: {
                    required: true,
                },
                Building: {
                    required: true,
                },
                Business_Unit: {
                    required: true,
                },
                Injuredillness: {
                    required: true,
                    minlength: 1
                },
                InjuredType: {
                    required: true,
                    minlength: 1
                },
                Inc_Type_Id: {
                    required: true,
                },
                Vechicle_Accident_List: {
                    required: true,
                },
                Master_Community_Id: {
                    required: true,
                },

                Injury_TypeRemarks: {
                    required: true,
                },
                Injury_IllnesseRemarks: {
                    required: true,
                },
                VehicleAccidentRemarks: {
                    required: true,
                },
                Injuredillness_yes: {
                    required: true,
                },
                PropertyDamaged: {
                    required: true,
                },
                Insuranceclaim: {
                    required: true,
                },
                Inc_Fire_Location: {
                    required: true,
                },
                Inc_Business_Unit_Type: {
                    required: true,
                },
                Immediate_Actions: {
                    required: true,
                },
                Followrequired: {
                    required: true,
                },
                Required_Actions: {
                    required: true,
                },
                Location: {
                    required: true,
                },
                ApplicableReports: {
                    required: true,
                },
                Justification: {
                    required: true,
                },
                M_UnsafeActList: {
                    required: true,
                    minlength: 1
                },
                M_UnsafeConditionList: {
                    required: true,
                    minlength: 1
                },
                PersonalFactorList: {
                    required: true,
                    minlength: 1
                },
                SystemFactorList: {
                    required: true,
                    minlength: 1
                },
                MechanismInjuryList: {
                    required: true,
                    minlength: 1
                },
                AgencySourceInjuryList: {
                    required: true,
                    minlength: 1
                },
            },
            messages: {
                IncidentReferenceStatus: {
                    required: "Please Select Interim or Final",
                },
                Total_Man_Days: {
                    required: "Please enter total man days name",
                },
                Involved_Department_Contractor: {
                    required: "Please Select Department/Contractor",
                },
                ApplicableReports: {
                    required: "Please Select Applicable Reports",
                },
                Justification: {
                    required: "Please Enter Justification / Comments ",
                },
                M_UnsafeActList: {
                    required: "Please Select Unsafe Act",
                },
                M_UnsafeConditionList: {
                    required: "Please Select Unsafe Condition",
                },
                PersonalFactorList: {
                    required: "Please Select Personal Factor",
                },
                SystemFactorList: {
                    required: "Please Select System Factor",
                },
                MechanismInjuryList: {
                    required: "Please Select Mechanism Injury",
                },
                AgencySourceInjuryList: {
                    required: "Please Select Agency / Source Of Injury / Illness",
                },
                UnsafeActRemarks: {
                    required: "Please Enter Other Name",
                },
                UnsafeCondRemarks: {
                    required: "Please Enter Other Name",
                },
                PFRemarks: {
                    required: "Please Enter Other Name",
                },
                SFRemarks: {
                    required: "Please Enter Other Name",
                },
                MechanismRemarks: {
                    required: "Please Enter Other Name",
                },
                AgencyRemarks: {
                    required: "Please Enter Other Name",
                },
                Justification: {
                    required: "Please Enter Justification / Comments",
                },
                Injury_Type_list: {
                    required: "Please Select Injury Type",
                },
                Injury_Illness_list: {
                    required: "Please Select Nature of Injury /Illness",
                },
                Injuredillness: {
                    required: "Please Select Is There Any Injury/Illness Additional",
                },
                Total_man_days_Req: {
                    required: "Please Enter Total Man Days",
                },
                txtPoliceReport1: {
                    required: "Please Enter Police Justification / Comments",
                },
                policeReport: {
                    required: "Please Select Applicable Reports",
                },
                txtMedicalReport1: {
                    required: "Please Enter Medical Justification / Comments",
                },
                txtTechnicalReport1: {
                    required: "Please Enter Technical Justification / Comments",
                },
                txtDCDReport1: {
                    required: "Please Enter DCD Justification / Comments",
                },
                txtWitness1: {
                    required: "Please Enter Witness Justification / Comments",
                },
                txtIP1: {
                    required: "Please Enter IP Justification / Comments",
                },
                txtOthers1: {
                    required: "Please Enter Others Justification / Comments",
                },
                MedicalReport: {
                    required: "Please Select Applicable Reports",
                },
                TechnicalReport: {
                    required: "Please Select Applicable Reports",
                },
                DCDReport: {
                    required: "Please Select Applicable Reports",
                },
                WitnessReport: {
                    required: "Please Select Applicable Reports",
                },
                IPReport: {
                    required: "Please Select Applicable Reports",
                },
                OthersReport: {
                    required: "Please Select Applicable Reports",
                },

                Company_Name: {
                    required: "Please Enter Company Name",
                },
                Contact_Name: {
                    required: "Please Enter Contact Name",
                    minlength: "Name must consist of at least 5 characters"
                },
                Mobile_Number: {
                    required: "Please Enter Mobile Number",
                    minlength: "Name must consist of at least 10 characters"
                },
                Sel_Incident_Cat: {
                    required: "Please Select Incident Category",
                },
                Injury_Type_list: {
                    required: "Please Select Injury Type",
                },
                Injury_Illness_list: {
                    required: "Please Select Injury Illness",
                },
                Inc_Date: {
                    required: "Please Select Date",
                },
                Zone: {
                    required: "Please Select Zone",
                },
                Community: {
                    required: "Please Select Community",
                },
                Inc_Time: {
                    required: "Please Select Time",
                },
                Building: {
                    required: "Please Select Building",
                },
                Business_Unit: {
                    required: "Please Select Business Unit",
                },
                Injuredillness: {
                    required: "Please Select Injured illness",
                    minlength: 1
                },
                InjuredType: {
                    required: "Please Select Injured Type",
                    minlength: 1
                },
                Inc_Type_Id: {
                    required: "Please Select Incident Type",
                },
                Vechicle_Accident_List: {
                    required: "Please Select Vechicle Accident Type",
                },
                Master_Community_Id: {
                    required: "Please Select Master Community",
                },

                Injury_TypeRemarks: {
                    required: "Please Enter other name",
                },
                Injury_IllnesseRemarks: {
                    required: "Please Enter other name",
                },
                VehicleAccidentRemarks: {
                    required: "Please Enter other name",
                },
                Injuredillness_yes: {
                    required: "Please Select provide more Information",
                },
                PropertyDamaged: {
                    required: "Please Select  Nahkeel Property Damaged",
                },
                Insuranceclaim: {
                    required: "Please Select insurance claim",
                },
                Inc_Fire_Location: {
                    required: "Please Select Fire Location",
                },
                Inc_Business_Unit_Type: {
                    required: "Please Select Business Unit Type",
                },
                Immediate_Actions: {
                    required: "Please Enter Immediate Actions Taken",
                },
                Followrequired: {
                    required: "Please Select Follow Up Required",
                },
                Required_Actions: {
                    required: "Please Enter Follow Up Action",
                },
                Location: {
                    required: "Please Enter Location",
                },
                ApplicableReports: {
                    required: "Please Select Applicable Reports",
                },
                Justification: {
                    required: "Please Enter Justification / Comments ",
                },
                M_UnsafeActList: {
                    required: "Please Select Unsafe Act",
                },
                M_UnsafeConditionList: {
                    required: "Please Select Unsafe Condition",
                },
                PersonalFactorList: {
                    required: "Please Select Personal Factor",
                },
                SystemFactorList: {
                    required: "Please Select System Factor",
                },
                MechanismInjuryList: {
                    required: "Please Select Mechanism Injury",
                },
                AgencySourceInjuryList: {
                    required: "Please Select Agency / Source Of Injury / Illness",
                },
                UnsafeActRemarks: {
                    required: "Please Enter Other Name",
                },
                UnsafeCondRemarks: {
                    required: "Please Enter Other Name",
                },
                PFRemarks: {
                    required: "Please Enter Other Name",
                },
                SFRemarks: {
                    required: "Please Enter Other Name",
                },
                MechanismRemarks: {
                    required: "Please Enter Other Name",
                },
                AgencyRemarks: {
                    required: "Please Enter Other Name",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#M_UnsafeActList-error").removeClass('mt-2');
                $("#M_UnsafeActList-error").addClass('m-10 text-danger');

                $("#M_UnsafeConditionList-error").removeClass('mt-2');
                $("#M_UnsafeConditionList-error").addClass('m-10 text-danger');

                $("#PersonalFactorList-error").removeClass('mt-2');
                $("#PersonalFactorList-error").addClass('m-10 text-danger');

                $("#SystemFactorList-error").removeClass('mt-2');
                $("#SystemFactorList-error").addClass('m-10 text-danger');

                $("#MechanismInjuryList-error").removeClass('mt-2');
                $("#MechanismInjuryList-error").addClass('m-10 text-danger');

                $("#AgencySourceInjuryList-error").removeClass('mt-2');
                $("#AgencySourceInjuryList-error").addClass('m-10 text-danger');

                $("#txtunsafeactothers-error").removeClass('mt-2');
                $("#txtunsafeactothers-error").addClass('m-11 text-danger');

                $("#txtunsafeactoCondthers-error").removeClass('mt-2');
                $("#txtunsafeactoCondthers-error").addClass('m-11 text-danger');

                $("#txtPFothers-error").removeClass('mt-2');
                $("#txtPFothers-error").addClass('m-11 text-danger');

                $("#txtSFothers-error").removeClass('mt-2');
                $("#txtSFothers-error").addClass('m-11 text-danger');

                $("#txtMechanismothers-error").removeClass('mt-2');
                $("#txtMechanismothers-error").addClass('m-11 text-danger');

                $("#txtAgencyothers-error").removeClass('mt-2');
                $("#txtAgencyothers-error").addClass('m-11 text-danger');

                $("#Injury_Type_list-error").removeClass('mt-2');
                $("#Injury_Type_list-error").addClass('m-10 text-danger');

                $("#Injury_Illness_list-error").removeClass('mt-2');
                $("#Injury_Illness_list-error").addClass('m-10 text-danger');

                $("#Injuredillness-error").removeClass('mt-2');
                $("#Injuredillness-error").addClass('m-112 text-danger');

                $("#Total_man_days-error").removeClass('mt-2');
                $("#Total_man_days-error").addClass('m-11 text-danger');

                $("#Injury_Illness_list-error").removeClass('mt-2');
                $("#Injury_Illness_list-error").addClass('m-10 text-danger');


                $("#Injury_Type_list-error").removeClass('mt-2');
                $("#Injury_Type_list-error").addClass('m-10 text-danger');

                $("#Vechicle_Accident_List-error").removeClass('mt-2');
                $("#Vechicle_Accident_List-error").addClass('m-10 text-danger');

                $("#Inc_Category_Id-error").removeClass('mt-2');
                $("#Inc_Category_Id-error").addClass('m-100 text-danger');

                $("#Inc_Type_Id-error").removeClass('mt-2');
                $("#Inc_Type_Id-error").addClass('m-100 text-danger');


                $("#Business_Unit-error").removeClass('mt-2');
                $("#Business_Unit-error").addClass('m-100 text-danger');

                $("#Zone-error").removeClass('mt-2');
                $("#Zone-error").addClass('m-100 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-100 text-danger');

                $("#Building_Id-error").removeClass('mt-2');
                $("#Building_Id-error").addClass('m-100 text-danger');

                $("#Master_Community_Id-error").removeClass('mt-2');
                $("#Master_Community_Id-error").addClass('m-100 text-danger');

                $("#search_input-error").removeClass('mt-2');
                $("#search_input-error").addClass('m-101 text-danger');
            },
            submitHandler: function () {
                debugger;
           

                //Add Class Active
                $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");
                //show the next fieldset
                next_fs.show();
                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now) {
                        // for making fielset appear animation
                        opacity = 1 - now;

                        current_fs.css({
                            'display': 'none',
                            'position': 'relative'
                        });
                        next_fs.css({ 'opacity': opacity });
                    },
                    duration: 600
                });
            }
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $('.radio-group .radio').click(function () {
        $(this).parent().find('.radio').removeClass('selected');
        $(this).addClass('selected');
    });

    $(".submit").click(function () {
        return false;
    })

    //Add More
    $(".Add_next").click(function () {
        debugger;
        Gr_current_fs = $(this).parent();
        Gr_next_fs = $(this).parent().next();

        $("#addform").validate({

            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            submitHandler: function () {
                //Add Class Active
                $("#progressbar li").eq($("fieldset").index(Gr_next_fs)).addClass("active");
                //show the next fieldset
                Gr_next_fs.show();
                //hide the current fieldset with style
                Gr_current_fs.animate({ Gr_opacity: 0 }, {
                    step: function (now) {
                        // for making fielset appear animation
                        Gr_opacity = 1 - now;

                        Gr_current_fs.css({
                            'display': 'none',
                            'position': 'relative'
                        });
                        Gr_next_fs.css({ 'opacity': Gr_opacity });
                    },
                    duration: 600
                });
            }
        });
    });
});