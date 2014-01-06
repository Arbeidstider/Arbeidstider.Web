define([
  'jquery',
  'underscore',
  'backbone',
  'marionette',
  'text!templates/header.html',
], function ($, _, Backbone, Marionette, headerMenuTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(headerMenuTemplate),
        events: {
        },
        initialize: function () {
            console.log("HeaderMenuView.initialize()");
            $(document).ready(function () {
                $("a.dropdown-menu-link").click(function (e) {
                    e.preventDefault();
                    var target = $(this).data("target");
                    Backbone.history.navigate(target, { trigger: true });
                });
                $("a.brand").click(function (e) {
                    e.preventDefault();
                    Backbone.history.navigate("", { trigger: true });
                });
            });
        },
    });
});
