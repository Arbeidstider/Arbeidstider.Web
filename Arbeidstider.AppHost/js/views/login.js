define(['app',
        'jquery',
        'jquery.cookie',
        'underscore',
        'backbone',
        'marionette',
        'text!templates/login.html',
], function (App, $, jquerycookie, _, Backbone, Marionette, LoginTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(LoginTemplate),
        el: 'body',
        events: {
            "click .btn-block": "signIn"
        },
        initialize: function (options) {
            _.bindAll(this, "signIn", "render");
            var self = this;
            this.session = App.session;
            if (!$("body").hasClass("sign-in")) {
                $("body").attr("class", "");
                $("body").attr("class", "contrast-red sign-in contrast-background sign-in");
            }
        },
        signIn: function (e) {
            e.preventDefault();
            var authInfo = {
                UserName: $("#UserName").val(),
                Password: $("#Password").val(),
                RememberMe: Boolean($("#RememberMe").is(":checked"))
            };
            var self = this;
            self.session.doAuth(authInfo, {
                complete: function () {
                    if (self.session.get("loggedIn")) {
                        console.log("logged in, switching view");
                        $("body").fadeOut("slow", function() {
                            $("body").attr("class", "contrast-red sign-in contrast-background");
                            App.body.reset();
                            App.router.navigate("", { trigger: true, replace: false });
                        });
                    }
                }
            });
        }
    })
});