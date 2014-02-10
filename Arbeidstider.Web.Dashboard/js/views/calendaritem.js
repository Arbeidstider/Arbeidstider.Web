define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'text!templates/calendaritem.html'
], function ($, _, Backbone, Marionette, CalendarItemTemplate) {
    return Backbone.Marionette.ItemView.extend( {
            template: _.template(CalendarItemTemplate),
            initialize: function (options) {
                console.log("CalendarItemView this.model: " + JSON.stringify(options.model));
                console.log("CalendarItemView initialize");
            },
        });
    });