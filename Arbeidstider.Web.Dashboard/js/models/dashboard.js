﻿define(['models/base'
    ], function(BaseModel) {
        var DashboardModel = BaseModel.extend({
            defaults: {
                employeeId: 0
            },
            initialize: function () {
                console.log("models/dashboard.initialize()");
            },
            url: function () { return this.getUrl("timesheet/dashboard/" + this.employeeId); }
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