define(['marionette',
        'text!templates/changeworkday.html'
], function (Marionette, ChangeWorkDayTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(ChangeWorkDayTemplate),
        initialize: function () { console.log("ChangeWorkDayView.initialize");},
    });
});