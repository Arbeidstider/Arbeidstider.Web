define(["jquery", "jquery.mobile"], function($, mobile) {
    return {
        initialize: function () {
                if (DEBUG) console.log("Nav.initialize()");
                var body, content, nav, nav_closed_width, nav_open, nav_toggler;
                nav_toggler = $("header .toggle-nav");
                nav = $("#main-nav");
                content = $("#content");
                body = $("body");
                nav_closed_width = 50;
                nav_open = body.hasClass("main-nav-opened") || nav.width() > nav_closed_width;
                $("a.dropdown-collapse").on("click", function(e) {
                    console.log("dropdown collasep");
                    var link, list;
                    e.preventDefault();
                    link = $(this);
                    list = link.parent().find("> ul");
                    if (list.is(":visible")) {
                        if (body.hasClass("main-nav-closed") && link.parents("li").length === 1) {
                            false;
                        } else {
                            link.removeClass("in");
                            list.slideUp(300, function() {
                                return $(this).removeClass("in");
                            });
                        }
                    } else {
                        if (list.parents("ul.nav.nav-stacked").length === 1) {
                            $(document).trigger("nav-open");
                        }
                        link.addClass("in");
                        list.slideDown(300, function() {
                            return $(this).addClass("in");
                        });
                    }
                    return false;
                });
                nav.swiperight(function(event, touch) {
                    return $(document).trigger("nav-open");
                });
                nav.swipeleft(function(event, touch) {
                    return $(document).trigger("nav-close");
                });
                nav_toggler.on("click", function() {
                    console.log("toggle nav");
                    if (nav_open) {
                        $(document).trigger("nav-close");
                    } else {
                        $(document).trigger("nav-open");
                    }
                    return false;
                });
                $(document).bind("nav-close", function(event, params) {
                    body.removeClass("main-nav-opened").addClass("main-nav-closed");
                    return nav_open = false;
                });
                return $(document).bind("nav-open", function(event, params) {
                    body.addClass("main-nav-opened").removeClass("main-nav-closed");
                    return nav_open = true;
                });
        },
    };
});
