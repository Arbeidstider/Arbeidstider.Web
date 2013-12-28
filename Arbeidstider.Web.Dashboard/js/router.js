define([
    'jquery',
    'underscore',
    'backbone',
    'vm'
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
                var session = appView.models.session;
                var view = Vm.reuseView('LoginView', function() { return new LoginView({ model: session }); });
                appView.render(view, true);
            });
        });
        /* Logout */
        router.on('route:logout', function() {
            console.log('logout route');
            appView.signOut();
        });
        /* Default */
        router.on('route:home', function () {
            console.log('home route');
            if (!appView.isLoggedIn()) {
                console.log('not logged in');
                return router.navigate("login", true);
            }
            console.log('logged in');
            require(['views/dashboard'], function(DashboardView) {
                var session = appView.models.session;
                var view = Vm.reuseView('DashboardView', function() { return new DashboardView({ session: session }); });
                appView.render(view, true);
            });
        });

        Backbone.history.start();
    };  
    return {
        initialize: initialize
    };
});