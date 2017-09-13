app.controller("myCtrl", function ($scope, $http) {

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
        $http.post("/Location/LoadPageData", data).then(
            function successCallback(response) {
                $scope.locations = response.data;
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Location/LoadPages', data).then(
        function successCallback(response) {
            $scope.pages = response.data;
            if (!$scope.page) {
                $scope.page = $scope.pages[Object.keys($scope.pages)[0]];
            }
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
                LocationName: $scope.newLocation,
                LocationAddress: $scope.newAddress
            }
            $http.post('/Location/AddNewLocation', data).then(
             function successCallback(response) {
                 if (response.data == "LocationExists") {
                     toastr.error("Location already exists. Please try another.", "Can't add new location");
                     $scope.newLocation = '';
                 } else {
                     toastr.success("You've successfully created a new location.", "New Location Added");
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
                        toastr.warning("Please input another location name it must be different with the current name.", "Can't change location name");
                        $scope.newLocatioName = '';
                    } else {
                        toastr.success("You've successfully updated the details of the location.", "Location has been updated");
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