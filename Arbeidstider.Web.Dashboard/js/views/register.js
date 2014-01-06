define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'helpers/mixins',
        'models/settings',
        'text!templates/register.html'
], function ($, _, Backbone, Marionette, mixins, Settings, RegisterTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(RegisterTemplate),
        //el: "#content",
        events: {
            "click .btn-block": "register"
        },
        initialize: function () {
            _.bindAll(this, "register", "registerSuccess", "render");
        },
        registerSuccess: function (response) {
            var data = JSON.stringify(response);
            console.log("user created: " + data);
            alert("user created: " + data);
        },
        register: function(e) {
            if (e) e.preventDefault();
            var data = _.formData("form");
            _.post({
                url: Settings.ServiceUrl("/employee/register"),
                data: data,
                success: this.registerSuccess,
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