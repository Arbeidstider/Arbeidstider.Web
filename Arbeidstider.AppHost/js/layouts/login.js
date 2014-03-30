define(["layouts/base",
        "text!templates/layouts/login.html"],
    function (BaseLayout, LoginTemplate) {
        var AppLayout =  BaseLayout.extend({
            template: _.template(LayoutTemplate),
            regions: {
                header: "#header",
                content: "#content-wrapper",
                navbar: "#main-nav"
            },
        });

        return AppLayout;
    });