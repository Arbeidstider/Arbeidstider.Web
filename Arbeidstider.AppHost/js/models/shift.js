define(['backbone'
],
    function (Backbone) {
        var ShiftModel = Backbone.Model.extend({
            defaults: {
                Id: 0,
                DayOfWeek: "",
                ShiftDate: "",
                EmployeeId: 0,
                ShiftEnd: "",
                ShiftStart: ""
            },
            initialize: function (options) {
            },
        });

        return ShiftModel;
    });