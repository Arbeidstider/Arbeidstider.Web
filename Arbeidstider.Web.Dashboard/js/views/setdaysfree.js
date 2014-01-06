define(['underscore',
        'marionette',
        'text!templates/setdaysfree.html'
], function (_, Marionette, SetDaysFreeTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(SetDaysFreeTemplate),
        initialize: function () { console.log("SetDaysFreeView.initialize");},
    });
});