define(['app', 'backbone'
    ], function(App, Backbone) {
        var DashboardModel = Backbone.Model.extend({
            defaults: {
                // id is employeeId
                EmployeeId: null,
                Day: null,
                StartHour: null,
                EndHour: null,
                Date: null
            },
            initialize: function (options) {
                console.log("models/dashboard.initialize()");
            },
            parse: function(resp) {
                console.log("parse() resp: ");
                console.log(resp);
            },
            url: function () { return App.API + "/workinghours/upcoming"; }
            //createEmployee: function() {
            //    var employee = {
            //        Firstname: $("input[name=Firstname]").text(),
            //        Lastname: $("input[name=Lastname]").text(),
            //        Mobile: $("input[name=Mobile]").text(),
            //        BirthDate: $("input[name=BirthDate]").text(),
            //        EmployeeGroup: $("input[name=EmployeeGroup]").text(),
            //        Email: $("input[name=Email]").text()
            //    };

            //    $.get("/register", { employee: employee }, function(data) {
            //        if (data["Result"] == true) alert("employee created");
            //    });
            //}
        });
        
        return DashboardModel;
    });