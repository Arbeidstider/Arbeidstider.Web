define(["jquery", "jquery.cookie", "settings", "models/employee"],
    function ($, jquerycookie, Settings, EmployeeModel) {
        var SessionModel = Backbone.Model.extend({
            defaults: {
                loggedIn: false,
                employeeId: '',
                userId: ''
            },
            initialize: function () {
                //_.bindAll(this, "verifySession", "loadSession");
                //this.sessionId = $.cookie("sessionId");
                //this.userId = $.cookie("userId");
                //this.employeeId = $.cookie("employeeId");
                //this.userName = $.cookie("userName");
                var employeeId = $.cookie("employeeId");
                var userId = $.cookie("userId");
                this.userId = _.isUndefined(userId) ? null : userId;
                this.employeeId = _.isUndefined(employeeId) ? null : employeeId;
                this.employee = new EmployeeModel({});
            },
            checkAuth: function (callback) {
                // refator to servicehelper
                var data = { EmployeeId: this.employeeId };
                var self = this;
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    data: data,
                    contentType: "application/json",
                    url: Settings.ServiceUrl("employee/session/get"),
                    error: function (jqXHR, textStatus, errorThrown) { console.log(errorThrown); },
                    success: function (data) {
                        if (data) {
                            console.log("data ok");
                            self.employee.set(_.pick(data, _.keys(self.employee.defaults)));
                            self.set({ loggedIn: true });
                        } else {
                            console.log("data not ok");
                            self.set({ loggedIn: false });
                        }
                    },
                    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
                }).complete(function() {
                    callback();
                });
            },
            signOut: function () {
                $.removeCookie("sessionId");
                $.removeCookie("userId");
                $.removeCookie("employeeId");
                $.removeCookie("userName");
                this.set({ loggedIn: false });
                this.employee.set({});
                $.get("/auth/logout");
                window.location.replace("/login");
            }
        });

        return SessionModel;
    }
);


        /*
        Session.signIn = function (session) {
            store.remove("sessionId");
            // 1hr expire time for login if remember me is not checked, storewithexpiration module
            var exp = 60000;
            store.set("sessionId", session.sessionId);
            console.log("signIn()");
            console.log(session.sessionId);
            console.log(session);
            employeeId = session.employeeId;
            userId = session.userId;
            workplaceId = session.workplaceId;
            email = session.email;
            username = session.username;
            fullname = session.fullname;
            roles = session.roles;
            window.location.replace("/");
        };
        var Session = {
            "sessionId": "SessionId",
            employeeId: 0,
            userId: 0,
            workplaceId: 0,
            email: "",
            username: "",
            fullname: "",
            roles: [],
        };
        */