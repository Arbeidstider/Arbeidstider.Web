define([
        'models/base'
    ], function(BaseModel) {
        var Employee = BaseModel.extend({
            defaults: {
            },
            initialize: function (opt) {
                console.log("Employee.initialize()");
            },
        });
        return Employee;
    });
