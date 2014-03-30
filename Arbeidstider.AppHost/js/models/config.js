define([
        'underscore',
        'models/base'
], function (_, BaseModel) {
    var config = BaseModel.extend({
        //useRemoteService: false,
        initialize: function () {
            _.bindAll(this, "serviceUrl", "container", "sessionKey");
            // set baseurl depending on mine.arbeidstider.no/locahost
            console.log("config.initialize()");
        },
        container: function() {
            var App = require('app');
            return App.layout.main;
        },
        sessionKey: "AuthSession",
        serviceUrl: function (action) {
            var baseUrl;
            if (document.domain.indexOf("localhost") > -1)
                baseUrl = "http://localhost:8181";
            else
                baseUrl = "http://services.arbeidstider.no";
                
            return baseUrl + action;
        },
    });

    return new Config();
});;
