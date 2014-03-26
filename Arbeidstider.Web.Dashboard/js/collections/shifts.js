define(['backbone', 'models/shift'],
    function (Backbone, ShiftModel) {
        var ShiftCollection = Backbone.Collection.extend({
            model: ShiftModel,
            initialize: function (options) {
            },
        });

        return ShiftCollection;
    });