define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'models/session',
        'views/base',
        'views/dashboard',
        'views/login',
        'events',
        'text!templates/layout.html',
    ], function($, _, Backbone, Vm, Session, BaseView, DashboardView, LoginView, Events, LayoutTemplate) {
        var AppView = BaseView.extend({
            el: '#view-application',
            template: _.template(LayoutTemplate),
            router: {},
            initialize: function (options) {
                console.log("AppView initialize()");
                _.bindAll(this, "isLoggedIn", "isAuthenticated", "render", "signOut");
                this.models = {};
                this.models.session = options.session;
                this.models.session.bind('change:isAuthenticated', this.isAuthenticated);
            },
            isAuthenticated: function (e) {
                // Render dashboard on authenticated 
                var session = this.models.session;
                if (!this.models.session.get("isAuthenticated")) return;
                var view = Vm.reuseView('DashboardView', function() { return new DashboardView({ session: session }); });
                this.render({ "#view-dashboard": view}, true);
            },
            render: function (selectorAndView, close) {
                console.log("AppView.render()");
                // Set app template
                $(this.el).html(this.template());
                
                var previous = this.currentView || null;
                if (previous && close) {
                    console.log("previous: " + previous);
                     previous.close();
                }
                
                this.assign(selectorAndView);
                
                this.currentView = selectorAndView.view;
                return this.el;
            },
            isLoggedIn: function () {
                var session = truStorage.getItem("AuthSession");
                return (!session.SessionId && !session.UserName);
            },
            signOut: function() {
                this.models.session.signOut();
            }
        });

        return AppView;
    });