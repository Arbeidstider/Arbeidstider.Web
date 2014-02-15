define(['jquery',
        'backbone',
        'marionette',
        'underscore',
        "layouts/appLayout",
        "layouts/content",
        "views/headers/calendar",
        "views/header",
        "views/navigation",
        "collections/calendardays",
        "views/calendar",
        "store",
        "routers/appRouter",
        "controllers/appController"],
    function ($, Backbone, Marionette, _, AppLayout, ContentLayout, CalendarHeaderView, HeaderView, NavigationView, CalendarDayCollection, CalendarView, Store, AppRouter, AppController) {
        var App = new Backbone.Marionette.Application();

        App.getSession = function () {
            return Store.get("AuthSession");
        };

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
            App.initLayout();
            App.initContentLayout();
        });

        App.initLayout = function () {
            App.layout = new AppLayout();
            App.container.show(App.layout);
            App.layout.render();
            App.layout.header.show(new HeaderView());
            App.layout.nav.show(new NavigationView());
        };

        App.initContentLayout = function () {
            App.contentLayout = new ContentLayout();
            App.layout.content.show(App.contentLayout);
            App.contentLayout.render();
            App.contentLayout.pageHeader.show(new CalendarHeaderView());
            var calendarView = App.contentLayout.getCalendarView();
            var view = calendarView.view;
            App.contentLayout.mainColumn.show(view);
            calendarView.reset();
        };

        App.isAuthenticated = function () {
            var session = App.getSession();
            console.log(session);
            if (!_.isUndefined(session) && !_.isNumber(session.sessionId)) {
                console.log("authenticated");
                return true;
            }
            return false;
        };

        return App;
    });