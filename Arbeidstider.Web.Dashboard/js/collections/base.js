define([
        'jquery',
        'underscore',
        'backbone',
    ], function($, _, Backbone) {
        var BaseCollection = Backbone.Collection.extend({
            initialize: function () {
            },
        });
        
        return BaseCollection;
    });