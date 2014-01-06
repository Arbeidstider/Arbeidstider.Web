define(['jquery',
        'backbone',
        'marionette',
        'text!templates/calendaritem.html'
    ],
    function($,Backbone, Marionette, CalendarItemTemplate) {
        return Backbone.Marionette.ItemView.extend({
            tagName: "div",
            className: "calendar-item",
            template: _.template(CalendarItemTemplate),
            initialize: function() {
                console.log("Calendear item initialize");
            }
        });
    });