define(["store"], function (Store) {

    var StoreWithExpiration = {
        set: function (key, val, exp) {
            Store.set(key, { val: val, exp: exp, time: new Date().getTime() })
        },
        get: function (key) {
            var info = Store.get(key)
            if (!info) {
                return null
            }
            if (new Date().getTime() - info.time > info.exp) {
                return null
            }
            return info.val
        }
    };

    return StoreWithExpiration;
});