define(['backbone', 'collections/timesheets'],
    function (Backbone, TimesheetCollection) {
        var CalendarDayModel = Backbone.Model.extend({
            defaults: {
                Id: 0,
                DayOfWeek: 0,
                Shifts: []
            },
            initialize: function (options) {
                console.log("Calendar day model initialize");
            },
            parse: function (response) {
                console.log("calendarday.parse");
                var models = [];
                if (response.Timesheets.length != 0) {
                    for (var i = 0; i < response.Timesheets.length; i++) {
                        var timesheet = JSON.stringify(response.Timesheets[i]);
                        console.log("timesheet: " + timesheet);
                        models.push(timesheet);
                    }
                }
                
                console.log("models");
                console.log(models);
                var result = { Id: response.Id, DayOfWeek: response.DayOfWeek, Shifts: models };
                console.log("result: " + result);
                return result;
            }
        });

        return CalendarDayModel;
    });