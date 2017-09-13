app.controller("myCtrl2", function ($scope, $http) {
    $scope.filterActions = function () {
        $http.post('/FilterActions/SelectUserRoles').then(
            function successCallBack(response) {
                $scope.userRoles = response.data;
            });
    }
});