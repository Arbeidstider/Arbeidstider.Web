define([
        'jquery',
        'underscore',
        'backbone',
        'views/base',
        'text!templates/login.html',
], function ($, _, Backbone, BaseView, LoginTemplate) {
    var LoginView = BaseView.extend({
        template: _.template(LoginTemplate),
        el: '#view-login',
        events: {
            "click .btn-block" : "signIn"
        },
        initialize: function () {
            _.bindAll(this, 'signIn', 'render');
            $("form").submit(this.signIn);
        },
        signIn: function(e) {
            if (e) e.preventDefault();
            if (this.model.validate())
                this.model.signIn("form");
        },
        render: function () {
            console.log("LoginView.render()");
            $(this.el).html(this.template());
            $("body").attr("class", "contrast-red sign-in contrast-background");
            return this.el;
        },
        signOut: function () {
            console.log('LoginView.signOut');
            this.model.signOut();
        },
    });

    return LoginView;
});