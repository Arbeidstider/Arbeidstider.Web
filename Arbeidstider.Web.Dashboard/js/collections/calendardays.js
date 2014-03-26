define(['backbone',
        'models/calendarday'
],
    function (Backbone, CalendarDayModel) {
        var CalendarDayCollection = Backbone.Collection.extend({
            model: CalendarDayModel,
            initialize: function (options) {
            },
            parse: function (response) {
                return response.WeeklyTimesheetCalendar;
            },
            url: function () {
            }
        });

        return CalendarDayCollection;
    });