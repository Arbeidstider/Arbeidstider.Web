define(['underscore',
        'marionette',
        'helpers/mixins',
        'models/settings',
        'collections/timesheets',
        'views/mytimesheets',
        'views/register',
        'views/changeworkday',
        'views/setdaysfree',
        'views/confirmavailabledays',
        'views/profile',
        'views/addressbookitem',
        'views/addressbook',
        'views/messages'
],
function (_, Marionette, Mixins, Settings, TimesheetCollection, MyTimesheetsView, RegisterView, ChangeWorkDayView, SetDaysFreeView,  ConfirmAvailableDaysView, ProfileView,AddressBookItemView,  AddressBookView,  MessagesView) {
    var AppController = Backbone.Marionette.Controller.extend({
        initialize: function (options) {
            _.bindAll(this, "dashboard", "changeView");
            console.log("appController.initialize();");
        },
        addressbook: function () {
            this.changeView(AddressBookView);
        },
        dashboard: function () {
            this.changeView(MyTimesheetsView);
        },
        profile: function () {
            this.changeView(ProfileView);
        },
        index: function () {
            this.dashboard();
        },
        register: function () {
            this.changeView(RegisterView);
        },
        logout: function() {
            var Store = require("store");
            Store.remove("AuthSession");
            Store.set("AuthSession", null);
            window.location.replace("/login");
        },
        messages: function () {
            this.changeView(MessagesView);
        },
        changeWorkDay: function() {
            this.changeView(ChangeWorkDayView);
        },
        setDaysFree: function() {
            this.changeView(SetDaysFreeView);
        },
        confirmAvailableDays: function() {
            this.changeView(ConfirmAvailableDaysView);
        },
        changeView: function(view) {
            var App = require('app');
            var Store = require('store');
            var newView = new view({ session: Store.get("AuthSession") });
            App.layout.main.show(newView);
        }
    });
    return AppController;
});