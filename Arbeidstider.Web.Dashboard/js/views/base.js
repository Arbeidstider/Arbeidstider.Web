define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    var BaseView = Backbone.View.extend({
        initialize: function () {
            _.bindAll(this, "assignSelectorsToViewAndRender");
        },
        assignSelectorsToViewAndRender: function(selectors) {
            if (!_.isObject(selectors)) return;
            _.each(selectors, function (view, selector) {
                view.setElement(this.$(selector)).render();
            }, this);
        },
    });

    return BaseView;
});
