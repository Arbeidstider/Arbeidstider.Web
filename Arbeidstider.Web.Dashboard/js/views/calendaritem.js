define(['marionette',
], function (Marionette) {
    return Backbone.Marionette.ItemView.extend( {
            initialize: function (options) {
                console.log("CalendarItemView this.model: " + JSON.stringify(options.model));
                console.log("CalendarItemView initialize");
            },
        });
    });