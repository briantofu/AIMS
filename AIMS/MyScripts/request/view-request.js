app.controller("myCtrl", function ($scope, $http) {
    $scope.request;
    $scope.initialize = function () {
        $http.post('/Request/AllPendingRequest')
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
    //Reject request
    $scope.declineFunction = function (requestId, reasonForDeclining) {
        var declineConfirm = confirm("Are you sure you want to decline the request?");
        if (declineConfirm) {
            var data =
                      {
                          requestID: requestId,
                          Status: "Declined",
                          ReasonForDeclining: reasonForDeclining
                      };
            $http.post("/Request/DeclineRequest", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    //$scope.requestItems = response.data;
                    toastr.success("You've decline the requisition.", "Request Declined"),
                    $("#declineModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }

    }
    //Accept request
    $scope.acceptFunction = function (requestId) {
        var acceptConfirm = confirm("Are you sure you want to accept the request?");
        if (acceptConfirm) {
            var data =
          {
              requestID: requestId,
              requestItems: $scope.requestItems
          };
            $http.post("/Request/AcceptRequest", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    $scope.requestItems = response.data;
                    toastr.success("You've successfully sent your request. Please check you approval.", "Request Sent"),
                    $("#viewModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }
    }
    //Show decline modal
    $scope.showDeclineModal = function () {

        $("#declineModal").modal("show");
        $("#viewModal").modal("hide");
    }
    //Close decline modal
    $scope.closeDeclineModal = function () {
        $("#declineModal").modal("hide");
        $("#viewModal").modal("show");
    }

});