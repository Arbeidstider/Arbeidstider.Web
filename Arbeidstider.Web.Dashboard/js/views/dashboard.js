define(['app',
        'marionette',
        'models/dashboard',
        'text!templates/dashboard.html'
], function (App, Marionette, DashboardModel, DashboardTemplate) {
    var DashboardView = Backbone.Marionette.ItemView.extend({
        template: _.template(DashboardTemplate),
        initialize: function () {
            this.model = new DashboardModel({});
            this.model.bind("sync", this.render, this);
            console.log("views/dashboard initialize");
        },
    });

    return DashboardView;
});