define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'models/timesheet'
], function ($, _, Backbone, Marionette, TimesheetModel) {
    return Backbone.Collection.extend({
        model: TimesheetModel,
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
        