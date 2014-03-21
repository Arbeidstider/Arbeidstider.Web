define(['backbone', 'settings'
    ], function(Backbone, Settings) {
        var BaseModel = Backbone.Model.extend({
            initialize: function () {
            },
            getUrl: function (action) {
                return Settings.ServiceUrl(action);
            },
            post: function (action, data, successCb) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: data,
                    contentType: "application/json",
                    url: getUrl(action),
                    error: function (jqXHR, textStatus, errorThrown) { },
                    success: successCb,
                    beforeSend: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); }
                });
            }
        });
        
        return BaseModel;
    });