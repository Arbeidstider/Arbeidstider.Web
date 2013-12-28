define([
        'jquery',
        'underscore',
        'backbone',
        'views/base',
        'text!templates/my_working_hours.html'
    ], function($, _, Backbone, BaseView, MyWorkingHoursTemplate) {
        var MyWorkingHoursView = BaseView.extend({
            template: _.template(MyWorkingHoursTemplate),
            el: '#view-my-working-hours',
            initialize: function () {
            },
            render: function () {
                $(this.el).html(this.template());
                return this.el;
            }
        });

        return MyWorkingHoursView;
    });