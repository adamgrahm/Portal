

//(function () {
//    $("#ToTheTop").hide();
//})();


window.onscroll = function () { test() };
    function test() {
        if (document.body.scrollTop > 1200 && !$("#ToTheTop").length) {
            $("#NavbarTest").append('<li><a href="#" type="button" class="btn btn-sm btn-info" id="ToTheTop">To the top!</a></li>');
        }
        else if (document.body.scrollTop < 1200) {
            $("#ToTheTop").remove();
        }
    }

    $("button").click(function () {
        $('html,body').animate({
            scrollTop: $(".navbartest").offset().top
        },
            'slow');
    });


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

(function () {
    var app1 = angular.module("HideMessage", []);

    app1.controller("HideMessageController", function ($scope) {
        $scope.showMessage = false;
        $scope.HideButton = false;
        $scope.ShowButton = true;

        $scope.show = function () {
            $scope.showMessage = true;
            $scope.HideButton = true;
            $scope.ShowButton = false;
        }

        $scope.hide = function () {
            $scope.showMessage = false;
            $scope.HideButton = false;
            $scope.ShowButton = true;
        }
    })
})();

(function () {

    var app2 = angular.module("ProfilePage", []);

    app2.controller("test", function($scope)
    {

    })
})();

