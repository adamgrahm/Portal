



window.onscroll = function () { onScrollButton() };
function onScrollButton() {
        if (document.body.scrollTop > 600 && !$("#ToTheTop").length) {
            $("#GlobalNavbar").append('<li><a onClick="scrollToTop()" style="float:right;" type="button" class="btn btn-sm btn-info" id="ToTheTop">To the top!</a></li>');
        }
        else if (document.body.scrollTop < 600) {
            $("#ToTheTop").remove();
        }
    }

    function scrollToTop() {
        window.scrollTo(0,0)

    };




//Visar och döljer en reply textarea för svar på första posten!
(function () {
    var app = angular.module("myModule", [])

    app.controller("myController", function($scope)
    {
        $scope.testing = 'Write a reply...'
        $scope.showReply = false;
        $scope.ShowButton = true;
        $scope.QuoteButton = true;
        $scope.HideButton = false;
        $scope.UnquoteButton = false;

        $scope.show = function(Id)
        {
            $scope.showReply = true;
            $scope.ShowButton = false;
            $scope.QuoteButton = true;
            $scope.HideButton = true;
            $scope.UnquoteButton = false;
            $("#textarea_" + Id).text('')
        }
        $scope.hide = function(Id)
        {
            $scope.ShowButton = true;
            $scope.QuoteButton = true;
            $scope.HideButton = false;
            $scope.UnquoteButton = false;
            $scope.showReply = false;
            $("#textarea_" + Id).text('')
        }
        $scope.quote = function (Id)
        {
            var quoting = $("#" + Id).text();
            $scope.ShowButton = false;
            $scope.QuoteButton = false;
            $scope.HideButton = true;
            $scope.UnquoteButton = true;
            $scope.showReply = true;
            $scope.testing = quoting;
            var thenum = Id.replace(/^\D+/g, '');
            $("#textarea_" + thenum).html('"' + quoting + '"')
            
        }

        $scope.unquote = function(Id)
        {
            $("#textarea_" + Id).text('')
            $scope.ShowButton = false;
            $scope.QuoteButton = true;
            $scope.HideButton = true;
            $scope.UnquoteButton = false;

        }




    })

}());



