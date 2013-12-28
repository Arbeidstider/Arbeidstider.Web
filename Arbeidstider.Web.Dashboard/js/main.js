// Require.js allows us to configure shortcut alias
// Their usage will become more apparent futher along in the tutorial.
require.config({
  paths: {
    // Major libraries
    jquery: '//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min',
    jquery_ui: '//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min',
    underscore: 'libs/underscore/underscore', // https://github.com/amdjs
    backbone: 'libs/backbone/backbone', // https://github.com/amdjs
    'backbone.localStorage' : 'libs/backbone.localStorage', 
    bootstrap: 'libs/bootstrap/bootstrap.min', 
    html5shiv: 'libs/html5shiv/html5shiv', 

    // Require.js plugins
    text: 'libs/require/text',

    // Just a short cut so we can put our html outside the js dir
    // When you have HTML/CSS designers this aids in keeping them out of the js directory
    templates: '../templates'
  },
  shim: {
    underscore: {
      exports: "_"
    },
    backbone: {
      deps: ['underscore', 'jquery'],
      exports: 'Backbone'
    },
    'backbone.localStorage': {
      deps: ['backbone'],
      exports: 'Backbone'
    }
  }
});

// Let's kick off the application

require(['jquery',
        'underscore',
        'backbone',
        'vm',
        'views/app',
        'router',
    ], function($, _, Backbone, Vm, AppView, Router) {

        var appView = Vm.reuseView("AppView", function() { return new AppView(); });
        Router.initialize({ appView: appView }); // The router now has a copy of all main appview
        // check redis session if authenticated and set on loginmodel

        jQuery.cors = true;

        Backbone.View.prototype.close = function () {
            console.log("close");
            this.remove();
            this.unbind();
            this.stopListening();
            if (this.onClose) {
                this.onClose();
            }
        };
    });