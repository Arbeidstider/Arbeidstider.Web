define(['marionette',
  'text!templates/messages.html',
], function (Marionette, MessagesTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(MessagesTemplate),
        initialize: function () {
            console.log("MessagesView.initialize()");
        },
    });
});
