define([
    'jquery',
    'underscore',
    'marionette',
    'nav',
    'text!templates/shared/navbar.html'
    ], function ($, _, Marionette, Nav, NavbarTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(NavbarTemplate), 
        initialize: function () {
        },
        /*
        render: function() {
            $(this.el).html();
            return this.el;
        },
        */
    });
});
