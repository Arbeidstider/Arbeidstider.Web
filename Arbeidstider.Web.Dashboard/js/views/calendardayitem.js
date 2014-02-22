define(['underscore',
        'marionette',
        'text!templates/calendardayitem.html'
], function (_, Marionette, CalendarDayItemTemplate) {
    var CalendarDayItemView = Backbone.Marionette.ItemView.extend({
        template: _.template(CalendarDayItemTemplate),
        el: "div.timesheet-shift",
        initialize: function (options) {
            console.log("CalendarDayItemView initialize");
        },
    });
    return CalendarDayItemView;
});