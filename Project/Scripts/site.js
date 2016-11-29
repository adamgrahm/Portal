//Visar och döljer en reply textarea för svar på första posten!
(function () {
    var app = angular.module("myModule", [])

    app.controller("myController", function($scope)
    {
        $scope.testing = 'Write a reply...'
        $scope.showReply = false;

        $scope.show = function(Id)
        {
            $scope.showReply = true;
            $("#textarea_" + Id).text('')
        }
        $scope.hide = function(Id)
        {
            $scope.showReply = false;
            $("#textarea_" + Id).text('')
        }
        $scope.quote = function (Id)
        {
            var quoting = $("#" + Id).text();
            $scope.showReply = true;
            $scope.testing = quoting;
            var thenum = Id.replace(/^\D+/g, '');
            $("#textarea_" + thenum).text(quoting)
            
        }
    })

}());

