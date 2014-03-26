define([], 
    function () {
    var Settings = {
        initialize: function () { },
        ServiceUrl: function (action) {
            action = action.substring(0, action.length) == "/" ? action.substring(1, action.length) : action;
            var baseUrl;
            if (document.domain.indexOf("localhost") > -1)
                baseUrl = "http://localhost:8181/";
            else
                baseUrl = "http://services.arbeidstider.no/";
                
            return baseUrl + action;
        }
    };

    return Settings;
});
