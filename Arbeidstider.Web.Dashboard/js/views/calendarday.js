define(['underscore',
        'marionette',
        'collections/shifts',
        'views/calendardayitem',
        'text!templates/calendarday.html'
],
    function (_, Marionette, ShiftCollection, CalendarDayItemView, CalendarDayTemplate) {
        var CalendarDayView = Backbone.Marionette.CompositeView.extend({
            template: _.template(CalendarDayTemplate),
            itemView: CalendarDayItemView,
            el: "div.calendar-day",
            itemViewContainer: "div.calendar-day",
            initialize: function () {
                _.bindAll(this, "render");
                //console.log("this.model: " + this.model);
                //console.log("this.model: " + JSON.stringify(this.model));
                //TODO Convert to shiftcollection
                this.collection = new ShiftCollection(this.model.get("Shifts"));
                //console.log("timesheets: " + timesheets);
            }
        });

        return CalendarDayView;
    });