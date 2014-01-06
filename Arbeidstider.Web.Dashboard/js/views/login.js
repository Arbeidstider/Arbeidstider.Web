define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'models/session',
        'text!templates/login.html',
], function ($, _, Backbone, Marionette, Session, LoginTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: LoginTemplate,
        el: '#login',
        events: {
            "click .btn-block" : "signIn"
        },
        initialize: function (options) {
            _.bindAll(this, "signIn", "render", "validate");
        },
        signIn: function(e) {
            if (e) e.preventDefault();
            if (this.validate())
                Session.signIn();
        },
        render: function () {
            console.log("LoginView.render()");
            $(this.el).html(this.template());
            $("body").attr("class", "contrast-red sign-in contrast-background");
            return this.el;
        },
        signOut: function () {
            console.log('LoginView.signOut');
            Session.signOut();
        },
        validate: function () { return true; }
    });
});