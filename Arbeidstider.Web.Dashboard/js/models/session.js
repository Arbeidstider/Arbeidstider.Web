define([
    'jquery',
    'underscore',
    'backbone',
    'models/base',
    'events',
    'truStorage',
    'router'
], function ($, _, Backbone, BaseModel, Events, truStorage, Router) {
    var Session = BaseModel.extend({
        defaults: {
            isAuthenticated: false,
            sessionId: null,
            form: null,
            fullname: "",
        },
        initialize: function () {
            _.bindAll(this, "signIn", "loginError", "signOut", "getData","getAuthSession", "makeBaseAuth", "populateSession");
            console.log("session initialize");
        },
        populateSession: function (response) {
            var authSession = this.getAuthSession(response);
            console.log("populatesession: " + authSession);
            truStorage.setItem("AuthSession", authSession);
            var router = new Router.AppRouter();
            router.navigate('');
            this.set({ isAuthenticated: true });
        },
        getData: function () {
           return  {
                UserName: $('input[name="UserName"]').val(),
                Password: $('input[name="Password"]').val(),
                RememberMe: true
           };
        },
        signOut: function () {
            truStorage.setItem("AuthSession", null);
            this.set({ isAuthenticated: false });
            $.getJSON(this.ServiceUrl("/auth/logout"));
            window.location.href = "/";
        },
		formData: function(form) {
			var ret = {};
			$(form).find("INPUT,TEXTAREA").each(function() {
				if (this.type == "button" || this.type == "submit") return;
				if (this.type == "checkbox") {
				    if (!this.checked) return;
				    ret[this.name] = this.value === "on" ? true : this.value;
			        return;
			    }
				ret[this.name] = $(this).val();
			});
			return ret;
		},
		makeBaseAuth: function() {
		    var data = this.getData();
             var tok = data.UserName + ':' + data.Password;
             var hash = btoa(tok);
             return "Basic " + hash;
		},
        signIn: function (form) {
		    var auth = this.makeBaseAuth();
		    this.ajaxRequest(
		        "POST", "/auth", JSON.stringify(this.formData(form)), this.populateSession, this.loginError, false, false,
		        function (xhr) { xhr.setRequestHeader("Authorization", auth); }
		    );
        },
        getAuthSession: function (response) {
            var data = {
                UserName: response.UserName,
                UserId: response.UserId,
                SessionId: response.SessionId
            };
            return JSON.stringify(data);
        },
        loginError: function (jqXHR, textstatus, errorThrown) {
            /* Handle error */
            if (jqXHR.status == 401) {
                //this.loginAgain();
            }

            console.log("jqXHR: " + jqXHR);
            console.log("textstatus: " +textstatus);
            console.log("errorThrown" + errorThrown);
        },
        validate: function () {
            return true;
        },
    });

    return Session;
});