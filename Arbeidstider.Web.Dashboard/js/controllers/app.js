define(['underscore',
        'marionette',
        'app',
        'globals',
        'views/changeworkday',
        'views/setdaysfree',
        'views/confirmavailabledays',
        'views/profile',
        'views/addressbook',
        'views/messages'
],
function (_, Marionette, App, Globals, ChangeWorkDayView, SetDaysFreeView, ConfirmAvailableDaysView, ProfileView, AddressBookView, MessagesView) {
    var AppController = Backbone.Marionette.Controller.extend({
        initialize: function () {
            _.bindAll(this, "changeView");
        },
        addressbook: function () {
            this.changeView(AddressBookView);
        },
        index: function () {
        },
        profile: function () {
            this.changeView(ProfileView);
        },
        register: function () {
            console.log("register");
            App.contentLayout.showRegister();
        },
        logout: function () {
            $.removeCookie("sessionId");
            $.removeCookie("userId");
            $.removeCookie("employeeId");
            $.removeCookie("userName");
            $.get("/auth/logout");
            window.location.replace("/login");
        },
        messages: function () {
            this.changeView(MessagesView);
        },
        changeWorkDay: function () {
            this.changeView(ChangeWorkDayView);
        },
        setDaysFree: function () {
            this.changeView(SetDaysFreeView);
        },
        confirmAvailableDays: function () {
            this.changeView(ConfirmAvailableDaysView);
        },
        changeView: function (view, collection, pageHeader) {
        }
    });
    return AppController;
});