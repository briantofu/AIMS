app.controller("myCtrl", function ($scope, $http) {
    $scope.request;
    $scope.initialize = function () {
        $http.post('/Request/AllRequest')
            .then(
        function successCallback(response) {
            $scope.requests = response.data;
        },
        function errorCallback(response) {

        });
    }
    $scope.showViewModal = function (request) {
        $scope.request = angular.copy(request);
        var data =
            {
                requestID: request.RequestID
            };
        $http.post("/Request/RequestItem", data).then(
            function successCallback(response) {
                $scope.requestItems = response.data;
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {
            }
        );
    }
    $scope.closeviewModal = function () {
        $("#viewModal").modal("hide");
    }
    ////Reject request
    //$scope.declineFunction = function (requestID) {
    //    var data =
    //        {
    //            requestID: requestID
    //        };
    //    $http.post("/Request/DeclineRequest", data).then(
    //        function successCallback(response) {
    //            $scope.initialize();
    //            $scope.requestItems = response.data;
    //            $("#viewModal").modal("hide");
    //        },
    //        function errorCallback(response) {
    //        }
    //    );
    //}
    ////Accept request
    //$scope.acceptFunction = function (requestID, requestItems) {
    //    var data =
    //        {
    //            requestID: requestID,
    //            requestItems: requestItems
    //        };
    //    $http.post("/Request/AcceptRequest", data).then(
    //        function successCallback(response) {
    //            $scope.initialize();
    //            $scope.requestItems = response.data;
    //            $("#viewModal").modal("hide");
    //        },
    //        function errorCallback(response) {
    //        }
    //    );
    //}
});