define([
    'jquery',
    'underscore',
    'backbone',
    'models/base'
], function ($, _, Backbone, BaseModel) {
    var Session = BaseModel.extend({
        defaults: {
            isAuthenticated: false,
            sessionId: null,
            form: null,
        },
        initialize: function () {
            _.bindAll(this, "signIn", "loginSuccess", "loginError", "signOut", "getData");
        },
        getData: function() {
           return  {
                "UserName": $('input[name="UserName"]').val(),
                "Password": $('input[name="Password"]').val(),
                "RememberMe": true,
                "provider": "credentials"
            };
        },
        signOut: function () {
            $.getJSON(this.serviceUrl("/auth/logout"));
            this.set({ isAuthenticated: false });
        },
        signIn: function () {
            this.ajaxRequest("POST", "/auth/credentials", this.getData(), this.loginSuccess, this.loginError);
        },
        loginSuccess: function (response, status) {
            console.log("success: " + response);
            this.set({ isAuthenticated: true, sessionId: response.SessionId, });
        },
        loginError: function (jqXHR, textstatus, errorThrown) {
            /* Handle error */
            console.log(jqXHR);
            console.log(textstatus);
            console.log(errorThrown);
        },
        validate: function () {
            return true;
        },
    });

    return Session;
});