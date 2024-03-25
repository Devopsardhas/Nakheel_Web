$(function () {
    'use strict';
    alert('test');
    $("#F_EMR_Alert").validate({  
        rules: {
            Zone_Id: {
                required: true,
            },
            Community_Id: {
                required: true,
            },
            Building_Id: {
                required: true,
            },
            Mitigation_Id: {
                required: true,
            },
        },
        messages: {
            Zone_Id: {
                required: "Please Select the Zone"
            },
            Community_Id: {
                required: "Please Select the Community"
            },
            Building_Id: {
                required: "Please Select the Building"
            },
            Mitigation_Id: {
                required: "Please Select the Mitigation",
            },
        },
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
            var Spot_Arr = [];
            var rowCount = $('#tbl_loc tbody > tr.SpotTr').length;
            if (rowCount > 0) {
                $("#tbl_loc tbody > tr.SpotTr").each(function () {
                    debugger;
                    var VarAddress = $(this).closest('tr').find('.Address').val();
                    var varExactLoc = $(this).closest('tr').find('.ExactAddress ').val();
                    var varLatitude = $(this).closest('tr').find('.lat ').val();
                    var varLongitude = $(this).closest('tr').find('.long ').val();
                    var varReason = $(this).closest('tr').find('.Reason_Txt ').val();
                    var varAssignee = $(this).closest('tr').find('.Ser_Pro_Id ').val();
                    if (varReason == null || varReason == "") {
                        toastr.error("Please enter the description!", "Error");                      
                    }

                    if (varAssignee == null || varAssignee == "") {
                        toastr.error("Please select the service provider!", "Error");
                    }

                    var data = {}
                    data.Hot_Spot_Id = "0";
                    data.EMR_Alert_ID = $("#EMRAlertID").val();
                    data.Created_By = $("#Created_By").val();
                    data.Address = VarAddress;
                    data.Exact_Loc = varExactLoc;
                    data.Reason = varReason;
                    data.Latitude = varLatitude;
                    data.Longitude = varLongitude;
                    data.Service_Provider = varAssignee;
                    Spot_Arr.push(data);
                });
                var obj = {
                    EMR_Alert_ID: $("#EMRAlertID").val(),
                    Zone_Id: $("#Zone_Id").val(),
                    Community_Id: $("#Community_Id").val(),
                    Sub_Building_Id: $("#Building_Id").val(),
                    Mitigation_Type: $("#Mitigation_Id").val(),
                    Created_By: $("#Created_By").val(),
                    _Spots: Spot_Arr

                }
                $.ajax({
                    url: "/EmergencyAlert/Add_Emr_Alert",
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
            else {
                toastr.error("Please add the spot!", "Error");

            }

        }
    })

    $("#F_Mitigation").validate({
        rules: {
            Area_Rain: {
                required: true,
            },
            Rain_Start: {
                required: true,
            },
            Rain_Finish: {
                required: true,
            },
            Response_Time: {
                required: true,
            },
            Duration_Rain: {
                required: true,
            },
            Pumps: {
                required: true,
            },
            Tankers: {
                required: true,
            },
            Trips: {
                required: true,
            },
            Clear_Area: {
                required: true,
            },
            Pumps_operated: {
                required: true,
            },
            Resource_SP: {
                required: true,
            },
            Resources_NCM: {
                required: true,
            },
            Cost_Mitigation: {
                required: true,
            },
            RainGauage: {
                required: true,
            },
            //files: {
            //    required: true,
            //},
        },
        messages: {
            Area_Rain: {
                required: "Please enter the Areas of Rain"
            },
            Rain_Start: {
                required: "Please select the time of rain started"
            },
            Rain_Finish: {
                required: "Please select the time of rain finished"
            },
            Response_Time: {
                required: "Please select the response time"
            },
            Duration_Rain: {
                required: "Please select the duration of rain"
            },
            Pumps: {
                required: "Please enter the no of pumps installed"
            },
            Tankers: {
                required: "Please select the no of tankers deployed"
            },
            Trips: {
                required: "Please select the no of trips"
            },
            Clear_Area: {
                required: "Please select the time of rain finished"
            },
            Pumps_operated: {
                required: "Please select the duration of Pumps operated"
            },
            Resource_SP: {
                required: "Please select the no of resource deployed"
            },
            Resources_NCM: {
                required: "Please select the no of resource deployed"
            },
            Cost_Mitigation: {
                required: "Please select the cost of Mitigation"
            },
            RainGauage: {
                required: "Please select the Rain Gauage"
            },
            //files: {
            //    required: "Please choose the file",
            //},
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        },
        submitHandler: function () {
            var obj = {
                Alert_Report_Id: "0",
                Trigger_ID: $("#V_Trigger_Id").val(),
                Area: $("#Area_Rain").val(),
                Start_Time: $("#Rain_Start").val(),
                End_Time: $("#Rain_Finish").val(),
                Resp_Time: $("#Response_Time").val(),
                Duration: $("#Duration_Rain").val(),
                Pumps_Deployed: $("#Pumps").val(),
                Pump_OP_Duration: $("#Pumps_operated").val(),
                Tankers_Deployed: $("#Tankers").val(),
                No_Trips: $("#Trips").val(),
                Clear_Time: $("#Clear_Area").val(),
                Deployed_SP: $("#Resource_SP").val(),
                Deployed_NCM: $("#Resources_NCM").val(),
                Mitigation_Cost: $("#Cost_Mitigation").val(),
                Gauge_Reading: $("#RainGauage").val(),
                Created_By: $("#M_Createdby").val(),
            }
            $.ajax({
                url: "/MigitationAction/Add_Mitigation_Report",
                type: "POST",
                cache: false,
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    debugger;
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
                    } else if (data == "202") {
                        debugger;
                        //alert("Please choose the file!");
                        //ToastrMessage("error", "Please enter the action taken!");
                        toastr.error("Please choose the file!", "Error");
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
    })

    $("#F_Trigger_Alert").validate({
        rules: {
            Zone_Id: {
                required: true,
            },
            Community_Id: {
                required: true,
            },
            MitigationId: {
                required: true,
            },
        },
        messages: {
            Zone_Id: {
                required: "Please select the zone"
            },
            Community_Id: {
                required: "Please select the community"
            },
            MitigationId: {
                required: "Please select the mitigation"
            },
        },
        errorPlacement: function (label, element) {
            label.addClass('mt-2 text-danger');
            label.insertAfter(element);
        },
        highlight: function (element, errorClass) {
            $(element).parent().addClass('has-danger')
            $(element).addClass('form-control-danger')
        },
        submitHandler: function () {
            var obj = {
                Trigger_ID: $("#TriggerID").val(),
                Zone_Id: $("#Zone_Id").val(),
                Community_Id: $("#Community_Id").val(),
                Mitigation_Id: $("#MitigationId").val(),
                Created_By: $("#Createdby").val(),
            }
            $.ajax({
                url: "/TriggerAlert/Add_Trigger_Alert",
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
                            title: 'Hot-Spot is not available for the selected community',
                            showConfirmButton: false,
                            timer: 1500
                        });
                    }
                },
            })
        }
    })


});
