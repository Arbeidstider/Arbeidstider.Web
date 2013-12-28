    app.ErrorView = Backbone.View.extend({
      events: {
        'ajaxError': 'handleAjaxError'
      },
      handleAjaxError: function (event, request, settings, thrownError) {
      }
    });