define(['marionette'],
    function (Marionette) {
        return Backbone.Marionette.AppRouter.extend({
            //"index" must be a method in AppRouter's controller
            appRoutes: {
                "": "index",
                "dashboard": "index",
                'register': 'register',
                //"dashboard": "index",
                "profile": "profile",
                "logout": "logout",
                "signout": "logout",
                "logoff": "logout",
                "changeWorkDay": "changeWorkDay",
                "setDaysFree": "setDaysFree",
                "confirmAvailableDays": "confirmAvailableDays",
                "addressbook": "addressbook",
                "messages": "messages"
                /*
                'logout': 'logout',
                'unauthorized': 'unauthorized',
                'notfound': 'notfound',settings
                'error': 'error',
                */
            }
        });
    });