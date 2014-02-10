define(['backbone',
        'backbone_validateAll',
        'models/base'
], function (Backbone, Backbone_validateAll, BaseModel) {
    var Employee = BaseModel.extend({
        defaults: {
            FirstName: "",
            LastName: "",
            Email: "",
            BirthDate: ""
        },
        initialize: function (opt) {
            console.log("Employee.initialize()");
        },
        validate: function (attrs, options) {
            return true;
        }
    });
    return Employee;
});
