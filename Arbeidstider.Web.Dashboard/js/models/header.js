define([
  'jquery',
  'views/base',
], function ($, BaseModel) {
    var HeaderMenu = BaseModel.extend({
        initialize: function (opt) {
            this.fullname = opt.fullname;
        },
    });
    
    return HeaderMenu;
});
