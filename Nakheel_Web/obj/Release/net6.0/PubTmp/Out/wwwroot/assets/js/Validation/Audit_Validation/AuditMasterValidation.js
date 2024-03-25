
$(function () {
    'use strict';
    $(function () {
        $.validator.addMethod("AlphaNum", function (value, element) {
            return this.optional(element) || /^[A-Za-z0-9 _.-]+$/.test(value);
        }, "Username must contain only letters or numbers.");
        //Web Menu Form

        $("#F_AddTopicCommunity").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                var form = $('#F_AddTopicCommunity');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Master_Community_List = [];
                var varAudit_Category_Id = $("#Audit_Category_Id").val()

                $('#tblAuditTopicsList tbody > tr.Modified').each(function () {
                    var varAudit_Topics_Id = $(this).closest('tr').find('.Audit_Topics_Id').val();
                    var varAudit_Topics_Name = $(this).closest('tr').find('.Audit_Topics_Name').val();
                    var data = {};
                    data.Audit_Category_Id = varAudit_Category_Id;
                    data.Audit_Topics_Id = varAudit_Topics_Id;
                    data.Audit_Topics_Name = varAudit_Topics_Name;
                    Master_Community_List.push(data);
                });
                var len = Master_Community_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Audit/AddAuditTopics",
                        dataType: 'json',
                        data: { model: Master_Community_List },
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
                    toastr["error"]("Audit Topics should not be empty!.");
                }
            }
        });

        $("#F_Add_Audit_Sub_Topic").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger;
                var form = $('#F_Add_Audit_Sub_Topic');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Audit_Sub_Topic_List = [];
                var varAudit_Category_Id = $("#Audit_Category_Id").val();


                $('#tblAuditTopicsList tbody > tr.Modified').each(function () {
                    var varAudit_Sub_Topics_Id = $(this).closest('tr').find('.Audit_Sub_Topics_Id').val();
                    var varAudit_Sub_Topics_Name = $(this).closest('tr').find('.Audit_Sub_Topics_Name').val();
                    var varRemarks = $('#AU_AuditTopics').val();
                    var data = {};
                    data.Audit_Category_Id = varAudit_Category_Id;
                    data.Audit_Sub_Topics_Id = varAudit_Sub_Topics_Id;
                    data.Audit_Sub_Topics_Name = varAudit_Sub_Topics_Name;
                    data.Remarks = varRemarks;
                    Audit_Sub_Topic_List.push(data);
                });
                var len = Audit_Sub_Topic_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Audit/Add_Audit_Sub_Topics",
                        dataType: 'json',
                        data: { model: Audit_Sub_Topic_List },
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

        $("#F_Add_Audit_Questionnaires").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger;
                var form = $('#F_Add_Audit_Questionnaires');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var Audit_Questionnaires_List = [];
                var varAudit_Category_Id = $("#AU_AuditTopics").val(); //topicsid
                var varAudit_Sub_Topics_Id = $("#AU_AuditSubTopics").val();

                if (varAudit_Sub_Topics_Id == "" || varAudit_Sub_Topics_Id == null) {
                    varAudit_Sub_Topics_Id = "0";
                }

                $('#tblAuditTopicsList tbody > tr.Modified').each(function () {
                    debugger;
                    var varAudit_Questionnaires_Id = $(this).closest('tr').find('.Audit_Sub_Topics_Id').val();
                    var varAudit_Questionnaires_Name = $(this).closest('tr').find('.Audit_Questionnaires_Name').val();
                    if (varAudit_Questionnaires_Id == "" || varAudit_Questionnaires_Id == null) {
                        varAudit_Questionnaires_Id = "0";
                    }
                    var data = {};
                    data.Audit_Category_Id = varAudit_Category_Id;    //Topics Id
                    data.Audit_Sub_Topics_Id = varAudit_Sub_Topics_Id;
                    data.Audit_Questionnaires_Id = varAudit_Questionnaires_Id;
                    data.Audit_Questionnaires_Name = varAudit_Questionnaires_Name;
                    Audit_Questionnaires_List.push(data);
                });
                var len = Audit_Questionnaires_List.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Audit/Add_Audit_Questionnaires",
                        dataType: 'json',
                        data: { model: Audit_Questionnaires_List },
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
                    toastr["error"]("Audit Questionnaires should not be empty!.");
                }
            }
        });

        $("#Add_Findings").validate({
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
            },
            highlight: function (element, errorClass) {
                $(element).parent().addClass('has-danger')
                $(element).addClass('form-control-danger')
            },
            submitHandler: function () {
                debugger;
                var form = $('#Add_Findings');
                var token = $('input[name="__RequestVerificationToken"]', form).val();
                var FindingsList = [];
                var varFindings_Type = $("#Findings_Type").val();

                $('#tblAuditFindingsList tbody > tr.Modified').each(function () {
                    /* var findingsName = $("input[name=Findings_Name]").attr('class');*/
                    var varFindings_Id = $(this).closest('tr').find('.Findings_Id').val();
                    var varFindings_Name = $(this).closest('tr').find(".Findings_Name").val();

                    var data = {};
                    data.Findings_Id = varFindings_Id;
                    data.Findings_Name = varFindings_Name;
                    data.Findings_Type = varFindings_Type;

                    FindingsList.push(data);
                });
                var len = FindingsList.length;
                if (len > 0) {
                    $.ajax({
                        type: 'POST',
                        url: "/Audit/AddAuditFindings",
                        dataType: 'json',
                        data: { model: FindingsList },
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
                    toastr["error"]("Audit Findings should not be empty!.");
                }
            }
        });

        $("#Audit_schd_Form").validate({
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
                Lead_Auditor_Id: {
                    required: true,
                },
                Audit_Emp_Id: {
                    required: true,
                },
                Service_Prov_Emp_Id: {
                    required: true,
                },
            },
            message: {
                Business_Unit_Id: {
                    required: "Please Select Business Unit",
                },
                Zone_Id: {
                    required: "Please Select Zone",
                },
                Community_Id: {
                    required: "Please Select Community"
                },
                Lead_Auditor_Id: {
                    required: "Please Select Lead Auditor",
                },
                Audit_Emp_Id: {
                    required: "Please Select Audit Team",
                },
                Service_Prov_Emp_Id: {
                    required: "Please Select Service Provider",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-2 text-danger');
                label.insertAfter(element);
                $("#Business_Unit_Id-error").removeClass('mt-2');
                $("#Business_Unit_Id-error").addClass('m-10 text-danger');

                $("#Zone_Id-error").removeClass('mt-2');
                $("#Zone_Id-error").addClass('m-10 text-danger');

                $("#Community_Id-error").removeClass('mt-2');
                $("#Community_Id-error").addClass('m-10 text-danger');

                $("#Lead_Auditor_Id-error").removeClass('mt-2');
                $("#Lead_Auditor_Id-error").addClass('m-10 text-danger');

                $("#Audit_Emp_Id-error").removeClass('mt-2');
                $("#Audit_Emp_Id-error").addClass('m-10 text-danger');

                $("#Service_Prov_Emp_Id-error").removeClass('mt-2');
                $("#Service_Prov_Emp_Id-error").addClass('m-10 text-danger');


            },
            submitHandler: function () {
                debugger;
                var varAuditTeamList = [];
                var varServiceProviderList = [];
                var subSno = 0;
                var ServSno = 0;
                var varDateTime = $("#DateTime").val().replace("T", " ");
                var varAudit_Sch_Id = $("#Audit_Sch_Id").val();
                var Business_Unit_Id = $("#Business_Unit_Id").val();
                var Zone_Id = $("#Zone_Id").val();
                var Community_Id = $("#Community_Id").val();
                var Lead_Auditor_Id = $("#Lead_Auditor_Id").val();
                var Business_Unit_Type = $("#Business_Unit_Type").val();
                var lat = $("#Lat").val();
                var long = $("#Long").val();

                if ($("#Lat").val() != "" || $("#Long").val() != "") {
                    lat = $("#Lat").val();
                    long = $("#Long").val();
                }

                $('#tbl_ServiceProv_Audit_Team tbody > tr').each(function () {
                    debugger;
                    var varAuditEmpId = $(this).closest('tr').find('.clsAudit_Emp_Id').val();
                    var data = {};
                    data.Audit_Emp_Id = varAuditEmpId;
                    varAuditTeamList.push(data);
                });
                $('#tbl_Service_Provider tbody > tr').each(function () {
                    var varAuditEmpId = $(this).closest('tr').find('.cls_Service_Prov_Emp_Id').val();
                    var data = {};
                    data.Service_Prov_Emp_Id = varAuditEmpId;
                    varServiceProviderList.push(data);

                });
                var Obj = {
                    Audit_Sch_Id: varAudit_Sch_Id,
                    Business_Unit_Id: Business_Unit_Id,
                    Zone_Id: Zone_Id,
                    Community_Id: Community_Id,
                    Lead_Auditor_Id: Lead_Auditor_Id,
                    DateTime: varDateTime,
                    Business_Unit_Type: Business_Unit_Type,
                    Audit_Lati: lat,
                    Audit_Long: long,
                    Audit_Team_List: varAuditTeamList,
                    Service_Prov_List: varServiceProviderList
                };
                $.ajax({
                    url: "/Audit/Add_AuditSchedule",
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

        $("#F_Inter_Audit_Schd").validate({
            rules: {
                Audit_Emp_Id: {
                    required: true,
                },
                Service_Prov_Emp_Id: {
                    required: true,
                },
            },
            message: {
                Int_Audit_Team: {
                    required: "Please Select Audit Team",
                },
                Int_Service_Provider_Rep: {
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


        $("#FN_NCRFormSubmit").validate({
            rules: {
                Non_Conformity: {
                    required: true,
                },
                Applicable_Standard: {
                    required: true,
                },
                Responsibility: {
                    required: true,
                },
                Completion_Date: {
                    required: true,
                },
                Target_Date: {
                    required: true,
                },
                Authorised_by: {
                    required: true,
                },
                Root_Cause_Analysis_Name: {
                    required: true,
                },
                Corrective_Action_Name: {
                    required: true,
                },
              
            },
            message: {
                Non_Conformity: {
                    required: "Please Enter Date time",
                },
                Applicable_Standard: {
                    required: "Please Select Business Unit",
                },
                Responsibility: {
                    required: "Please Select Business Unit",
                },
                Completion_Date: {
                    required: "Please Select Business Unit",
                },
                Target_Date: {
                    required: "Please Select Business Unit",
                },
                Authorised_by: {
                    required: "Please Select Business Unit",
                },
                Root_Cause_Analysis_Name: {
                    required: "Please Select Business Unit",
                },
                Corrective_Action_Name: {
                    required: "Please Select Business Unit",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-10 text-danger');
                label.insertAfter(element);
                $("#Applicable_Standard-error").removeClass('mt-2');
                $("#Applicable_Standard-error").addClass('m-10 text-danger');

                $("#Responsibility-error").removeClass('mt-2');
                $("#Responsibility-error").addClass('m-10 text-danger');

                $("#Completion_Date-error").removeClass('mt-2');
                $("#Completion_Date-error").addClass('m-10 text-danger');

                $("#Completion_Date-error").removeClass('mt-2');
                $("#Completion_Date-error").addClass('m-10 text-danger');

                $("#Target_Date-error").removeClass('mt-2');
                $("#Target_Date-error").addClass('m-10 text-danger');

                $("#Authorised_by-error").removeClass('mt-2');
                $("#Authorised_by-error").addClass('m-10 text-danger');

                $("#Root_Cause_Analysis_Name-error").removeClass('mt-2');
                $("#Root_Cause_Analysis_Name-error").addClass('m-10 text-danger');

                $("#Corrective_Action_Name-error").removeClass('mt-2');
                $("#Corrective_Action_Name-error").addClass('m-10 text-danger');
            },
            submitHandler: function () {
                debugger;
                var varRoot_Cause_AnalysisList = [];
                var varCorrective_ActionList = [];

                var varNCR_Report_Id = $("#NCR_Report_Id").val();
                var varQuestionnaires_Id = $("#Questionnaire_Id").val();
                var varInternal_Audit_Id = $("#Inv_Inc_Id").val();
                var varNon_Conformity = $("#Non_Conformity").val();
                var varApplicable_Standard = $("#Applicable_Standard").val();
                var varResponsibility = $("#Responsibility").val();
                var varCompletion_Date = $("#Completion_Date").val();
                var varTarget_Date = $("#Target_Date").val();
                var varAuthorised_by = $("#Authorised_by").val();
                var varClause_Id = $("#Clause_Id").val();

                //$('#tbl_Root_Cause_Analysis tbody > tr').each(function () {
                //    var varRoot_Cause_Analysis_Name = $(this).closest('tr').find('.Root_Cause_Analysis_Name').val();
                //    var varRoot_Cause_Analysis_Description = $(this).closest('tr').find('.Root_Cause_Analysis_Description').val();

                //    var data = {};
                //    data.Root_Cause_Analysis_Name = varRoot_Cause_Analysis_Name;
                //    data.Root_Cause_Analysis_Description = varRoot_Cause_Analysis_Description;
                //    varRoot_Cause_AnalysisList.push(data);
                //});
                //$('#tbl_Corrective_Action tbody > tr').each(function () {
                //    var varCorrective_Action_Name = $(this).closest('tr').find('.Corrective_Action_Name').val();
                //    var data1 = {}
                //    data1.Corrective_Action_Name = varCorrective_Action_Name;
                //    varCorrective_ActionList.push(data1);
                //});
                var Obj = {
                    NCR_Report_Id: varNCR_Report_Id,
                    Int_Audit_Id: varInternal_Audit_Id,
                    Questionnaires_Id: varQuestionnaires_Id,
                    Non_Conformity_Description: varNon_Conformity,
                    Applicable_Standard_Procedure: varApplicable_Standard,
                    Responsibility: varResponsibility,
                    Completion_Date: varCompletion_Date,
                    Target_Date: varTarget_Date,
                    Authorised_by: varAuthorised_by,
                    Remarks_1: varClause_Id,
                    L_NCR_Root_Cause_Analysis: varRoot_Cause_AnalysisList,
                    L_NCR_Corrective_Action: varCorrective_ActionList
                };
                $.ajax({
                    url: "/Audit/Add_NCR_Report_Form",
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
                                //window.location.reload();
                                $('.add-new').modal('hide');
                                $('input[type=text]').each(function () {
                                    $(this).val('');
                                });
                                $('input[type=date]').each(function () {
                                    $(this).val('');
                                });
                                $("#checkbox_multiple_" + varQuestionnaires_Id).show();
                                $("#NCRFormmodal_" + varQuestionnaires_Id).hide();
                                var tt = $("#hdnIsNCRComplete_Id").val();
                                $("#Is_NCR_Complete_" + tt).val('1');
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

        $("#FN_NCRFormSubmit_Report").validate({
            rules: {
                Root_Cause_Analysis_Name: {
                    required: true,
                },
                Corrective_Action_Name: {
                    required: true,
                },

            },
            message: {
                Root_Cause_Analysis_Name: {
                    required: "Please Select Business Unit",
                },
                Corrective_Action_Name: {
                    required: "Please Select Business Unit",
                },
            },
            errorPlacement: function (label, element) {
                label.addClass('mt-10 text-danger');
                label.insertAfter(element);
                $("#Root_Cause_Analysis_Name-error").removeClass('mt-2');
                $("#Root_Cause_Analysis_Name-error").addClass('m-10 text-danger');

                $("#Corrective_Action_Name-error").removeClass('mt-2');
                $("#Corrective_Action_Name-error").addClass('m-10 text-danger');
            },
            submitHandler: function () {
                debugger;
                var varRoot_Cause_AnalysisList = [];
                var varCorrective_ActionList = [];

                $('#tbl_Root_Cause_Analysis tbody > tr').each(function () {
                    var varRoot_Cause_Analysis_Name = $(this).closest('tr').find('.Root_Cause_Analysis_Name').val();
                    var varRoot_Cause_Analysis_Description = $(this).closest('tr').find('.Root_Cause_Analysis_Description').val();

                    var data = {};
                    data.Root_Cause_Analysis_Name = varRoot_Cause_Analysis_Name;
                    data.Root_Cause_Analysis_Description = varRoot_Cause_Analysis_Description;
                    varRoot_Cause_AnalysisList.push(data);
                });
                $('#tbl_Corrective_Action tbody > tr').each(function () {
                    var varCorrective_Action_Name = $(this).closest('tr').find('.Corrective_Action_Name').val();
                    var data1 = {}
                    data1.Corrective_Action_Name = varCorrective_Action_Name;
                    varCorrective_ActionList.push(data1);
                });
                var Obj = {
                    NCR_Report_Id: varNCR_Report_Id,
                    Int_Audit_Id: varInternal_Audit_Id,
                    Questionnaires_Id: varQuestionnaires_Id,
                    Non_Conformity_Description: varNon_Conformity,
                    Applicable_Standard_Procedure: varApplicable_Standard,
                    Responsibility: varResponsibility,
                    Completion_Date: varCompletion_Date,
                    Target_Date: varTarget_Date,
                    Authorised_by: varAuthorised_by,
                    Remarks_1: varClause_Id,
                    L_NCR_Root_Cause_Analysis: varRoot_Cause_AnalysisList,
                    L_NCR_Corrective_Action: varCorrective_ActionList
                };
                $.ajax({
                    url: "/Audit/Add_NCR_Report_Form",
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
                                //window.location.reload();
                                $('.add-new').modal('hide');
                                $('input[type=text]').each(function () {
                                    $(this).val('');
                                });
                                $('input[type=date]').each(function () {
                                    $(this).val('');
                                });
                                $("#checkbox_multiple_" + varQuestionnaires_Id).show();
                                $("#NCRFormmodal_" + varQuestionnaires_Id).hide();
                                var tt = $("#hdnIsNCRComplete_Id").val();
                                $("#Is_NCR_Complete_" + tt).val('1');
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