define([
        'models/base'
], function (BaseModel) {
    var TimesheetModel = BaseModel.extend({
        idAttribute: "Id",
        defaults: {
            Id: null,
            UserId: null,
            EmployeeId: null,
            WorkplaceId: "",
            StartDate: null,
            EndDate: null,
            ShiftStart: null,
            ShiftEnd: null,
        },
        initialize: function () {
        },
        url: function () { return this.getUrl("/timesheet"); }
    });

    return TimesheetModel;
});;
