define([
    'jquery',
    'underscore',
    'marionette',
    'nav',
    'text!templates/navbar.html'
    ], function ($, _, Marionette, Nav, NavbarTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(NavbarTemplate), 
        initialize: function () {
            $(document).ready(function () {
                $("a.navigation-link").click(function (e) {
                    e.preventDefault();
                    var target = $(this).data("target");
                    Backbone.history.navigate(target, { trigger: true });
                });

            });
        },
        /*
        render: function() {
            $(this.el).html();
            return this.el;
        },
        */
    });
});
