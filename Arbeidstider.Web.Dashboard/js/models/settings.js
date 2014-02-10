define([
        'underscore',
        'models/base'
], function (_, BaseModel) {
    var Settings = BaseModel.extend({
        //useRemoteService: false,
        initialize: function () {
            _.bindAll(this, "ServiceUrl");
        },
        ServiceUrl: function (action) {
            var baseUrl;
            if (document.domain.indexOf("localhost") > -1)
                baseUrl = "http://localhost:8181";
            else
                baseUrl = "http://services.arbeidstider.no";
                
            return baseUrl + action;
        }
    });

    return new Settings();
});;
