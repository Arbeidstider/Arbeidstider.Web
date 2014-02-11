define(["underscore",
        "marionette",
        "text!templates/headers/calendar.html"],
    function (_, Marionette, CalendarHeaderTemplate) {
        var CalendarHeaderView = Backbone.Marionette.ItemView.extend({
            template: _.template(CalendarHeaderTemplate)
        });

        return CalendarHeaderView;
    });
