define(['app', 'models/base'
    ], function(App, BaseModel) {
        var HeaderModel = BaseModel.extend({
            defaults: {
                isManagerOrAdmin: false
            },
            initialize: function () {
                if (App.session.employee.isManager || App.session.employee.isAdmin)
                    this.set({ isManagerOrAdmin: true });
            },
        });

        return HeaderModel;
    });