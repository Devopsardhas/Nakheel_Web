$(function () {

    $.validator.addMethod("req", function (value, element) {
        return value !== "";
    }, "This field is required");

    $.validator.addMethod("AlphaNum", function (value, element) {
        return this.optional(element) || /^(?=.*\S)[a-zA-Z0-9-&/().,\s\r\n]*$/.test(value);
    }, "Must contain only letters or numbers.");

    $.validator.addMethod("AlphaNumaudit", function (value, element) {
        return this.optional(element) || /^(?=.*\S)[a-zA-Z0-9-&/().;:?"%&,\s\r\n]*$/.test(value);
    }, "Must contain only letters or numbers.");
    $.validator.addMethod("EmailFormat", function (value, element) {
        // Email validation regular expression
        var emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
        return this.optional(element) || emailRegex.test(value);
    }, "Invalid email format.");

    //$(document).ready(function () {
    //    $("#FrmSchTaskAssign").validate({
    //        errorPlacement: function (error, element) {
    //            // Customize the placement of error messages if needed
    //            // By default, error messages are placed after the invalid element
    //            error.insertAfter(element);
    //        },
    //        submitHandler: function (form) {
    //            alert("ok")
    //            // Your code for handling the form submission
    //            // This function will be called when the form is valid
    //        }
    //    });
    //});

    //$("#FrmSchTaskAssign").validate({
    //    rules: {
    //        'req': {
    //            required: true,
    //            // Add more rules as needed
    //        },
    //        // Add more fields and rules as needed
    //    },
    //    messages: {
    //        'req': {
    //            required: "Please enter a value for Field 1",
    //            // Add more error messages as needed
    //        },
    //        // Add more fields and error messages as needed
    //    },
    //    submitHandler: function () {
    //        debugger;
    //        alert("ok");
    //    }
    //});




    //$(UI_Fields.FRM_TASK_ASSIGN).validate({
    //    rules: {
    //        Company_Name: {
    //            required: true,
    //        },

    //    },
    //    messages: {
    //        Company_Name: {
    //            required: "Please Enter Company Name",
    //        },

    //    },
    //    errorPlacement: function (label, element) {
    //        label.addClass('mt-2 text-danger');
    //        label.insertAfter(element);
    //        $("#Business_Unit-error").removeClass('mt-2');
    //        $("#Business_Unit-error").addClass('m-100 text-danger');

    //    },
    //    highlight: function (element, errorClass) {
    //        $(element).parent().addClass('has-danger')
    //        $(element).addClass('form-control-danger')
    //    },
    //    submitHandler: function () {

    //    }

});