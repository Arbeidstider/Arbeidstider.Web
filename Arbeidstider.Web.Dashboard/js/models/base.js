define(['app', 'backbone'
    ], function(App, Backbone) {
        var BaseModel = Backbone.Model.extend({
            initialize: function () {
                //_.bindAll(this, "post", "getUrl");
            },
            setRequestHeader: function (xhr) { xhr.setRequestHeader("Session-Id", $.cookie("sessionId")); },
            getUrl: function (relativeUrl) {
                relativeUrl = relativeUrl.charAt(0) == "/" ? relativeUrl : ("/" + relativeUrl);
                var absoluteUrl = App.API + relativeUrl;
                console.log("absoluteUrl: " + absoluteUrl);
                return absoluteUrl;
            },
            post: function (action, data, callback) {
                var self = this;
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    data: data,
                    contentType: "application/json",
                    url: self.getUrl(action),
                    error: function (jqXHR, textStatus, errorThrown) { if ("error" in callback) callback.error(); },
                    success: function (response) { callback.success(response); },
                    beforeSend: self.setRequestHeader 
                }).complete(function() {
                    if ("complete" in callback) callback.complete();
                });
            }
        });
        
        return BaseModel;
    });