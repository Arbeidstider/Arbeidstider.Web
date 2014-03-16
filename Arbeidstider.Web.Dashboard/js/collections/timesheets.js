define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'models/timesheet',
        'settings'
], function ($, _, Backbone, Marionette, TimesheetModel, Settings) {
    return Backbone.Collection.extend({
        model: TimesheetModel,
        action: "/timesheets",
        initialize: function () {
        },
        parse: function (response) {
            return response.Timesheets;
        },
        url: function() {
            return Settings.ServiceUrl(this.action);
        }
    });
});
        