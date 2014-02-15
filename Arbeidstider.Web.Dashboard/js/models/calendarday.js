define(['backbone'],
    function (Backbone) {
        var CalendarDayModel = Backbone.Model.extend({
            defaults: {
                Id: 0,
                DayOfWeek: 0,
                EmployeeId: 0,
                Date: "",
                Shifts: []
            },
            initialize: function (options) {
                console.log("Calendar day model initialize");
                console.log("options: " + JSON.stringify(options));
            },
            parse: function (response) {
                console.log("response: " + response);
                return response;
            }
        });

        return CalendarDayModel;
    });