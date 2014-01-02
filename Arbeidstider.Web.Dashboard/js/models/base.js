define([
        'jquery',
        'underscore',
        'backbone',
    ], function($, _, Backbone) {
        var BaseModel = Backbone.Model.extend({
            BaseUrl: "http://localhost:8181",
            ServiceUrl: function (path) { return this.BaseUrl + path; },
            
            initialize: function () {
                _.bindAll(this, "serviceUrl");
                console.log("BaseModel...");
            },
        });
        
        return BaseModel;
    });