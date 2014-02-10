define(["underscore"], function (_) {
    var DateHelper = {
        initialize: function () {
            _.bindAll(this, "getDayOfWeek", "tryToGetDateOfDay", "daysInMonth", "findBackward", "findForward", "getDateString");
        },
        findForward: function (date, day) {
            var j = date.getDate();
            var currentDay;
            var daysInMonth = this.daysInMonth(date.getMonth(), date.getFullYear());

            while (true) {
                currentDay = date.getDay();
                if (currentDay == day) {
                    return date;
                }
                date.setDate(++j);
                if (j == daysInMonth) {
                    if (currentDay == day) {
                        return date;
                    }

                    // Is not december
                    if (date.getMonth() != 11) {
                        date.setMonth(date.getMonth() + 1);
                        date.setDate(1);
                        return this.findForward(new Date(date), day);
                    } else {
                        date.setFullYear(date.getFullYear() + 1);
                        date.setMonth(1);
                        date.setDate(1);
                        return this.findForward(new Date(date), day);
                    }
                }
            }
        },
        findBackward: function (date, day) {
            var j = date.getDate();
            var currentDay;

            while (true) {
                currentDay = date.getDay();
                if (currentDay == day) {
                    return date;
                }
                date.setDate(--j);
                if (j == 1) {
                    if (currentDay == day) {
                        return date;
                    }
                    // Is not january, go back one month
                    if (date.getMonth() != 0) {
                        date.setMonth(date.getMonth() - 1);
                        date.setDate(this.daysInMonth(date.getMonth(), date.getFullYear()));
                        return this.findBackward(new Date(date), day);
                    } else {
                        date.setFullYear(date.getFullYear() - 1);
                        date.setMonth(11);
                        date.setDate(31);
                        return this.findBackward(new Date(date), day);
                    }
                }
            }
        },
        tryToGetDateOfDay: function (date, day) {
            // Monday
            if (day == 1) {
                return this.findBackward(date, day);
            }
            // Sunday
            if (day == 0) {
                return this.findForward(date, day);
            }
        },
        getDateString: function (date) {
            var month = parseInt(date.getMonth()) + 1;
            var dd = date.getDate();
            if (month < 10)
                month = '0' + month;
            if (dd < 10)
                dd = '0' + dd;
            return date.getFullYear() + "-" + month + "-" + dd;
        },
        getDayOfWeek: function (day, weekStart) {
            if (!_.isUndefined(weekStart)) {
                var d = new Date();
                // Return date if incrementing with 6 days to sunday works
                d.setDate(weekStart.getDate() + 6);
                if (d.getMonth == weekStart.getMonth && d.getYear == weekStart.getYear) {
                    return this.getDateString(d);
                }
            }

            // First try
            var date = this.tryToGetDateOfDay(new Date(), day);
            if (date.getDay() == day) {
                return this.getDateString(date);
            }
            return "";
        },
        daysInMonth: function (month, year) {
            return new Date(year, month, 0).getDate();
        }
    };
    return DateHelper;
});