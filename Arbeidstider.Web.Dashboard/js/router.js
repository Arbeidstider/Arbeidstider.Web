define([
    'jquery',
    'underscore',
    'backbone',
    'vm',
], function($, _, Backbone, Vm) {
    var AppRouter = Backbone.Router.extend({
        routes: {
            'login': 'login',
            'logout': 'logout',
            // Default - catch all
            '*view': 'home'
        }
    });

    var initialize = function (options) {
        console.log('router initialize');
        var appView = options.appView;
        var router = new AppRouter(options);

        /* Login */
        router.on('route:login', function () {
            console.log('login route');
            require(['views/login'], function (LoginView) {
                var view = Vm.reuseView('LoginView', function() { return new LoginView({ model: appView.models.session }); });
                appView.render({ "#view-login": view }, true);
            });
        });
        /* Logout */
        router.on('route:logout', function() {
            console.log('logout route');
            appView.signOut();
        });
        /* Default */
        router.on('route:home', function ()  {
            console.log("defualt route");
            if (!appView.models.isLoggedIn) {
                require(['views/login'], function (LoginView) {
                    var view = Vm.reuseView('LoginView', function() { return new LoginView({ model: appView.models.session }); });
                    appView.render({ "#view-login": view }, true);
                });
            }
            require(['views/dashboard'], function(DashboardView) {
                var view = Vm.reuseView('DashboardView', function() { return new DashboardView({ session: appView.models.session }); });
                appView.render({ "#view-dashboard": view }, true);
            });
        });

        Backbone.history.start();
    };  
    return {
        AppRouter: AppRouter,
        initialize: initialize
    };
});