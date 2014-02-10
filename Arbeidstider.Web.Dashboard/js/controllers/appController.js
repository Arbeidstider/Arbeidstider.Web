define(['underscore',
        'marionette',
        'views/register',
        'views/changeworkday',
        'views/setdaysfree',
        'views/confirmavailabledays',
        'views/profile',
        'views/addressbook',
        'views/messages'
],
function (_, Marionette, RegisterView, ChangeWorkDayView, SetDaysFreeView, ConfirmAvailableDaysView, ProfileView, AddressBookView, MessagesView) {
    var AppController = Backbone.Marionette.Controller.extend({
        initialize: function () {
            _.bindAll(this, "changeView");
            console.log("appController.initialize();");
        },
        addressbook: function () {
            this.changeView(AddressBookView);
        },
        index: function () {
            console.log("index");
        },
        profile: function () {
            this.changeView(ProfileView);
        },
        register: function () {
            this.changeView(RegisterView);
        },
        logout: function () {
            var Store = require("store");
            Store.remove("AuthSession");
            Store.set("AuthSession", null);
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
        changeView: function (view) {
            var App = require('app');
            var Store = require('store');
            var session = Store.get("AuthSession");
            var newView = new view({ session: session });
            App.layout.main.show(newView);
        }
    });
    return AppController;
});