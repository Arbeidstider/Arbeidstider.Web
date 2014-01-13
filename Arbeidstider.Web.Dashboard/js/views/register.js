define([
        'jquery',
        'underscore',
        'marionette',
        'helpers/mixins',
        'models/settings',
        'models/employee',
        'text!templates/register.html'
], function ($, _, Marionette, mixins, Settings, EmployeeModel, RegisterTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(RegisterTemplate),
        events: {
            "click .btn-block": "register"
        },
        initialize: function () {
            this.model = new EmployeeModel();
            _.bindAll(this, "register", "registerSuccess", "render");
        },
        registerSuccess: function (response) {
            var data = JSON.stringify(response);
            console.log("user created: " + data);
            alert("user created: " + data);
        },
        register: function (e) {
            if (e) e.preventDefault();
            console.log(Settings.ServiceUrl("/employee/register"));

            this.model.save({
                Firstname: this.$("#Firstname"),
                Surname: this.$("#surname"),
                BirthDate: this.$("#birthdate"),
            }, { url: Settings.ServiceUrl("/employee/register"), type: "POST" });
        },
        /*
        render: function () {
            $(this.el).html(this.template());
            return this.el;
        }
        */
    });
});