define(["marionette",
        "layouts/app",
        "layouts/main",
        "models/session",
        "views/header",
        "views/navigation",
        "nav",
        "store"],
        function (Marionette, AppLayout, MainLayout, SessionModel, HeaderView, NavigationView, Nav, Store) {
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
                App.initLayouts();
            });

            App.initLayouts = function () {
                var appLayout = new AppLayout();
                App.container.show(appLayout);
                appLayout.render();
                appLayout.header.show(new HeaderView());
                appLayout.nav.show(new NavigationView());
                Nav.initialize();

                var mainLayout = App.mainLayout = new MainLayout();
                appLayout.content.show(mainLayout);
                mainLayout.render();
                mainLayout.showDashboard();
            };

            return App;
        });