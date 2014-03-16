define(['backbone', 'settings'
    ], function(Backbone, Settings) {
        var BaseModel = Backbone.Model.extend({
            initialize: function () {
            },
            getUrl: function (action) {
                return Settings.ServiceUrl(action);
            }
        });
        
        return BaseModel;
    });