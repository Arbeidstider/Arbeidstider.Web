define(["app",
        "layouts/base",
        "globals",
        "text!templates/layouts/content.html"],
    function (App, BaseLayout, Globals, ContentTemplate) {
        var ContentLayout = BaseLayout.extend({
            el: "#content-wrapper",
            template: _.template(ContentTemplate),
            regions: {
                content: 
                    { 
                        selector: "#main-content",
                        regionType: Backbone.Marionette.TransitionRegion
                    }
            },
            initialize: function () { },
            changeView: function (view) {
                console.log("changeView");
                $("#main-content").fadeOut("slow", function() {
                    this.content.show(view);
                });
            }
            //showCalendar: function () {
            //    console.log("bootstrappedCalendar: " + Globals.bootstrappedCalendar);
            //    // convert to model
            //    var collection = new CalendarDayCollection();
            //    var view = new CalendarView({ collection: collection });
            //    this.mainColumn.show(view);
            //    collection.reset(Globals.bootstrappedCalendar || []);
            //}
        });

        return ContentLayout;
    });