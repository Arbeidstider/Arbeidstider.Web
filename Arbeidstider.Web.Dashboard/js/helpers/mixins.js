define(["jquery", "underscore", "models/settings", "store"],
    function($, _, Settings, Store) {
        _.mixin(require('underscore.deferred'));
        
        //_.mixin({
        //    formData: function (form) {
        //        var ret = {};
        //        $(form).find("INPUT,TEXTAREA").each(function() {
        //            if (this.type == "button" || this.type == "submit") return;
        //            if (this.type == "checkbox") {
        //                if (!this.checked) return;
        //                ret[this.name] = this.value === "on" ? true : this.value;
        //                return;
        //            }
        //            ret[this.name] = $(this).val();
        //        });
        //        return ret;
        //    },
        //    get: function (url, data, success, beforeSend) {
        //        return _.ajax({
        //            type: 'GET',
        //            url: url,
        //            data: data,
        //            success: success,
        //            beforeSend: beforeSend
        //        });
        //    },
        //    post: function (opt) {
        //        return _.ajax(opt);
        //    },
        //    ajax: function (opt) {
        //        var o = _.defaults(opt, {
        //            // Set session Id on all requests unless overriden
        //            beforeSend: _.setSessionId,
        //            type: 'POST',
        //            loading: function() {
        //                $(this.el).css({ opacity: 0.5 });
        //            },
        //            finishedLoading: function() {
        //                $(this.el).css({ opacity: 1 });
        //            },
        //            dataType: "json"
        //        });
        //        o.loading();
        //        $.ajax({
        //            type: o.type,
        //            url: o.url,
        //            beforeSend: o.beforeSend,
        //            data: o.data,
        //            success: function() {
        //                o.finishedLoading();
        //                if (o.success) o.success.apply(null, arguments)
        //            },
        //            error: function(xhr, err, status) {
        //                o.finishedLoading();
        //                try {
        //                    var r = JSON.parse(xhr.responseText);
        //                    var errStatus = (r && r.responseStatus);
        //                    (o.error || _.error).call(null, errStatus);
        //                    return;
        //                } catch(e) {
        //                    (o.error || _.error).apply(null, arguments);
        //                }
        //            },
        //            dataType: o.dataType || "json"
        //        });
        //    },
        //    error: function(xhr, err, statusText) {
        //        console.log("error: " + xhr + err + statusText);
        //    },
        //    setSessionId: function (xhr) {
        //        var authSession = Store.get("AuthSession");
        //        console.log("setSessionId.authSession: " + JSON.stringify(authSession.SessionId));
        //        if (!authSession && !authSession.SessionId) return false;
        //        var sessionId = authSession.SessionId;
        //        console.log("setSessionId.sessionid" + authSession.SessionId);
        //        xhr.setRequestHeader("Session-Id", sessionId);
        //    },
        //});
    });