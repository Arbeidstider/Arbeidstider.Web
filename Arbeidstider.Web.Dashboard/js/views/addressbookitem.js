define(['marionette',
        'text!templates/addressbookitem.html'
], function (Marionette, AddressBookItemTempate) {
    return Backbone.Marionette.ItemView.extend({
        template: _.template(AddressBookItemTempate),
        initialize: function () {
            console.log("AddressBookView.initialize");
        },
    });
});