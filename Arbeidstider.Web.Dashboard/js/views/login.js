define([
        'jquery',
        'underscore',
        'backbone',
        'views/base',
        'text!templates/login.html'
], function ($, _, Backbone, BaseView, LoginTemplate) {
    var LoginView = BaseView.extend({
        template: _.template(LoginTemplate),
        el: '#view-login',
        events: {
            "click .btn-block" : "signIn"
        },
        initialize: function () {
            _.bindAll(this, 'signIn', 'render');
            //$(".btn-block").bind("click", this.signIn);
        },
        signIn: function () {
            if (this.model.validate())
                this.model.signIn();
        },
        render: function () {
            console.log("LoginView.render()");
            if (!(this.model.get("isAuthenticated") == true)) {
                console.log("not logged in");
                $(this.el).html(this.template());
                $("body").attr("class", "contrast-red sign-in contrast-background");
                return this.el;
            }
        },
        onClose: function () {
            this.model.unbind("change:isAuthenticated", this.render);
            this.model.unbind("signIn", "render", this);
        },
        signOut: function () {
            console.log('LoginView.signOut');
            this.model.signOut();
        },
    });

    return LoginView;
});