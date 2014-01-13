define(['jquery',
        'backbone',
        'marionette',
        'underscore',
        "helpers/mixins",
        "layouts/appLayout",
        "views/header",
        "views/navigation",
        "store",
        "routers/appRouter",
        "controllers/appController"],
    function ($, Backbone, Marionette, _, mixins, AppLayout, HeaderView, NavigationView, Store, AppRouter, AppController) {
        var App = new Backbone.Marionette.Application();

        function isMobile() {
            var userAgent = navigator.userAgent || navigator.vendor || window.opera;
            return ((/iPhone|iPod|iPad|Android|BlackBerry|Opera Mini|IEMobile/).test(userAgent));
        }

        //Organize Application into regions corresponding to DOM elements
        //Regions can contain views, Layouts, or subregions nested as necessary
        App.addRegions({
            mainRegion: "#main"
        });
        
        App.addInitializer(function () {
            if (!App.isAuthenticated()) {
                window.location.replace("/login");
            }
            App.appRouter = new AppRouter({controller: new AppController()});
            App.layout = new AppLayout();
            App.mainRegion.show(this.layout);
            App.layout.render();
            App.layout.header.show(new HeaderView());
            App.layout.nav.show(new NavigationView());
            
            Backbone.history.start({ pushState: true });
        });

        App.isAuthenticated = function () {
            var session = Store.get("AuthSession");
            if (!_.isUndefined(session)) {
                console.log("sessionuserid: " + session.userId);
                console.log("sessionid: " + session.sessionId);
            }
            return _.isObject(session) && session.sessionId && session.userId;
        };

        App.mobile = isMobile();

        return App;
    });