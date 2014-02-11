define(['underscore',
        'marionette',
        'text!templates/calendardayitem.html'
], function (_, Marionette, CalendarDayItemTemplate) {
    var CalendarDayItemView = Backbone.Marionette.ItemView.extend({
        template: _.template(CalendarDayItemTemplate),
        tagName: "div",
        className: "timesheet-shift",
        initialize: function (options) {
            console.log("CalendarDayItemView this.model: " + this.model);
            console.log("CalendarDayItemView initialize");
        },
    });
    return CalendarDayItemView;
});