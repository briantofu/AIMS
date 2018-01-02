app.controller("myCtrl", function ($scope, $http) {
    //Replacing codes
    $scope.requisition;
    $scope.initialize = function () {
        $scope.page;
        $scope.loadpage(1, true);
    }

    //======LOAD USERS IN PAGE========//
    $scope.pageChange = function (page) {
        $scope.page = page;
        $scope.loadpage(page.PageNumber, page.PageStatus);
    }

    $scope.loadpage = function (pagenumber, pagestatus) {
        var data = {
            pagenumber: pagenumber,
            pagestatus: pagestatus
        };
        $http.post("/Requisition/LoadPageDeliveredData", data).then( //Created the http.post of LoadPageDeliveredData on the Requisition Controller Directory
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
                //    });
                //}
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Requisition/LoadDeliveredPages', data).then( //Created the http.post of LoadDeliveredPages on the Requisition Controller Directory
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
                $scope.overTotal = 0;
                for (var item in $scope.requisitionItems) {
                    var x = $scope.requisitionItems[item];
                    $scope.overTotal += (x["Quantity"] * x["UnitPrice"]);
                }
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {
            }
        );
    }

    //Close decline modal
    $scope.closeViewModal = function () {
        $("#viewModal").modal("hide");
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
