define(['underscore',
        'marionette',
        'text!templates/profile.html'
], function (_, Marionette, ProfileTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(ProfileTemplate),
        initialize: function () { console.log("ProfileView.initialize");},
    });
});