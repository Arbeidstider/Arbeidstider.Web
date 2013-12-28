define([
        'models/base'
    ], function(BaseModel) {
        var MyWorkingHours = BaseModel.extend({
            urlRoot: '/timesheet/getweekly',
            initialize: function() {
            },
        });

        return MyWorkingHours;
    });