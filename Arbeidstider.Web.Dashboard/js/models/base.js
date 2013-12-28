define([
        'jquery',
        'underscore',
        'backbone'
    ], function($, _, Backbone) {
        var BaseModel = Backbone.Model.extend({
            baseUrl: "http://localhost:8181",
            initialize: function () {
                _.bindAll(this, "ajaxRequest", "serviceUrl");
                console.log("BaseModel...");
            },
            serviceUrl: function (path) { return this.baseUrl + path; },
            ajaxRequest: function (type, path, json, successCb, errorCb) {
                console.log("BaseModel.ajaxRequest() data: " + json);
                $.ajax({
                  type: type,
                  url: this.serviceUrl(path),
                  data: json,
                  error: errorCb,
                  success: successCb,
                });
            }
        });
        
        return BaseModel;
    });