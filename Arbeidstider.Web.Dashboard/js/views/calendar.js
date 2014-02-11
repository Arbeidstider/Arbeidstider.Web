define(['underscore',
        'marionette',
        'views/calendarday',
        'text!templates/calendar.html'
], function (_, Marionette, CalendarDayView, CalendarTemplate) {
    var CalendarView = Backbone.Marionette.CollectionView.extend({
        el: "#calendar-items",
        template: _.template(CalendarTemplate),
        itemView: CalendarDayView,
        initialize: function () {
            console.log("Calendar initialize");
            _.bindAll(this, "render");
            this.collection.on("reset", this.render, this);
        },
        //appendHtml: function (collectionView, itemView, index) {
        //    if (collectionView.isBuffering) {
        //        // buffering happens on reset events and initial renders
        //        // in order to reduce the number of inserts into the
        //        // document, which are expensive.
        //        collectionView.elBuffer.appendChild(itemView.el);
        //    }
        //    else {
        //        // If we've already rendered the main collection, just
        //        // append the new items directly into the element.
        //        collectionView.$el.append(itemView.el);
        //    }
        //},
    });

    return CalendarView;
});