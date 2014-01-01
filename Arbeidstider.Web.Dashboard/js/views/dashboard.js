define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'views/base',
        'views/header',
        'views/myworkinghours',
        'views/sidebar',
        'text!templates/dashboard.html',
    ], function ($, _, Backbone, Vm, BaseView, HeaderMenuView, MyWorkingHoursView, SidebarView, DashboardTemplate) {
        // Subview of appview
        var DashboardView = BaseView.extend({
            template: _.template(DashboardTemplate),
            el: "#view-dashboard",
            initialize: function(options) {
                _.bindAll(this, "render");
                console.log("Dashboard initialize");
                if (options.session) {
                    this.session = options.session;
                }
            },
            render: function () {
                console.log("DashboardView.render()");
                $("body").attr("class", "");
                $("body").attr("class", "contrast-dark fixed-header fixed-navigation");
                $(this.el).html(this.template());
                var headerMenuView = Vm.reuseView('HeaderMenuView', function () { return new HeaderMenuView(); });
                var sideBarView = Vm.reuseView('SidebarView', function () { return new SidebarView(); });
                var myWorkingHoursView = Vm.reuseView('MyWorkingHoursView', function () { return new MyWorkingHoursView(); }); 

                this.assign({
                    "#view-header-menu" : headerMenuView,
                    "#view-sidebar" : sideBarView,
                    "#view-my-working-hours" : myWorkingHoursView
                });
                
                return this.el;
            },
            signOut: function () {
                this.session.signOut();
            }
        });
        
        return DashboardView;
    });