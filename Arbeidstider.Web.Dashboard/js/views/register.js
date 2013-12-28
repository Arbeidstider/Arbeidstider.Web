app.RegisterView = app.BaseView.extend({
	className: "view-register",
	initialize: function () {
	    //_.bindAll(this, "render", "register", "registerSuccess", "login");
	    //this.model.bind("change", this.render);

		this.$("[name=displayName]").val(localStorage.getItem("displayName"));
		this.$("[name=email]").val(localStorage.getItem("email"));

		this.$el = $(this.el);
		this.$errorMsg = this.$el.find("form b[data-error=summary]");
		this.$signup = $(this.el).find("#signup");
		this.$registerLogin = $(this.el).find("#register-login");

		this.$signup.find("form").submit(this.register);
		this.$registerLogin.find("form").submit(this.login);
	},
	register: function(e) {
		if (e) e.preventDefault();

		var form = this.$signup.find("form");
		_.post({
			form: form,
			url: form.attr("action"),
			data: _.formData(form),
			success: this.registerSuccess
		});

		this.$registerLogin.find("INPUT[name=userName]").val(form.find("INPUT[name=email]").val());

		localStorage.setItem("displayName", this.$("[name=displayName]").val());
		localStorage.setItem("email", this.$("[name=email]").val());
    },
	registerSuccess: function(r) {
		//this.model.set({ hasRegistered: true, userId: r.userId, isAuthenticated: !!r.sessionId });
	},
	login: function(e) {
	    if (e) e.preventDefault();
	    //this.model.login(this.$registerLogin.find("form"));
	},
	signIn: function(e) {
		//this.model.set({ hasRegistered: true });
	},
    unregistered: function(e) {    
        //this.model.set({ hasRegistered: false });
    },
    render: function() {
		this.$errorMsg.html("");
		//$("BODY").toggleClass("registered", this.model.get('hasRegistered'));
	}
});
