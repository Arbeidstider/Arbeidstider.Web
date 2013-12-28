define([
  'jquery',
  'underscore',
  'backbone',
  'views/base',
  'text!templates/header.html'
], function ($, _, Backbone, BaseView, headerMenuTemplate) {
    var HeaderMenuView = BaseView.extend({
        template: _.template(headerMenuTemplate),
        el: "#view-header-menu",
        events: {
        },
        initialize: function () {
            console.log("HeaderMenuView.initialize()");
        },
        render: function() {
            $(this.el).html(this.template());
            return this.el;
        },
    });
    
    return HeaderMenuView;
});
