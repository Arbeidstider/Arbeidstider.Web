define(['backbone',
        'marionette',
        'underscore',
        "layouts/appLayout",
        "views/header",
        "views/navigation",
        "collections/calendardays",
        "views/calendar",
        "store",
        "routers/appRouter",
        "controllers/appController"],
    function (Backbone, Marionette, _, AppLayout, HeaderView, NavigationView, CalendarDayCollection, CalendarView, Store, AppRouter, AppController) {
        var App = new Backbone.Marionette.Application();

        App.getSession = function() {
            return Store.get("AuthSession");
        };

        //Organize Application into regions corresponding to DOM elements
        //Regions can contain views, Layouts, or subregions nested as necessary
        App.addRegions({
            mainRegion: "#main"
        });
        
        App.addInitializer(function () {
            if (!App.isAuthenticated()) {
                window.location.replace("/login");
            }
            
            App.appRouter = new AppRouter({ controller: new AppController() });
            App.initLayout();
            App.inititalizeTimesheets();
            
            Backbone.history.start({ pushState: true });
        });

        App.initLayout = function() {
            App.layout = new AppLayout();
            App.mainRegion.show(App.layout);
            App.layout.render();
            App.layout.header.show(new HeaderView());
            App.layout.nav.show(new NavigationView());
        };
        
        App.inititalizeTimesheets = function() {
            console.log("initializeTimesheets");
            var session = App.getSession();
            var collection = new CalendarDayCollection();
            var calendarView = new CalendarView({ collection: collection});
            var p = collection.fetch({ data: { WeeklyView: true, EmployeeId: session.employeeId} });
            p.done(function() {
                console.log("done");
                App.layout.main.show(calendarView);
            });
        };

        App.isAuthenticated = function () {
            var session = App.getSession();
            if (!_.isUndefined(session) && !_.isNumber(session.sessionId)) {
                console.log("session: " + session);
                console.log("session: " + JSON.stringify(session));
                console.log("sessionuserid: " + session.userId);
                console.log("sessionid: " + session.sessionId);
                return true;
            }
            return false;
        };

        return App;
    });