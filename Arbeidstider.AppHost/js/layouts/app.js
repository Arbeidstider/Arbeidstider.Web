define(["layouts/base",
        "text!templates/layouts/layout.html"],
    function (BaseLayout, LayoutTemplate) {
        var AppLayout = BaseLayout.extend({
            template: _.template(LayoutTemplate),
            //el: "#app-container",
            regions: {
                header: "#header",
                content:
                    {
                        selector: "#content-wrapper",
                    },
                navbar: "#main-nav"
            },
        });

        return AppLayout;
    });