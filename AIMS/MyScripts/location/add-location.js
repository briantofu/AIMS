app.controller("myCtrl", function ($scope, $http) {
    $scope.initialize = function () {
        $http.post('/Location/DisplayAllLocation').then(
            function successCallback(response) {
                $scope.locations = response.data;
            },
            function errorCallback(response) {
            });
    }
    $scope.showAddLocationModal = function () {
        $("#addLocationModal").modal("show");
    }
    $scope.closeAddLocationModal = function () {
        $("#addLocationModal").modal("hide");
    }
    $scope.closeEditLocationModal = function () {
        $("#editLocationModal").modal("hide");
    }
    $scope.showEditLocation = function (location) {
        $scope.locationCopy = angular.copy(location);
        $("#editLocationModal").modal("show");
    }
    $scope.addLocation = function () {
        var confirmation = confirm("Are you sure to add this new location?");
        if (confirmation) {
            var data = {
                LocationName: $scope.newLocation
            }
            $http.post('/Location/AddNewLocation', data).then(
             function successCallback(response) {
                 if (response.data == "LocationExists") {
                     alert('Location is already exist!');
                     $scope.newLocation = '';
                 } else {
                     alert('New location successfully added.')
                     $scope.initialize();
                     $scope.newLocation = '';
                     $("#addLocationModal").modal("hide");
                 }
             },
             function errorCallback(response) {

             });
        } else {

        }
    }
    $scope.editLocation = function (locationId) {
        var confirmation = confirm("Are you sure to this new location name?");
        if (confirmation) {
            var data = {
                LocationID: locationId,
                LocationName: $scope.newLocatioName,
            };
            $http.post('/Location/UpdateLocation', data).then(
                function successCallback(response) {
                    if (response.data == "SameLocationName") {
                        alert('Your inputed name of location is the same. Please try another one.');
                        $scope.newLocatioName = '';
                    } else {
                        alert("Location successfully updated.");
                        $scope.initialize();
                        $("#editLocationModal").modal("hide");
                        $scope.newLocatioName = '';
                    }
                }, function errorCallback(response) {

                });
        } else {
        }

    }
});