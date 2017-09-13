app.controller("myCtrl", function ($scope, $http) {
    $scope.requisition;
    $scope.initialize = function () {
        var data =
           {
               Status: "For Approval"
           };
        $http.post('/Reviewer/ViewAllRequisition', data).then(
            function successCallback(response) {
                $scope.requisitions = response.data;
                if ($scope.requisitions.length > 1) {
                    text = 'You have (' + $scope.requisitions.length + ') new requisition/request received. Click here to view the details...';
                }  else {
                    text = 'You have a new item requisition received. Click here to view the details.';
                }
                if ($scope.requisitions.length != 0) {
                    PNotify.desktop.permission();
                    (new PNotify({
                        title: 'New Notification',
                        text: text,
                        desktop: {
                            desktop: true,
                        }
                    })).get().click(function (e) {
                        PNotify.removeAll();
                        if ($('.ui-pnotify-closer, .ui-pnotify-sticker, .ui-pnotify-closer *, .ui-pnotify-sticker *').is(e.target)) return;
                        alert('Declined Requests');
                    });
                }
            },
            function errorCallback(response) {

            });
    }
    $scope.showViewModal = function (requisition) {
        $scope.requisition = angular.copy(requisition);
        var data =
            {
                requisition: requisition
            };
        $http.post("/Reviewer/RequisitionItem", data).then(
            function successCallback(response) {
                $scope.requisitionItems = response.data;
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {

            }
        );
    }

    //Show decline modal
    $scope.showDeclineModal = function () {

        $("#viewModal").modal("hide");
        $("#declineModal").modal("show");
    }
    //Close decline modal
    $scope.closeDeclineModal = function () {
        $("#declineModal").modal("hide");
        $("#viewModal").modal("show");
    }
    //Decline modal function
    $scope.declineFunction = function (requisitionID, reason) {
        var data =
            {
                requisitionID: requisitionID,
                ReasonForDeclining: reason,
                Status: "Declined by Approver"
            };
        $http.post("/Reviewer/DeclineRequisition", data).then(
            function successCallback(response) {
                $scope.initialize();
                $scope.requisitionItems = response.data;
                $("#declineModal").modal("hide");
            },
            function errorCallback(response) {
            }
        );
    }

    //Accept requisition
    $scope.acceptFunction = function (requisitionID, requisitionItems) {
        var data =
            {
                requisitionID: requisitionID,
                requisitionItems: requisitionItems,
                Status : 'Approved'
            };
        $http.post("/Reviewer/AcceptRequisition", data).then(
            function successCallback(response) {
                $scope.initialize();
                $scope.requisitionItems = response.data;
                $("#viewModal").modal("hide");
            },
            function errorCallback(response) {
            }
        );
    }

    //Display supplier info modal
    $scope.SupplierInformation = function (item) {
        $scope.supplierInfo = item;
        $("#supplierInfoModal").modal("show");
    }
    //Close supplier info modal
    $scope.closeSupplierInfo = function () {
        $("#supplierInfoModal").modal("hide");
    }
});