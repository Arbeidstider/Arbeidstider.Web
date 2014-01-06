define(['jquery',
        'underscore',
        'backbone',
        'marionette',
], function ($, _, Backbone, Marionette) {
    return Backbone.Marionette.ItemView.extend({
        events: {
            'ajaxError': 'handleAjaxError'
        },
        handleAjaxError: function(event, request, settings, thrownError) {
        }
    });