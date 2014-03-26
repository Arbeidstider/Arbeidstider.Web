define([
  'jquery',
  'underscore',
  'marionette',
    'models/header',
  'text!templates/shared/header.html',
], function ($, _, Marionette, HeaderModel, headerMenuTemplate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(headerMenuTemplate),
        events: {
        },
        initialize: function () {
            this.model = new HeaderModel();
            console.log("HeaderMenuView.initialize()");
            $(document).ready(function () {
            });
        },
    });
});
