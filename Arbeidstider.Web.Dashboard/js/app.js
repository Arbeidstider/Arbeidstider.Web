define(["marionette",
        "jquery",
        "jquery.cookie"
],
        function (Marionette, $, jquery_cookie) {
            var App = new Backbone.Marionette.Application();

            if (document.domain.indexOf("localhost") > -1)
                App.API = "http://localhost:8181";
            else
                App.API = "http://services.arbeidstider.no";

            //Organize Application into regions corresponding to DOM elements
            //Regions can contain views, Layouts, or subregions nested as necessary
            App.addRegions({
                content: "#app-content"
            });

            return App;
        });