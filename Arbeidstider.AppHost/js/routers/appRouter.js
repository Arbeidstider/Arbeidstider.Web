define(['app',
        'backbone',
    'jquery',
        'nav',
        'layouts/app',
        'layouts/content',
        "models/session",
        'views/login',
        'views/addressbook',
        'views/calendar',
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
    function (App, Backbone, $, Nav, AppLayout, ContentLayout, Session, LoginView, AddressbookView,
        CalendarView, ChangeworkdayView, ConfirmavailabledaysView, MessagesView, SetdaysfreeView, DashboardView, ProfileView, RegisterView, HeaderView, NavbarView) {
        return Backbone.Router.extend({
            //"index" must be a method in AppRouter's controller
            initialize: function () {
                _.bindAll(this, "show");
                var self = this;

                // All navigation that is relative should be passed through the navigate
                // method, to be processed by the router. If the link has a `data-bypass`
                // attribute, bypass the delegation completely.
                $('body').on("click", "a:not([data-bypass])", function (evt) {
                    evt.preventDefault();
                    var href = $(this).attr("href");
                    if (href = "#") return false;
                    if (DEBUG) console.log("click inside #content-wrapper to: " + href);
                    self.navigate(href, { trigger: true, replace: false });
                });
            },
            routes: {
                /* Route Name: Function */
                "": "index",
                "logout": "logout",
                "signout": "logout",
                "logoff": "logout",
                "login": "login",
                "signoff": "logout",
                "kalender": "calendar",
                "calendar": "calendar",
                "nyansatt": 'register',
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
            login: function () {
                if (!App.session) {
                    App.session = new Session();
                }
                if (!this.loginView) {
                    this.loginView = new LoginView();
                }
                console.log("login route");
                App.body.show(this.loginView);
                this.loginView.render();
                if (Boolean($.cookie("rememberMe"))) {
                    $("#UserName").val($.cookie("userName"));
                }
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
            calendar: function () {
                this.show(new CalendarView());
            },
            messages: function () {
                this.show(new MessagesView());
            },
            profile: function () {
                this.show(new ProfileView());
            },
            register: function () {
                console.log("register()");
                this.show(new RegisterView());
            },
            show: function (view, options) {
                console.log("show");
                /* The main layout inside #content-app, layouts/app.js, contains navbar/header and content-wrapper */
                if (!this.appLayout) {
                    this.appLayout = new AppLayout();
                    App.body.show(this.appLayout);
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

                var self = this;
                App.session.checkAuth({
                    success: function (res) {
                        // If auth successful, render inside the page wrapper
                        App.session.set({ loggedIn: true });
                        self.contentLayout.content.show(view);
                    },
                    error: function (res) {
                        if (DEBUG) console.log("check auth failed when swithcing view!, should have been redirectd to unauth page");
                        //self.navigate("/", { trigger: true, replace: true });
                    }
                });
                // checkAuth/isAuthenticated
                // Need to be authenticated before rendering view.
                // For cases like a user's settings page where we need to double check against the server.
                //App.session.checkAuth({
                //    success:
                //        function (mod, resp) {
                //            if (resp && resp.IsAuthenticated) {
                //                console.log("checkAuth ok");
                //                App.session.set({ loggedIn: true });
                //                self.contentLayout.changeView(view);
                //            } else {
                //                //if (DEBUG) console.log("checkAuth not ok");
                //                console.log("checkAuth not ok");
                //                App.session.set({ loggedIn: false });
                //            }
                //        },
                //    error: function () {
                //        if (DEBUG) console.log("Could not verify authentication and can not change view");
                //        self.navigate("/", { trigger: true, replace: true });
                //    }
                //});
            },
            index: function () {
                var self = this;
                console.log("index route");
                // Fix for non-pushState routing (IE9 and below)
                var hasPushState = !!(window.history && history.pushState);
                if (!hasPushState) this.navigate(window.location.pathname.substring(1), { trigger: true, replace: false });
                App.session.checkAuth({
                    success: function (mod, resp) {
                        if (App.session.get('loggedIn')) {
                            var view = new DashboardView();
                            console.log("showing view");
                            self.show(view);
                        } else {
                            if (DEBUG) console.log("index(): not logged in");
                            self.navigate("login", { trigger: true, replace: false });
                        }
                    },
                    complete: function () {
                        if (!App.session.get('loggedIn')) {
                            if (DEBUG) console.log("index(): not logged in");
                            self.navigate("login", { trigger: true, replace: false });
                        }
                    }
                });

            },
            logout: function () {
                App.session.signOut();
            }
        });
    });