define(["marionette",
        "collections/calendardays",
        "views/calendar",
        "globals",
        "text!templates/content.html"],
    function (Marionette, CalendarDayCollection, CalendarView, Globals, ContentTemplate) {
        var ContentLayout = Backbone.Marionette.Layout.extend({
            template: _.template(ContentTemplate),
            regions: {
                pageHeader: ".page-header",
                mainColumn: "#main-column"
            },
            initialize: function () {
            },
            showCalendar: function () {
                console.log("bootstrappedCalendar: " + Globals.bootstrappedCalendar);
                // convert to model
                var collection = new CalendarDayCollection();
                var view = new CalendarView({ collection: collection });
                this.mainColumn.show(view);
                collection.reset(Globals.bootstrappedCalendar || []);
            }
        });

        return ContentLayout;
    });