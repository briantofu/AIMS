app.controller('myCtrl', function ($scope, $http, $timeout, $interval) {
    var vm = this;
    $scope.forCheckItem; //variable for cheking if InventoryItem exist
    $scope.forCheckUOM; //variable for cheking if UnitOfMeasurement(UOM) is exist

    //Initialize default item
    $scope.items = [{ Quantity: 1, Description: "", }];

    vm.existingRequestTypes = [
     { name: 'Purchase' },
     { name: 'Lease' }
    ];


    //Display all existing item in inventory
    $scope.initialize = function () {
        $http.post('/Request/AllItem')
            .then(
        function successCallback(response) {
            $scope.existingItem = response.data;
            vm.existingItemsx = [];//initialize  array of InventoryItem
            vm.existingUOMsx = [];//initialize array of UnitOfMeasurement(UOM)
            vm.existingLocations = [];//initialize array of Location
            $scope.loadLocation();
            for (var i in $scope.existingItem) {
                var item = $scope.existingItem[i];
                vm.existingItemsx.push({ ItemName: '' + item['ItemName'], InventoryItemID: item['InventoryItemID'], UnitOfMeasurement: item.UnitOfMeasurement['Description'] });
            }
        },
        function errorCallback(response) {

        });
    }

    // -----------------------------------------------
    $scope.submitRequest = function (requestType, requiredDate, instruction, items,locationID) {
        var data = {
            RequisitionType: requestType,
            //RequiredDate: fRequiredDate,
            RequiredDate: requiredDate,
            SpecialInstruction: instruction,
            LocationID: locationID,
            RequestItems: items
        };
        $http.post('/Request/AddRequestItem', data)
            .then(
        function successCallback(response) {
            if (response.data.length === 0) {
                $scope.items = [{ Quantity: 1, Description: "" }];
                $('#instruction').val('');
                alert("Your request has been sent.");
                $scope.initialize();
            }
        },
        function errorCallback(response) {

        });
    }

    

    //Display all existing supplier
    $scope.loadLocation = function () {
        $http.post('/Requisition/DisplayLocation')
           .then(
       function successCallback(response) {
           $scope.locationData = response.data;
           for (var i in $scope.locationData) {
               var loc = $scope.locationData[i];
               vm.existingLocations.push({
                   LocationID: loc['LocationID'],
                   LocationName: '' + loc['LocationName']
               });
           }
       },
       function errorCallback(response) {

       });
    }

    //Add new item
    $scope.addNewItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push({ Quantity: 1, Description: "" });
    };

    $scope.setItemUOM = function (item, unitOfMeasure) {
        item.UnitOfMeasurement = unitOfMeasure;
    }

    //Remove item
    $scope.removeItem = function () {
        var lastItem = $scope.items.length - 1;
        $scope.items.splice(lastItem);
    };



    //check if item is existing
    function itemExists(item) {
        return vm.existingItemsx.some(function (el) {
            return el.ItemName.toLowerCase().includes(item.toLowerCase());
        });
    }

    //key press function to determine if item is existing
    $scope.key = function (data) {
        if (itemExists(data)) {
            $scope.forCheckItem = true;
        } else {
            $scope.forCheckItem = false;
        }
    };

    //add new item in inventory to database
    $scope.addInventoryItem = function (newItemName, unitOfMeasurement) {
        alert("New item successfully added...");
        var data = {
            newItemName: newItemName,
            unitOfMeasurement: unitOfMeasurement
        };
        $http.post('/Request/AddNewItem', data)
            .then(
        function successCallback(response) {
            if (response.data.length === 0) {
                vm.existingItemsx = [];
                $scope.initialize();
                $("#addItemModal").modal("hide");

            }
        },
        function errorCallback(response) {

        });
    }

    //function to display add new item modal
    $scope.showViewModal = function () {
        $("#addItemModal").modal("show");
        $http.post('/Request/AllUnitOfMeasurement')//gett all existing UnitOfMeasurement(UOM)
             .then(
         function successCallback(response) {
             $scope.existingUOM = response.data;
             vm.existingUOMsx = []
             for (var i in $scope.existingUOM) {
                 var uom = $scope.existingUOM[i];
                 vm.existingUOMsx.push({ UnitOfMeasurementID: '' + uom['UnitOfMeasurementID'], Description: uom['Description'] });
             }
         },
         function errorCallback(response) {

         });
    }

    $scope.tempNewUOM;
    //check if  UnitOfMeasurement(UOM) is existing
    function uomExist(data) {
        return vm.existingUOMsx.some(function (el) {
            return el.Description.toLowerCase().includes(data.toLowerCase());
        });
    }

    //key press function to determine if  UnitOfMeasurement(UOM) is existing
    $scope.searchUOM = function (data) {
        $scope.tempNewUOM = data;
        if (uomExist(data)) {
            $scope.forCheckUOM = true;
        } else {
            $scope.forCheckUOM = false;
        }
    };

    //Add new Unit of Measurement to database
    $scope.addNewUOM = function () {
        vm.existingUOMsx = []
        var data = {
            unitOfMeasurement: $scope.tempNewUOM
        };
        $http.post('/Request/AddNewUnitOfMeasurement', data)
            .then(
        function successCallback(response) {
            if (response.data.length === 0) {
                $http.post('/Request/AllUnitOfMeasurement')//gett all existing UnitOfMeasurement(UOM)
                    .then(
                    function successCallback(response) {
                        $scope.existingUOM = response.data;
                        for (var i in $scope.existingUOM) {
                            var uom = $scope.existingUOM[i];
                            vm.existingUOMsx.push({ UnitOfMeasurementID: '' + uom['UnitOfMeasurementID'], Description: uom['Description'] });
                        }
                    },
                function errorCallback(response) {

                });
                alert("Successfully addedd...");
            }
        },
        function errorCallback(response) {

        });
    }

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    // Disable weekend selection
    function disabled(data) {
        var date = data.date,
          mode = data.mode;
        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
    }

    $scope.toggleMin = function () {
        $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
        $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
    };

    $scope.toggleMin();

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];
    $scope.altInputFormats = ['M!/d!/yyyy'];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
        opened: false
    };

    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    var afterTomorrow = new Date();
    afterTomorrow.setDate(tomorrow.getDate() + 1);
    $scope.events = [
      {
          date: tomorrow,
          status: 'full'
      },
      {
          date: afterTomorrow,
          status: 'partially'
      }
    ];

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }
});

