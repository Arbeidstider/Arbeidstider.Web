define(["marionette",
        "text!templates/layout.html"],
    function (Marionette, LayoutTemplate) {
        return Backbone.Marionette.Layout.extend({
            template: _.template(LayoutTemplate),
            el: "#container",
            regions: {
                header: "#header",
                content: "#main-content",
                nav: "#main-nav"
            },
        });
    });