// Require.js allows us to configure shortcut alias
// Their usage will become more apparent futher along in the tutorial.
require.config({
    paths: {
        // Major libraries
        jquery: '//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min',
        jquery_ui: '//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min',
        underscore: 'libs/underscore/underscore', // https://github.com/amdjs
        "underscore.deferred": 'libs/underscore.deferred/underscore.deferred', // https://github.com/amdjs
        backbone: 'libs/backbone/backbone', // https://github.com/amdjs
        backbone_validateAll: 'libs/backbone.validateAll/backbone.validateAll', // 
        "backbone.wreqr": 'libs/backbone.wreqr/wreqr', // 
        "backbone.babysitter": 'libs/backbone.babysitter/backbone.babySitter', // 
        marionette: 'libs/backbone.marionette/backbone.marionette', // 
        jquery_mobile: 'libs/jquery_mobile/jquery.mobile.custom',
        bootstrap: 'libs/bootstrap/bootstrap',
        html5shiv: 'libs/html5shiv/html5shiv',
        store: 'libs/store/store',

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
        "underscore.deferred": {
            deps: ['underscore']
        },
        backbone: {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        'backbone.babysitter': {
            deps: ['backbone', 'underscore']
        },
        "marionette": {
            "deps": ["underscore", "backbone", "jquery"],
            // Exports the global window.Marionette object
            "exports": "Marionette"
        },
    }
});

// Let's kick off the application
require(['jquery',
        'backbone',
        'routers/appRouter',
        'collections/calendardays',
        'controllers/appController',
        'helpers/bootstrapper',
        'app'
], function ($, Backbone, AppRouter, CalendarDayCollection, AppController, Bootstrapper, App) {
    $(document).ready(function () {
        App.on("initialize:after", function () {
            Backbone.history.start();
        });

        App.start();
        console.log("App.start");
    });
});