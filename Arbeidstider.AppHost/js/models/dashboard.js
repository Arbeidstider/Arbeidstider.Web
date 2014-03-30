define(['app', 'backbone'
    ], function(App, Backbone) {
        var DashboardModel = Backbone.Model.extend({
            defaults: {
                // id is employeeId
                Day: null,
                StartHour: null,
                EndHour: null,
                Date: null
            },
            initialize: function (options) {
                if (DEBUG) console.log("models/dashboard.initialize()");
                    var nextScheduledDay = App.session.employee.get("nextScheduledDay");
                    if (nextScheduledDay) {
                        self.set(nextScheduledDay);
                    }
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