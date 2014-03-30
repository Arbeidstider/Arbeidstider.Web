define(['marionette',
        'text!templates/confirmavailabledays.html'
], function (Marionette, ConfirmAvailableDaysTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(ConfirmAvailableDaysTemplate),
        initialize: function () { console.log("ConfirmAvailableDaysView.initialize");},
    });
});