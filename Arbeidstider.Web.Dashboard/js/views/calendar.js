define(['underscore',
        'marionette',
        'jquery',
        'app',
        'tooltip',
        'text!templates/calendar.html'
], function (_, Marionette, $, App, Tooltip, CalendarTemplate) {
    var CalendarView = Backbone.Marionette.ItemView.extend({
        template: _.template(CalendarTemplate),
        initialize: function () {
            //_.bindAll(this, "render");
        },
        onShow: function () {
            console.log("onShow");
            var options = {
                events_source: 'http://localhost:8181/calendar/events',
                view: 'month',
                tmpl_path: 'templates/calendar/',
                tmpl_cache: false,
                day: '2014-03-28',
                onAfterEventsLoad: function (events) {
                    if (!events) {
                        return;
                    }
                    var list = $('#eventlist');
                    list.html('');

                    $.each(events, function (key, val) {
                        $(document.createElement('li'))
                            .html('<a href="' + val.url + '">' + val.title + '</a>')
                            .appendTo(list);
                    });
                },
                onAfterViewLoad: function (view) {
                    $('.page-header h3').text(this.getTitle());
                    $('.btn-group button').removeClass('active');
                    $('button[data-calendar-view="' + view + '"]').addClass('active');
                },
                classes: {
                    months: {
                        general: 'label'
                    }
                }, views: {
                    year: {
                        slide_events: 1,
                        enable: 1
                    },
                    month: {
                        slide_events: 1,
                        enable: 1
                    },
                    week: {
                        enable: 1
                    },
                    day: {
                        enable: 1
                    }
                },
                merge_holidays: false,
                // ------------------------------------------------------------
                // CALLBACKS. Events triggered by calendar class. You can use
                // those to affect you UI
                // ------------------------------------------------------------
                onBeforeEventsLoad: function (next) {
                    // Inside this function 'this' is the calendar instance
                    next();
                },
            };
            //$(function () {
                var calendar = $("#calendar").calendar(options);

                $('.btn-group button[data-calendar-nav]').each(function () {
                    var $this = $(this);
                    $this.click(function () {
                        calendar.navigate($this.data('calendar-nav'));
                    });
                });

                $('.btn-group button[data-calendar-view]').each(function () {
                    var $this = $(this);
                    $this.click(function () {
                        calendar.view($this.data('calendar-view'));
                    });
                });

                $('#first_day').change(function () {
                    var value = $(this).val();
                    value = value.length ? parseInt(value) : null;
                    calendar.setOptions({ first_day: value });
                    calendar.view();
                });

                $('#language').change(function () {
                    calendar.setLanguage($(this).val());
                    calendar.view();
                });

                $('#events-in-modal').change(function () {
                    var val = $(this).is(':checked') ? $(this).val() : null;
                    calendar.setOptions({ modal: val });
                });
                $('#events-modal .modal-header, #events-modal .modal-footer').click(function (e) {
                    //e.preventDefault();
                    //e.stopPropagation();
                });
            //});
        }
    });

    return CalendarView;
});