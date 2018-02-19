app.controller("myCtrl", function ($scope, $http) {
    $scope.requisition;

    $scope.initialize = function () {
        $scope.page;
        $scope.loadpage(1, true);
    }

    $scope.pageChange = function (page) {
        $scope.page = page;
        $scope.loadpage(page.PageNumber, page.PageStatus);
    }

    $scope.loadpage = function (pagenumber, pagestatus) {
        var data = {
            pagenumber: pagenumber,
            pagestatus: pagestatus,
            status: "For Approval"
        };
        $http.post("/Reviewer/LoadPageData", data).then(
            function successCallback(response) {
                $scope.requisitions = response.data;
                //if ($scope.requisitions.length > 1) {
                //    text = 'You have (' + $scope.requisitions.length + ') new requisition/request received. Click here to view the details...';
                //} else {
                //    text = 'You have a new requisition/request received. Click here to view the details.';
                //}
                //if ($scope.requisitions.length != 0) {
                //    PNotify.desktop.permission();
                //    (new PNotify({
                //        title: 'New Notification',
                //        text: text,
                //        desktop: {
                //            desktop: true,
                //        }
                //    })).get().click(function (e) {
                //        PNotify.removeAll();
                //        if ($('.ui-pnotify-closer, .ui-pnotify-sticker, .ui-pnotify-closer *, .ui-pnotify-sticker *').is(e.target)) return;
                //        alert('Declined Requests');
                //    });
                //}
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Reviewer/LoadPages', data).then(
        function successCallback(response) {
            $scope.pages = response.data;
            if (!$scope.page) {
                $scope.page = $scope.pages[Object.keys($scope.pages)[0]];
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

        $("#declineModal").modal("show");
        $("#viewModal").modal("hide");
    }

    //Close decline modal
    $scope.closeDeclineModal = function () {
        $("#declineModal").modal("hide");
        $("#viewModal").modal("show");
    }

    //Decline modal function
    $scope.declineFunction = function (requisitionId, reasonForDeclining) {
        var declineConfirm = confirm('Are you sure to decline this requisition?');
        if (declineConfirm) {
            var data =
           {
               requisitionID: requisitionId,
               status: "Declined",
               reasonForDeclining: reasonForDeclining
           };
            $http.post("/Reviewer/DeclineRequisition", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    $scope.requisitionItems = response.data;
                    toastr.success("The requisition has been sent back to the requestor.", "Requisition has been declined");
                    $("#declineModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }
       
    }

    //Accept requisition
    $scope.acceptFunction = function (requisitionID, requisitionItems) {

        var declineConfirm = confirm('Are you sure to accept this requisition?');
        if (declineConfirm) {
            var data =
                {
                    requisitionID: requisitionID,
                    requisitionItems: requisitionItems,
                    Status: "For Purchasing"
                };
            $http.post("/Reviewer/AcceptRequisition", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    $scope.requisitionItems = response.data;
                    toastr.success("The requistion is now ready for processing. Please see it  in Order Processing form", "Requisition has been accepted");
                    $("#viewModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }

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