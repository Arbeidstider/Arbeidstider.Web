define([
        'jquery',
        'underscore',
        'backbone',
        'truStorage'
    ], function($, _, Backbone, truStorage) {
        var BaseModel = Backbone.Model.extend({
            BaseUrl: "http://localhost:8181",
            ServiceUrl: function (path) { return this.BaseUrl + path; },
            
            initialize: function () {
                _.bindAll(this, "ajaxRequest", "serviceUrl", "setSessionId");
                console.log("BaseModel...");
            },
            getData: function () { },
            postData: function() { } ,
            ajaxRequest: function (type, path, json, successCb, errorCb, async, processData, beforeSend) {
                $.support.cors = true;
                
                $.ajax({
                    type: type,
                    url: this.ServiceUrl(path),
                    data: json,
                    processData: processData,
                    async: async,
                    error: errorCb,
                    success: successCb,
                    beforeSend:
                        beforeSend ?
                        beforeSend: this.setSessionId,
                    contentType: "application/json",
                    dataType: "json",
                });
            },
            setSessionId: function (xhr) {
                console.log("setSessionId");
                var authSession = truStorage.getItem("AuthSession");
                console.log("authSession, setSessionId: " + authSession);
                console.log("authSession, setSessionId: " + authSession.SessionId);
                if (authSession == "{}") return;
                var sessionId = authSession.SessionId;
                xhr.setRequestHeader("Session-Id", sessionId);
            }
        });
        
        return BaseModel;
    });