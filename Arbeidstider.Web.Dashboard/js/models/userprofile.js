define([
        'models/base'
    ], function(BaseModel) {
        var UserProfile = BaseModel.extend({
            url: "api/profile",
            defaults: {
                id: null,
                email: null,
                userName: null,
                displayName: null,
                twitterUserId: null,
                twitterScreenName: null,
                twitterName: null,
                facebookName: null,
                facebookFirstName: null,
                facebookLastName: null,
                facebookUserId: null,
                facebookUserName: null,
                facebookEmail: null,
                googleUserId: null,
                googleFullName: null,
                googleEmail: null,
                yahooUserId: null,
                yahooFullName: null,
                yahooEmail: null,
                gravatarImageUrl64: null,
                showProfile: null
            },
            initialize: function (opt) {
                console.log("UserProfile.initialize()");
            },
        });
    });
