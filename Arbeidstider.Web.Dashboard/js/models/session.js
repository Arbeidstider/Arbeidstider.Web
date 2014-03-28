define(["app", "jquery", "jquery.cookie", "backbone", "models/employee"],
    function (App, $, jquerycookie, Backbone, EmployeeModel) {
        var SessionModel = Backbone.Model.extend({
            //idAttribute: "EmployeeId",
            defaults: {
                EmployeeId: null,
                SessionId: null,
                Role: 3,
                loggedIn: false
            },
            initialize: function () {
                var employeeId = $.cookie("employeeId");
                var sessionId = $.cookie("sessionId");
                this.set({ EmployeeId: _.isUndefined(employeeId) ? null : employeeId });
                this.set({ SessionId: _.isUndefined(sessionId) ? null : sessionId });
                this.employee = new EmployeeModel({});
                _.bindAll(this, "doAuth", "authSuccess", "authError", "checkAuth", "parse", "setRole");
            },
            setRole: function () {
                if (_.isUndefined(this.employee.Roles)) return;
                if (this.employee.Roles.contains("Administrator")) {
                    this.set({ Role: 0 });
                    return;
                }
                if (this.employee.Roles.contains("Manager")) {
                    this.set({ Role: 1 });
                    return;
                }
                if (this.get("isLoggedIn")) {
                    this.set({ Role: 2 });
                    return;
                }
                this.set({ Role: 3 });
            },
            url: function () {
                return App.API + "/employee/auth";
            },
            checkAuth: function (callback, args) {
                var self = this;
                var data = { EmployeeId: self.get("EmployeeId"), SessionId: self.get("SessionId") };
                self.fetch({
                    data: data,
                    url: self.url() + "/check",
                    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", self.get("SessionId")); },
                    error: function (mod, resp) { if ("error" in callback) callback.error(mod, resp); },
                    success: function (mod, resp) { if ("success" in callback) callback.success(mod, resp); },
                }).complete(function () {
                    if ("complete" in callback) callback.complete();
                });
                // refactor to fetch
                //$.ajax({
                //    type: "POST",
                //    dataType: "json",
                //    data: data,
                //    contentType: "application/json",
                //    //TODO: Refactor to App.API: url
                //    url: this.getUrl("/authenticate/check"),
                //    error: function (mod, res) {
                //        self.set({ loggedIn: false });
                //        if ('error' in callback) callback.error(mod, res);
                //    },
                //    success: function (res) {
                //        if (data) {
                //            console.log("checkAuth ok");
                //            self.set({ loggedIn: true });
                //            if ('success' in callback) callback.success(mod, res);
                //        } else {
                //            console.log("checkAuth not ok");
                //            self.set({ loggedIn: false });
                //            if ('error' in callback) callback.error(mod, res);
                //        }
                //    },
                //    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
                //}).complete(function () {
                //    if ('complete' in callback) callback.complete();
                //});
            },
            authError: function (jqXHR, textStatus, errorThrown) {
                if (DEBUG) console.log("authError: ");
                if (DEBUG) console.log(errorThrown);
            },
            authSuccess: function (model, resp, options) {
                var self = this;
                if (resp.IsAuthenticated) {
                    self.employee.set(_.pick(resp, _.keys(self.employee.defaults)));
                    if (DEBUG) console.log("authSuccess() self.employee: " + JSON.stringify(self.employee));
                    self.set({ loggedIn: true });
                } else {
                    if (DEBUG) console.log("authSuccess: not authenticated");
                    self.set({ loggedIn: false });
                }
            },
            doAuth: function (callback) {
                // refator to servicehelper
                var self = this;
                var dataToSend = { EmployeeId: self.get("EmployeeId"), SessionId: self.get("SessionId") };
                //$.param(dataToSend, true);
                // refactor to fetch
                self.fetch({ data: dataToSend, wait: true, error: self.authError, success: self.authSuccess }).complete(function () {
                    callback();
                });
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
                $.removeCookie("sessionId", {path: '/'});
                $.removeCookie("userId", {path: '/'});
                $.removeCookie("employeeId", {path: '/'});
                $.removeCookie("userName", {path: '/'});
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