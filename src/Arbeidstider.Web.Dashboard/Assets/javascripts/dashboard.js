var Dashboard = {
    CreateEmployee: function () {
        var employee = {
            Firstname: $("input[name=Firstname]").text(),
            Lastname: $("input[name=Lastname]").text(),
            Mobile: $("input[name=Mobile]").text(),
            BirthDate: $("input[name=BirthDate]").text(),
            EmployeeGroup: $("input[name=EmployeeGroup]").text(),
            Email: $("input[name=Email]").text()
        };

        $.get("/register", { employee: employee }, function (data) {
            if (data["Result"] == true) alert("employee created");
        });
    },
    Logoff: function () {
        $.post("/logoff", function (data) {
        });
    }
};

$("#new-employee-submit-button").on("click", function (e) {
    e.preventDefault();
    Dashboard.CreateEmployee();
})