define(['underscore',
        'marionette',
        'helpers/mixins',
        'settings',
        'text!templates/profile.html'
], function (_, Marionette, mixins, Settings, ProfileTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(ProfileTemplate),
        initialize: function () { console.log("ProfileView.initialize");},
    });
});