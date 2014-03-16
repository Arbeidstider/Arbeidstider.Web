define(["marionette",
        "session",
        "layouts/app",
        "layouts/main",
        "views/header",
        "views/navigation",
        "store"],
    function (Marionette, Session, AppLayout, MainLayout, HeaderView, NavigationView, Store) {
        var App = new Backbone.Marionette.Application();

        //Organize Application into regions corresponding to DOM elements
        //Regions can contain views, Layouts, or subregions nested as necessary
        App.addRegions({
            container: "#container"
        });

        App.setupJquery = function () {
            $.support.cors = true;
            $.ajaxSetup({
                statusCode: {
                    401: function () {
                        // Redirec the to the login page.
                        window.location.replace('/login');

                    },
                    403: function () {
                        // 403 -- Access denied
                        window.location.replace('/denied');
                    }
                }
            });
        };
        
        App.addInitializer(function () {
            App.setupJquery();
            App.initAppLayout();
            App.initMainLayout();
        });

        App.initAppLayout = function () {
            App.appLayout = new AppLayout();
            console.log("App.initAppLayout appLayout: ");
            console.log(App.appLayout);
            App.container.show(App.appLayout);
            App.appLayout.render();
            App.appLayout.header.show(new HeaderView());
            App.appLayout.nav.show(new NavigationView());
        };

        App.initMainLayout = function () {
            App.mainLayout = new MainLayout();
            console.log("App.initMainLayout appLayout: ");
            console.log(App.appLayout);
            App.appLayout.content.show(App.mainLayout);
            App.mainLayout.render();
            App.mainLayout.showDashboard(Session);
        };

        App.isAuthenticated = function () {
            console.log("session: \n");
            console.log(session);
            if (!_.isUndefined(session) && !_.isNumber(session.sessionId)) {
                console.log("authenticated");
                return true;
            }
            return false;
        };

        return App;
    });