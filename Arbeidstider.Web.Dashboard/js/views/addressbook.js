define(["underscore",
        'marionette',
        'text!templates/addressbook.html',
        'views/addressbookitem'
], function (_, Marionette, AddressBookTemplate, AddressBookItemView) {
    return Backbone.Marionette.CollectionView.extend({
        letters: function() {
            var array = [];
            var charCodeRange = {
                start: 65,
                end: 90
            };
            var i = 0;
            for (var cc = charCodeRange.start; cc <= charCodeRange.end; cc++) {
                array[i] = String.fromCharCode(cc);
            }
            console.log(array);
            return array;
        },
        template: _.template(AddressBookTemplate),
        initialize: function () {
            _.bindAll(this, "letters");
            this.itemView = AddressBookItemView;
            console.log("AddressBookView.initialize");
        },
    });
});