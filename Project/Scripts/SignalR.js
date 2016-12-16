/// <reference path="knockout-3.4.0.js" />
/// <reference path="jquery.signalR-2.2.1.js" />


(function () {
    var chat = $.connection.chat;
    $.connection.hub.logging = true;
    $.connection.hub.start();
    chat.client.newMessage = function (message) {
        model.addMessage(message);
    };


    var Model = function () {
        var self = this;
        self.message = ko.observable(""),
        self.messages = ko.observableArray()
    };


    Model.prototype = {

        sendMessage: function () {
            var self = this;
            chat.server.send(self.message());
            self.message("");
        },

        addMessage: function (message) {
            var self = this;
            self.messages.push(message);
        }
    };


    var model = new Model();

    $(function () {
        ko.applyBindings(model);
    })

})();