define([
        'jquery',
        'underscore',
        'backbone',
        'marionette',
        'models/dailytimesheet',
        'collections/timesheets',
        'text!templates/mytimesheets.html',
        'helpers/datehelper'
], function ($, _, Backbone, Marionette, DailyTimesheetModel, TimesheetCollection, MyTimesheetsTemplate, DateHelper) {
    return Backbone.Marionette.CompositeView.extend( {
            template: _.template(MyTimesheetsTemplate),
            itemViewContainer: "#my-timesheets",
            initialize: function (options) {
                if (options) this.session = options.session;
                this.collection = new TimesheetCollection();
                this.itemView = DailyTimesheetModel;
                var params = {
                    StartDate: DateHelper.getDayOfWeek(1),
                    EndDate: DateHelper.getDayOfWeek(0),
                    UserID: this.session.userId,
                    WorkplaceID: 0
                };
                this.collection.fetch({
                    data: params
                });
                console.log(params);
                console.log("MyTimesheetsView initialize");
            },
        });
    });