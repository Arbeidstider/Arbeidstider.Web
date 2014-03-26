define(['App', 'backbone'
], function (App, Backbone) {
    var Employee = Backbone.Model.extend({
        idAttribute: "Id",
        defaults: {
            Id: null,
            FirstName: null,
            LastName: null,
            Email: null,
            BirthDate: null,
            WorkplaceId: null,
            Phonenumber: null,
            City: null,
            Address: null,
            Zipcode: null,
            Roles: null
        },
        initialize: function (opt) {
            console.log("Employee.initialize()");
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
        url: function () { return this.getUrl("/employee"); },
        validate: function () { }
    });
    return Employee;
});
