define([
        'jquery',
        'underscore',
        'marionette',
        'settings',
        'models/employee',
        'text!templates/register.html'
], function ($, _, Marionette, Settings, EmployeeModel, RegisterTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(RegisterTemplate),
        events: {
            "click .btn-block": "register"
        },
        initialize: function () {
            _.bindAll(this, "register", "registerSuccess", "registerError", "render");
        },
        registerError: function (a, b, c) {
            console.log("error");
            console.log(a);
            console.log(b);
            console.log(c);
        },
        registerSuccess: function (response) {
            var data = JSON.stringify(response);
            console.log("user created: " + data);
        },
        register: function (e) {
            console.log("register()");
            if (e) e.preventDefault();

            var data = 
            {
                FirstName: $("#Firstname").val(),
                LastName: $("#surname").val(),
                Email: $("#e-post").val(),
                BirthDate: $("#birthday").val(),
                Phonenumber: $("#mobnr").val(),
                Address: $("#address").val(),
                Zipcode: $("#zip-code").val(),
                City: $("#city").val(),
                Continue: "/dashboard",
            };
            
            console.log("data: ");
            console.log(data);
            //post("employee/register", data, successCb);
            $.ajax({
                type: "POST",
                dataType: "json",
                data: JSON.stringify(data),
                contentType: "application/json",
                url: Settings.ServiceUrl("addemployee"),
                error: function (jqXHR, textStatus, errorThrown) { console.log(errorThrown); },
                success: this.registerSuccess,
                beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
            });
        },
        /*
        render: function () {
            $(this.el).html(this.template());
            return this.el;
        }
        */
    });
});