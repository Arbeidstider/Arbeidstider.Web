define(["marionette",
        "text!templates/layout.html"],
    function (Marionette, LayoutTemplate) {
    return Backbone.Marionette.Layout.extend({
        template: _.template(LayoutTemplate),

        regions: {
            header: "#header",
            main: "#main-content",
            nav: "#main-nav"
        },
    });
});