app.controller("myCtrl", function ($scope, $http) {
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
        $http.post("/Inventory/LoadPageData", data).then(
            function successCallback(response) {
                $scope.itemStocks = response.data;
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Inventory/LoadPages', data).then(
        function successCallback(response) {
            $scope.pages = response.data;
            
            if (!$scope.page) {
                $scope.page = $scope.pages[Object.keys($scope.pages)[0]];
            }
        },
        function errorCallback(response) {
        });
    }

    $scope.validation = function (inventoryItemId, itemName, totalStock, requestedQuantity, newItemLimit) {
        var isValid;


    }
});