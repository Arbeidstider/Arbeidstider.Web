define(['underscore',
        'marionette',
        'views/calendardayitem',
        'text!templates/calendarday.html'
],
    function (_, Marionette, CalendarDayItemView, CalendarDayTemplate) {
        var CalendarDayView = Backbone.Marionette.CompositeView.extend({
            template: _.template(CalendarDayTemplate),
            itemView: CalendarDayItemView,
            itemViewContainer: "div.calendar-days",
            initialize: function () {
                _.bindAll(this, "render");
                //console.log("this.model: " + this.model);
                //TODO Convert to shiftcollection
                this.collection = this.model.get("Shifts");
                //console.log("timesheets: " + timesheets);
                console.log("this.collection: " + this.collection);
            }
        });

        return CalendarDayView;
    });