define([
        'jquery',
        'underscore',
        'backbone',
        'vm',
        'views/base',
        'views/header',
        'models/myweeklytimesheet',
        'views/myweeklytimesheet',
        'views/sidebar',
        'collections/timesheets',
        'text!templates/dashboard.html',
    ], function ($, _, Backbone, Vm, BaseView, HeaderMenuView, MyWeeklyTimesheetModel, MyWeeklyTimesheetView, SidebarView, TimesheetCollection, DashboardTemplate) {
        // Subview of appview
        var DashboardView = BaseView.extend({
            template: _.template(DashboardTemplate),
            el: "#view-dashboard",
            initialize: function() {
                console.log("Dashboard initialize");
                _.bindAll(this, "render");
                this.cachedCollections = {
                    timesheets: undefined
                };
                this.cachedModels = {
                    myWeeklyTimesheet: undefined
                };
                var params = {
                    StartDate: "2013-12-01",
                    EndDate: "2014-01-05",
                    UserID: "5"
                };
                this.cachedModels.myWeeklyTimesheet = this.cachedModels.myWeeklyTimesheet || new MyWeeklyTimesheetModel(params);
                this.models = this.models || {};
                var myweeklytimesheet = this.models.myweeklytimesheet = this.cachedModels.myWeeklyTimesheet;
                
                this.cachedCollections.timesheets = this.cachedCollections.timesheets || new TimesheetCollection(params);
                this.collections = this.collections || {};
                var timesheetCollection = this.collections.timesheets = this.cachedCollections.timesheets;
                
                this.headerMenuView = Vm.reuseView('HeaderMenuView', function () { return new HeaderMenuView(); });
                this.sideBarView = Vm.reuseView('SidebarView', function () { return new SidebarView(); });
                this.myWeeklyTimesheetView = Vm.reuseView('MyWeeklyTimesheet', function() {
                     return new MyWeeklyTimesheetView({model: myweeklytimesheet, collection: timesheetCollection});
                }); 
            },
            render: function() {
                console.log("DashboardView.render()");
                $("body").attr("class", "");
                $("body").attr("class", "contrast-dark fixed-header fixed-navigation");
                $(this.el).html(this.template());
                this.collections.timesheets.url = this.models.myweeklytimesheet.url;
                var params = {
                    StartDate: "2013-12-01",
                    EndDate: "2014-01-05",
                    UserID: "5"
                };
                this.collections.timesheets.fetch({ data: params });

                this.assignSelectorsToViewAndRender({
                    "#view-header-menu" : this.headerMenuView,
                    "#view-sidebar" : this.sideBarView,
                    "#view-my-weekly-timesheet" : this.myWeeklyTimesheetView
                });
                
                return this.el;
            },
            signOut: function () {
                Backbone.history.navigate("logout", { trigger: true });
            }
        });
        
        return DashboardView;
    });