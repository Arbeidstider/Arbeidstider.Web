// Require.js allows us to configure shortcut alias
// Their usage will become more apparent futher along in the tutorial.
require.config({
    paths: {
        // Major libraries
        jquery: '//ajax.googleapis.com/ajax/libs/jquery/2.0.3/jquery.min',
        "jquery.ui": '//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min',
        "jquery.cookie": 'libs/jquery.cookie/jquery.cookie',
        underscore: 'libs/underscore/underscore', // https://github.com/amdjs
        backbone: 'libs/backbone/backbone', // https://github.com/amdjs
        "backbone.wreqr": 'libs/backbone.wreqr/wreqr', // 
        "backbone.babysitter": 'libs/backbone.babysitter/backbone.babySitter', // 
        marionette: 'libs/backbone.marionette/backbone.marionette', // 
        "jquery.mobile": 'libs/jquery.mobile/jquery.mobile.custom',
        bootstrap: 'libs/bootstrap/bootstrap',
        html5shiv: 'libs/html5shiv/html5shiv',
        store: 'libs/store/store',
        nav: 'libs/bootstrap/nav',

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
        'backbone.babysitter': {
            deps: ['backbone', 'underscore']
        },
        'jquery.cookie': {
            deps: ['jquery']
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
        'bootstrap',
        'models/session',
        'app',
        'routers/appRouter',
], function ($, Backbone, Bootstrap, SessionModel, App, AppRouter) {
    // Just use GET and POST to support all browsers
    Backbone.emulateHTTP = true;
    App.session = new SessionModel();
    App.router = new AppRouter();

    App.session.checkAuth(function () {
        // HTML5 pushState for URLs without hashbangs
        var hasPushstate = !!(window.history && history.pushState);
        if (hasPushstate) Backbone.history.start({ pushState: true, root: '/' });
        else Backbone.history.start();
    });

    // All navigation that is relative should be passed through the navigate
    // method, to be processed by the router. If the link has a `data-bypass`
    // attribute, bypass the delegation completely.
    $('#content-wrapper').on("click", "a:not([data-bypass])", function (evt) {
        console.log("main click handler");
        evt.preventDefault();
        var href = $(this).attr("href");
        App.router.navigate(href, { trigger: true, replace: false });
    });

    // Dropdown toggle, pretty much
    Bootstrap.initialize();
});