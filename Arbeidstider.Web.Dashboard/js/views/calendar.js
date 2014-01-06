define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'collections/timesheets',
        'text!templates/calendar.html',
        'helpers/datehelper'
], function ($, _, Backbone, Marionette, TimesheetCollection, CalendarTemplate, DateHelper) {
    return Backbone.Marionette.CompositeView.extend( {
            template: _.template(CalendarTemplate),
            itemViewContainer: "#calendar",
            initialize: function() {
                var params = {
                    StartDate: DateHelper.getDayOfWeek(1),
                    EndDate: DateHelper.getDayOfWeek(0),
                    UserID: 5,
                    WorkplaceID: 0
                };
                console.log(params);
                this.collection.fetch({
                    data: params
                });
                console.log("Calendar initialize");
            },
        });
    });