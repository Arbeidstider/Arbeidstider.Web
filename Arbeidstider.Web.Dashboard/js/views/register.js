define([
        'jquery',
        'underscore',
        'marionette',
        'helpers/mixins',
        'settings',
        'models/employee',
        'text!templates/register.html'
], function ($, _, Marionette, mixins, Settings, EmployeeModel, RegisterTemplate) {
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
            if (e) e.preventDefault();
            
            var model = new EmployeeModel({
                FirstName: $("#Firstname").val(),
                LastName: $("#surname").val(),
                Email: $("#e-post").val(),
                BirthDate: $("#birthday").val(),
            });
            model.save({}, { success: this.registerSuccess, error: this.registerError, url: Settings.ServiceUrl("/employee/register"), type: "POST" });
        },
        /*
        render: function () {
            $(this.el).html(this.template());
            return this.el;
        }
        */
    });
});