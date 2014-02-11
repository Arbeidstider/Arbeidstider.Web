define(['underscore',
        'marionette',
        'globals',
        'collections/calendardays',
        'views/calendar',
        'views/register',
        'views/changeworkday',
        'views/setdaysfree',
        'views/confirmavailabledays',
        'views/profile',
        'views/addressbook',
        'views/messages'
],
function (_, Marionette, Globals, CalendarDayCollection, CalendarView, RegisterView, ChangeWorkDayView, SetDaysFreeView, ConfirmAvailableDaysView, ProfileView, AddressBookView, MessagesView) {
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
        changeView: function (view, collection, pageHeader) {
            var newView = collection ? new view({ collection: collection }) : new view();
            var App = require(['App']);
            App.contentLayout.mainColumn.show(newView);
        }
    });
    return AppController;
});