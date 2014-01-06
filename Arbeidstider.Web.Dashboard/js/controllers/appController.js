define(['underscore',
        'marionette',
        'helpers/mixins',
        'libs/vm/vm',
        'models/settings',
        'collections/timesheets',
        'views/calendar',
        'views/calendaritem',
        'views/register',
        'views/changeworkday',
        'views/setdaysfree',
        'views/confirmavailabledays',
        'views/profile',
        'views/addressbookitem',
        'views/addressbook',
        'views/messages'
],
function (_, Marionette, Mixins, Vm, Settings, TimesheetCollection, CalendarView, CalendarItemView, RegisterView, ChangeWorkDayView, SetDaysFreeView,  ConfirmAvailableDaysView, ProfileView,AddressBookItemView,  AddressBookView,  MessagesView) {
    var AppController = Backbone.Marionette.Controller.extend({
        initialize: function (options) {
            _.bindAll(this, "dashboard", "changeView");
            console.log("appController.initialize();");
        },
        addressbook: function () {
            var view = new AddressBookView({ itemView: AddressBookItemView });
            this.changeView(view);
        },
        dashboard: function () {
            var view = new CalendarView({ itemView: CalendarItemView, collection: new TimesheetCollection() });
            this.changeView(view);
        },
        profile: function () {
            this.changeView(new ProfileView());
        },
        index: function () {
            this.dashboard();
        },
        register: function () {
            console.log("register");
            this.changeView(new RegisterView());
        },
        logout: function() {
            var Store = require("store");
            Store.remove("AuthSession");
            Store.set("AuthSession", null);
            window.location.replace("/login");
        },
        messages: function () {
            this.changeView(new MessagesView());
        },
        changeWorkDay: function() {
            this.changeView(new ChangeWorkDayView());
        },
        setDaysFree: function() {
            this.changeView(new SetDaysFreeView());
        },
        confirmAvailableDays: function() {
            this.changeView(new ConfirmAvailableDaysView());
        },
        changeView: function (view) {
            var App = require('app');
            App.layout.main.show(view);
        }
    });
    return AppController;
});