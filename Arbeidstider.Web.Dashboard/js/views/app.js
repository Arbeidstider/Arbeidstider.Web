define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'models/session',
        'views/base',
        'views/dashboard',
        'views/login',
        'text!templates/layout.html',
    ], function($, _, Backbone, Vm, Session, BaseView, DashboardView, LoginView,  LayoutTemplate) {
        var AppView = BaseView.extend({
            defaults: {
                models:  {}
            },
            el: '#view-application',
            template: _.template(LayoutTemplate),
            initialize: function () {
                console.log("AppView initialize()");
                _.bindAll(this, "isLoggedIn", "isAuthenticated", "render",  "signOut");
                this.models = this.models || {};
                this.models.session = new Session();
                console.log("session initialize");
                this.models.session.on('change:isAuthenticated', this.isAuthenticated);
            },
            isAuthenticated: function() {
                var session = this.models.session;
                
                var view;
                if (this.isLoggedIn()) view = Vm.reuseView('DashboardView', function() { return new DashboardView({ session: session }) });
                else view = Vm.reuseView('LoginView', function() { return new LoginView({ session: session }) });
                
                this.render(view,  true);
            },
            render: function (view, close) {
                console.log("AppView.render()");
                // Set app template
                $(this.el).html(this.template());
                
                var previous = this.currentView || null;
                if (previous && close) previous.close();

                if (!this.isLoggedIn()) this.assign({ "#view-login": view });
                else this.assign({ "#view-dashboard" : view });
                
                this.currentView = view;
                return this.el;
            },
            isLoggedIn: function () {
                return this.models.session.get("isAuthenticated") == true;
            },
            signOut: function() {
                this.models.session.signOut();
            }
        });

        return AppView;
    });