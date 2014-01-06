define([
        'jquery',
        'underscore',
        'backbone',
    ], function($, _, Backbone) {
        var BaseModel = Backbone.Model.extend({
            initialize: function () {
            },
        });
        
        return BaseModel;
    });