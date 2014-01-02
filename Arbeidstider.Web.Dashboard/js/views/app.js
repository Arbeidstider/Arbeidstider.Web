define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'views/base',
        'views/dashboard',
        'views/login',
        'text!templates/layout.html',
    ], function($, _, Backbone, Vm, BaseView, DashboardView, LoginView, LayoutTemplate) {
        var AppView = BaseView.extend({
            el: '#view-application',
            template: _.template(LayoutTemplate),
            initialize: function () {
                this.models = {};
                console.log("AppView initialize()");
                _.bindAll(this, "render", "signOut");
            },
            render: function (selectorAndView) {
                console.log("AppView.render()");
                // Set app template
                $(this.el).html(this.template());
                this.assignSelectorsToViewAndRender(selectorAndView);
                return this.el;
            },
            signOut: function() {
                this.models.login.signOut();
            }
        });

        return AppView;
    });