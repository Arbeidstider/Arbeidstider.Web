define([
    'jquery',
    'underscore',
    'backbone',
    'views/base',
    'text!templates/sidebar.html'
], function ($, _, Backbone, BaseView, SidebarTemplate) {
    var SidebarView = BaseView.extend({
        template: _.template(SidebarTemplate), 
        el: '#view-sidebar',
        defaults:
        {
            model: {
                "#switch-shift-with-colleague": "Bytt vakt med kollega",
            }
        },
        initialize: function() {
        },
        render: function() {
            $(this.el).html(this.template());
            return this.el;
        },
    });

    return SidebarView;
});
