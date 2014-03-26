define(['app',
        'backbone',
        'nav',
        'layouts/app',
        'layouts/content',
        'views/addressbook',
        'views/changeworkday',
        'views/confirmavailabledays',
        'views/messages',
        'views/setdaysfree',
        'views/dashboard',
        'views/profile',
        'views/register',
        "views/shared/header",
        "views/shared/navbar"
],
    function (App, Backbone, Nav, AppLayout, ContentLayout, AddressbookView, ChangeworkdayView, ConfirmavailabledaysView, MessagesView, SetdaysfreeView, DashboardView, ProfileView, RegisterView, HeaderView, NavbarView) {
        return Backbone.Router.extend({
            //"index" must be a method in AppRouter's controller
            initialize: function () {
                _.bindAll(this, "show");
            },
            routes: {
                /* Route Name: Function */
                "": "index",
                "logout": "logout",
                "signout": "logout",
                "logoff": "logout",
                "signoff": "logout",
                "dashboard": "index",
                'addemployee': 'register',
                "profile": "profile",
                "settings": "profile",
                "changeWorkDay": "changeWorkDay",
                "setDaysFree": "setDaysFree",
                "confirmAvailableDays": "confirmAvailableDays",
                "addressbook": "addressbook",
                "messages": "messages"
            },
            changeWorkDay: function () {
                this.show(new ChangeworkdayView());
            },
            setDaysFree: function () {
                this.show(new SetdaysfreeView());
            },
            confirmAvailableDays: function () {
                this.show(new ConfirmavailabledaysView());
            },
            addressbook: function () {
                this.show(new AddressbookView());
            },
            messages: function () {
                this.show(new MessagesView());
            },
            profile: function () {
                this.show(new ProfileView());
            },
            register: function () {
                this.show(new RegisterView());
            },
            show: function (view, options) {
                /* The main layout inside #content-app, layouts/app.js, contains navbar/header and content-wrapper */
                if (!this.appLayout) {
                    this.appLayout = new AppLayout();
                    App.content.show(this.appLayout);
                    this.appLayout.render();
                }

                // Every page view in the router should need a header.
                // Instead of creating a base parent view, just assign the view to this
                // so we can create it if it doesn't yet exist
                if (!this.headerView) {
                    this.headerView = new HeaderView({});
                    this.appLayout.header.show(this.headerView);
                }

                if (!this.navbarView) {
                    this.navbarView = new NavbarView();
                    this.appLayout.navbar.show(this.navbarView);
                    Nav.initialize();
                }

                // Close and unbind any existing page view
                if (this.currentView) this.currentView.close();

                // Establish the requested view into scope
                this.currentView = view;

                /* Contains main-content and responsible for swapping view */
                if (!this.contentLayout) {
                    this.contentLayout = new ContentLayout();
                    this.appLayout.content.show(this.contentLayout);
                    this.contentLayout.render();
                }

                this.contentLayout.changeView(view);
                // Need to be authenticated before rendering view.
                // For cases like a user's settings page where we need to double check against the server.
                //if (typeof options !== 'undefined' && options.requiresAuth) {
                //var self = this;
                //App.session.checkAuth({
                //success: function (res) {
                // If auth successful, render inside the page wrapper
                //$('#content').html(self.currentView.render().$el);
                //}, error: function (res) {
                //self.navigate("/", { trigger: true, replace: true });
                //}
                //});


            },
            index: function () {
                console.log("index()");
                // Fix for non-pushState routing (IE9 and below)
                var hasPushState = !!(window.history && history.pushState);
                if (!hasPushState) this.navigate(window.location.pathname.substring(1), { trigger: true, replace: true });
                else {
                    if (App.session.get('loggedIn')) this.show(new DashboardView({}));
                    //else window.location.replace("/login");
                }

            },
            logout: function () {
                App.session.signOut();
            }
        });
    });