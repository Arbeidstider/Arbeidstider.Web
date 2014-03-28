define([
  'jquery',
  'underscore',
  'marionette',
    'models/header',
  'text!templates/shared/header.html',
], function ($, _, Marionette, HeaderModel, headerMenuTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(headerMenuTemplate),
        initialize: function () {
            this.model = new HeaderModel();
            if (DEBUG) console.log("views/shared/header initialize()");
        },
    });
});
