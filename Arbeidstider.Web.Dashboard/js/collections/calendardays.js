define(['backbone',
        'models/calendarday',
        'settings'
],
    function (Backbone, CalendarDayModel, Settings) {
        var CalendarDayCollection = Backbone.Collection.extend({
            model: CalendarDayModel,
            initialize: function (options) {
            },
            parse: function (response) {
                return response.WeeklyTimesheetCalendar;
            },
            url: function () {
                return Settings.ServiceUrl("/timesheets");
            }
        });

        return CalendarDayCollection;
    });