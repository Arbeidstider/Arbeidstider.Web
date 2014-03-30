define([
        'jquery',
        'underscore',
        'marionette',
        'text!templates/register.html'
], function ($, _, Marionette, RegisterTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(RegisterTemplate),
        events: {
            "click .btn-block": "register"
        },
        initialize: function () {
            console.log("views/register initialize");
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
            var self = this;

            this.model.set(
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
            });

            this.model.save({}, {wait: true, error: self.registerError, success: self.registerSuccess, beforeSend: self.setRequestHeader()});
            
            //$.ajax({
            //    type: "POST",
            //    dataType: "json",
            //    data: JSON.stringify(data),
            //    contentType: "application/json",
            //    url: Settings.ServiceUrl("addemployee"),
            //    error: function (jqXHR, textStatus, errorThrown) { console.log(errorThrown); },
            //    success: this.registerSuccess,
            //    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
            //});
        },
        /*
        render: function () {
            $(this.el).html(this.template());
            return this.el;
        }
        */
    });
});