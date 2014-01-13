define(['backbone',
        'backbone_validateAll',
        'models/base'
    ], function(Backbone, Backbone_validateAll, BaseModel) {
        var Employee = BaseModel.extend({
            defaults: {
                Firstname: "",
                Surname: "",
                BirthDate: ""
            },
            initialize: function (opt) {
                console.log("Employee.initialize()");
            }, validate: function (attrs, options) {
                console.log(attrs);
                if (attrs.Email == "")
                    console.log(attrs.Email);
                return true;
            }
        });
        return Employee;
    });
