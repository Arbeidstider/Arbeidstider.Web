define(['models/base',
        'settings'
], function (BaseModel, Settings) {
    var Employee = BaseModel.extend({
        defaults: {
            FirstName: null,
            LastName: null,
            Email: null,
            BirthDate: null,
            WorkplaceId: null,
            UserName: null,
            EmployeeId: null,
            Roles: ""
        },
        initialize: function (opt) {
            console.log("Employee.initialize()");
            _.bindAll(this, "url");
            //_.bindAll(this, "getUrl");
        },
        isAdmin: function () {
            var roles = this.Roles.split(",");
            for (var i = 0; i < roles.length; i++) {
                if (roles[i] == "Administrator") return true;
            }
            return false;
        },
        isManager: function () {
            var roles = this.Roles.split(",");
            for (var i = 0; i < roles.length; i++) {
                if (roles[i] == "Manager") return true;
            }
            return false;
        },
        url: function () { return Settings.ServiceUrl("employee/get"); },
    });
    return Employee;
});
