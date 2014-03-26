define(["marionette",
        "jquery"
],
        function (Marionette, $) {
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

            $.support.cors = true;
            $.ajaxSetup({
                cache: false,
                statusCode: {
                    401: function () {
                        // Redirec the to the login page.
                        alert("401");
                        window.location.replace('/login');

                    },
                    403: function () {
                        // 403 -- Access denied
                        window.location.replace('/denied');
                    }
                }
            });


            return App;
        });