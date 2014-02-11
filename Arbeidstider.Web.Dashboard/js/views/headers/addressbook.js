define(["underscore",
        "marionette",
        "text!templates/headers/addressbook.html"],
    function (_, Marionette, AddressBookHeaderTemplate) {
        var AddressBookHeaderView = Backbone.Marionette.ItemView.extend({
            template: _.template(AddressBookHeaderTemplate)
        });

        return AddressBookHeaderView;

    });
