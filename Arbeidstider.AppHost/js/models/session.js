define(["app", "jquery", "jquery.cookie", "backbone", "models/employee"],
    function (App, $, jquerycookie, Backbone, EmployeeModel) {
        var SessionModel = Backbone.Model.extend({
            //idAttribute: "EmployeeId",
            defaults: {
                // Change to UserId
                userId: "",
                userName: "",
                rememberMe: false,
                loggedIn: false
            },
            initialize: function () {
                this.employee = new EmployeeModel({});
                _.bindAll(this, "doAuth", "authSuccess", "authError", "checkAuth");
                this.set({ userName: $.cookie("userName") });
            },
            url: function () {
                return App.API + "/employee/auth/check";
            },
            checkAuth: function (callback, args) {
                var self = this;

                $.ajax({
                    type: "GET",
                    url: "/api/employee/verify",
                    data: {},
                    dataType: "json",
                    contentType: "application/json",
                    error: self.authError,
                    success: function (mod, resp) {
                        if (mod.IsAuthenticated) {
                            console.log("resp: " + resp);
                            self.set({ loggedIn: true });
                            if ("success" in callback) callback.success(mod, resp);
                            //self.employee.set(_.pick(resp, _.keys(self.employee.defaults)));
                        }
                        else {
                            self.set({ loggedIn: false });
                        }
                    }
                }).complete(function (mod, resp) {
                    if ("complete" in callback) callback.complete(mod, resp);
                });
            },
            authError: function (jqXHR, textStatus, errorThrown) {
                if (DEBUG) console.log("authError: ");
                if (DEBUG) console.log(errorThrown);
            },
            authSuccess: function (model, resp, options) {
                var self = this;
                console.log("model: " + JSON.stringify(model));
                if (model.SessionId && model.userName) {
                    //self.employee.set(_.pick(resp, _.keys(self.employee.defaults)));
                    if (DEBUG) console.log("authSuccess()");
                    self.set({ loggedIn: true });
                    self.set({ userName: model.userName });
                    self.employee.set({ id: model.employeeId });
                    self.employee.set({ workplaceId: model.workplaceId });
                    self.employee.set({ roles: model.roles });
                    $.removeCookie("userName", {path: '/'});
                    $.removeCookie("rememberMe", {path: '/'});
                    self.employee.set({ displayName: model.displayName });
                    if (self.get("rememberMe")) {
                        $.cookie("rememberMe", true, { path: '/' });
                        $.cookie("userName", model.userName, { path: '/' });
                    } else {
                        var expire = new Date();
                        // 1hr
                        expire.setTime(expire.getTime() + (60 * 1000));
                        $.cookie("userName", model.userName, { path: '/', expires: expire });
                        $.cookie("rememberMe", false, { path: '/', expires: expire });
                        self.set({ rememberMe: false });
                    }
                } else {
                    if (DEBUG) console.log("authSuccess: not authenticated");
                    self.set({ loggedIn: false });
                    self.set({ rememberMe: false });
                }
            },
            doAuth: function (authInfo, callback) {
                // refator to servicehelper
                var self = this;
                //$.param(dataToSend, true);
                // refactor to fetch
                if (authInfo.RememberMe) {
                    self.set({ rememberMe: true });
                }
                $.ajax({
                    type: "POST",
                    url: "/api/auth",
                    data: JSON.stringify(authInfo),
                    dataType: "json",
                    contentType: "application/json",
                    error: self.authError,
                    success: self.authSuccess
                }).complete(function () {
                    if ("complete" in callback) callback.complete();
                });
                //self.fetch({ data: dataToSend, wait: true, error: self.authError, success: self.authSuccess }).complete(function () {
                //    callback();
                //});
                //$.ajax({
                //    type: "GET",
                //    dataType: "json",
                //    data: data,
                //    contentType: "application/json",
                //    //TODO: Refactor to App.API: url
                //    url: self.getUrl("/authenticate"),
                //    error: function (jqXHR, textStatus, errorThrown) { console.log(errorThrown); },
                //    success: function (data) {
                //        if (data.IsAuthenticated) {
                //            console.log("checkAuth ok");
                //            self.employee.set(_.pick(data, _.keys(self.employee.defaults)));
                //            self.set({ loggedIn: true });
                //        } else {
                //            console.log("checkAuth not ok");
                //            self.set({ loggedIn: false });
                //        }
                //    },
                //    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
                //}).complete(function () {
                //    callback();
                //});
            },
            signOut: function () {
                this.set({ loggedIn: false });
                this.employee.set({});
                $.get("/api/auth/logout");
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