
define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'views/dailytimesheet',
        'collections/dailytimesheets',
        'text!templates/mytimesheets.html',
        'helpers/datehelper'
], function ($, _, Backbone, Marionette, DailyTimesheetView, DailyTimesheetCollection, CalendarTemplate, DateHelper) {
    return Backbone.Marionette.ItemView.extend( {
            template: _.template(CalendarTemplate),
            itemViewContainer: "#my-timesheets",
            initialize: function () {
                this.collection = new DailyTimesheetCollection();
                this.itemView = DailyTimesheetView;
                var params = {
                    StartDate: DateHelper.getDayOfWeek(1),
                    EndDate: DateHelper.getDayOfWeek(0),
                    UserID: 5,
                    WorkplaceID: 0
                };
                this.collection.fetch({
                    data: params
                });
                console.log(params);
                console.log("Calendar initialize");
            },
        });
    });