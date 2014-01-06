define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'models/timesheet',
        'models/settings'
], function ($, _, Backbone, Marionette, TimesheetModel, Settings) {
    return Backbone.Collection.extend({
        model: TimesheetModel,
        action: "/timesheets",
        initialize: function () {
        },
        parse: function(response) {
            console.log("TimesheetsCollection.parse(): " + JSON.stringify(response.Timesheets));
            return response.Timesheets;
        },
        url: function() {
            return Settings.ServiceUrl(this.action);
        }
    });
});
        