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
            pagestatus: pagestatus
        };
        $http.post("/Reviewer/DeliveryMonitoringPageData", data).then(
            function successCallback(response) {
                $scope.requisitions = response.data;
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Reviewer/DeliveryMonitoringPages', data).then(
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
                requisition: $scope.requisition
            };
        $http.post("/Reviewer/DeliveryMonitoringRequisitionItem", data).then(
            function successCallback(response) {
                $scope.partialRequisition = response.data;
                $scope.overTotal = 0;
                for (var item in $scope.partialRequisition[0].RequisitionItem) {
                    var x = $scope.partialRequisition[0].RequisitionItem[item];
                    $scope.overTotal += (x["Quantity"] * x["UnitPrice"]);
                }
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {

            }
        );
    }

    //Show decline modal
    $scope.closeViewModal = function () {

        $("#viewModal").modal("hide");
    }

    //Display supplier info modal
    $scope.SupplierInformation = function (item) {
        $scope.supplierInfo = item;
        var data = {
            SupplierID: $scope.supplierInfo.SupplierID
        }
        $http.post("/Reviewer/DisplaySupplierItem", data).then(
                function successCallback(response) {
                    $scope.supplierItems = response.data;
                    //$scope.requisition.SupplierID = $scope.supplier.SupplierId;
                    //$scope.hasSupplier = true;
                    //$scope.initialize();
                    $("#supplierInfoModal").modal("show");
                },
                function errorCallback(response) {

                }
            );
    }

    //Close supplier info modal
    $scope.closeSupplierInfo = function () {
        $("#supplierInfoModal").modal("hide");
    }
});