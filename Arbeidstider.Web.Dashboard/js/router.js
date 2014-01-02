define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'mixins',
        'models/login'
    ], function($, _, Backbone, Vm, Mixins, LoginModel) {
        var AppRouter = Backbone.Router.extend({
            routes: {
                '': 'home',
                'login': 'login',
                'logout': 'logout',
                'unauthorized': 'unauthorized',
                'dashboard': 'dashboard',
                'notfound': 'notfound',
                'error': 'error'
                // Default - catch all
            }
        });

        var initialize = function(options) {
            console.log('router initialize');
            var appView = options.appView;
            var router = new AppRouter(options);
            this.cached = {
                model: undefined
            };

            var self = this;
            
            function setModels() {
                if (_.isUndefined(appView.models.login)) 
                    appView.models.login = self.cached.model = (self.cached.model || new LoginModel());
            };
            
            /* Login */
            router.on('route:login', function () {
                setModels();

                console.log('login route');
                require(['views/login'], function (LoginView) {
                    var view = Vm.reuseView('LoginView', function() { return new LoginView({ model: appView.models.login }); });
                    appView.render({ "#view-login": view });
                });
            });
            /* Logout */
            router.on('route:logout', function () {
                setModels();
                console.log('logout route');
                appView.signOut();
            });
            router.on('route:dashboard', function () {
                setModels();
                console.log('dashboard route');
                if (_.isLoggedIn) {
                    console.log("logged in");
                    require(['views/dashboard'], function(DashboardView) {
                        var view = Vm.reuseView('DashboardView', function() { return new DashboardView(); });
                        appView.render({ "#view-dashboard": view });
                    });
                    return;
                }
                Backbone.history.navigate("login", new { trigger: true });
            });
            /* Unauthorized */
            router.on('route:unauthorized', function() {
                console.log('unauthorized route');
            });
            router.on('route:notfound', function() {
                console.log('not found/404 route');
            });
            router.on('route:error', function() {
                console.log('error/500 route');
            });
            /* Default */
            router.on('route:home', function() {
                console.log("default route");
                if (!_.isLoggedIn) {
                    console.log("default route: not logged in");
                    Backbone.history.navigate("login", { trigger: true });
                } else {
                    console.log("default route: logged in");
                    Backbone.history.navigate("dashboard", { trigger: true });
                }
            });

            Backbone.history.start({ pushState: true });
        };
        return {
            initialize: initialize
        }
    });