define(['App', 'backbone'
], function (App, Backbone) {
    var Employee = Backbone.Model.extend({
        idAttribute: "EmployeeId",
        defaults: {
            id: null,
            displayName: null,
            // FirstName: null,
            // LastName: null,
            // Email: null,
            // PrimaryEmail: null,
            // BirthDate: null,
            workplaceId: null,
            // Phonenumber: null,
            // City: null,
            // Address: null,
            // Zipcode: null,
            roles: null,
            nextScheduledDay: null
        },
        initialize: function (opt) {
            if (DEBUG) console.log("Employee.initialize()");
        },
        isAdmin: function () {
            var roles = this.get("Roles").split(",");
            for (var i = 0; i < roles.length; i++) {
                if (roles[i] == "Administrator") return true;
            }
            return false;
        },
        isManager: function () {
            var roles = this.get("Roles").split(",");
            for (var i = 0; i < roles.length; i++) {
                if (roles[i] == "Manager") return true;
            }
            return false;
        },
        url: function () {
            return App.API + "/employee";
        },
        validate: function () { }
    });
    return Employee;
});
