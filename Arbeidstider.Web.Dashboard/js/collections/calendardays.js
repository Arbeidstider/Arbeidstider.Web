define(['backbone',
        'models/calendarday',
        'models/settings'
],
    function (Backbone, CalendarDayModel, Settings) {
        var CalendarDayCollection = Backbone.Collection.extend({
            model: CalendarDayModel,
            initialize: function (options) {
            },
            parse: function (response) {
                console.log("CalendarDayCollection.parse: " + JSON.stringify(response.WeeklyTimesheetCalendar));
                return response.WeeklyTimesheetCalendar;
            },
            url: function () {
                return Settings.ServiceUrl("/timesheets");
            }
        });

        return CalendarDayCollection;
    });