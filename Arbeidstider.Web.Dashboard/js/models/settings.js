define([
        'underscore',
        'models/base'
], function (_, BaseModel) {
    var Settings = BaseModel.extend({
        //useRemoteService: false,
        initialize: function () {
            _.bindAll(this, "ServiceUrl", "ViewSelectors", "GetViewSelector");
            // set baseurl depending on mine.arbeidstider.no/locahost
            console.log("settings.initialize()");
        },
        ServiceUrl: function (action) {
            var baseUrl;
            if (document.domain.indexOf("localhost") > -1)
                baseUrl = "http://localhost:8181";
            else
                baseUrl = "http://services.arbeidstider.no";
                
            return baseUrl + action;
        },
        GetViewSelector: function (View) {
            var viewSelector = _.find(this.ViewSelectors, function (obj) { return obj.View == View; });
            return viewSelector.Selector;
        },
        ViewSelectors: function () {
            return [{
                        View: "Dashboard",
                        Selector: "#view-dashboard"
                    },
                    {
                        View: "MyWeeklyTimesheet",
                        Selector: "#view-my-weekly-timesheet"
                    }];
        }
    });

    return new Settings();
});;
