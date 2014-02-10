define([
        'models/base'
], function (BaseModel) {
    var TimesheetModel = BaseModel.extend({
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
        idAttribute: "Id",
        initialize: function () {
        },
    });

    return TimesheetModel;
});;
