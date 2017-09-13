app.controller('myCtrl', function ($scope, $http, $timeout, $interval) {
    var vm = this;
    $scope.supplierIDChoice = 0;
    $scope.forCheckItem; //variable for cheking if InventoryItem exist
    $scope.forCheckUOM; //variable for cheking if UnitOfMeasurement(UOM) is exist
    $scope.items = [{ Quantity: 1, Description: "", UnitPrice: 0, UnitOfMeasurement: "Select from products" }];//Initialize default item
    $scope.toggleItem = true;

    vm.existingRequestTypes = [
      { name: 'Purchase' },
      { name: 'Lease' }
    ];

    //Display all existing item in inventory
    $scope.initialize = function () {
        $http.post('/Requisition/AllItem')
            .then(
        function successCallback(response) {
            $scope.existingItem = response.data;
            vm.existingItemsx = [];//initialize  array of InventoryItem
            vm.existingUOMsx = [];//initialize array of UnitOfMeasurement(UOM)
            $scope.ctrl.forNewItem = [];
            vm.existingLocations = [];//initialize array of Location
            vm.existingSuppliers = [{ SupplierName: 'No Supplier', SupplierID: 0 }];//initialize array of Suppliers
            $scope.loadSupplier();
            $scope.loadLocation();
            $scope.setItemViaSupplier($scope.supplierIDChoice);
            for (var i in $scope.existingItem) {
                var item = $scope.existingItem[i];
                vm.existingItemsx.push({ ItemName: '' + item['ItemName'], InventoryItemID: item['InventoryItemID'], UnitOfMeasurement: item.UnitOfMeasurement['Description'] });
            }
        },
        function errorCallback(response) {

        });
    }

    //Display all existing supplier
    $scope.loadSupplier = function () {
        $http.post('/Requisition/DisplayCompanyName')
           .then(
       function successCallback(response) {
           $scope.supplierData = response.data;
           for (var i in $scope.supplierData) {
               var supp = $scope.supplierData[i];
               vm.existingSuppliers.push({
                   SupplierName: '' + supp['SupplierName'],
                   SupplierID: supp['SupplierID']
               });
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
        $scope.items.push({ Quantity: 1, UnitPrice: 0, UnitOfMeasurement: "Select from products" });
    };

    //Remove item
    $scope.removeItem = function () {
        var lastItem = $scope.items.length - 1;
        $scope.items.splice(lastItem);
    };

    //Display UOM by selecting in combo box
    $scope.setItemUOM = function (item, unitOfMeasure) {
        item.UnitOfMeasurement = unitOfMeasure;
        $scope.forCheckItem = true;
    }

    //Display all supplier item by selecting in combo box
    var tempid;
    $scope.setItemViaSupplier = function (id) {
        $scope.supplierIDChoice = id;
        var data = {
            SupplierID: id
        };
        $http.post('/Requisition/DisplayViaSupplier', data)
           .then(
       function successCallback(response) {
           //NAG EEROR KAPAG LENGTH === 0 (TAMA NAMAN)
           if (tempid != id)
               $scope.items = [{ Quantity: 1, Description: "", UnitPrice: 0, UnitOfMeasurement: "Select from products" }];//Initialize default item
           tempid = id;
           vm.existingItemsx = [];//initialize  array of InventoryItem
           vm.existingUOMsx = [];//initialize array of UnitOfMeasurement(UOM)
           $scope.existingItems = response.data;
           for (var i in $scope.existingItems) {
               var item = $scope.existingItems[i];
               vm.existingItemsx.push({
                   ItemName: '' + item['ItemName'],
                   InventoryItemID: item['InventoryItemID'],
                   UnitOfMeasurement: item.UnitOfMeasurement['Description'],
                   UnitOfMeasurementID: item.UnitOfMeasurement['UnitOfMeasurementID']
               });
           }
       },
       function errorCallback(response) {

       });
    }

    /*FUNTION TO SHOW ALL MODALS*/

    //Function to show confirmation modal
    $scope.submitRequest = function (requestType, requiredDate, instruction, items,locationID) {
        if ($scope.supplierIDChoice == 0) {
            $("#confirmModal").modal("show");
        } else {
            $scope.submitRequisition(requestType, requiredDate, instruction, items, locationID, 'No');
        }
    }

    //For reload all item again
    $scope.reloadAllItem = function () {
        $http.post('/Requisition/AllItem')
           .then(
       function successCallback(response) {
           $scope.existingItem = response.data;
           $scope.ctrl.forNewItem = [];
           vm.existingItemsx = [];
           for (var i in $scope.existingItem) {
               var item = $scope.existingItem[i];
               vm.existingItemsx.push({
                   ItemName: '' + item['ItemName'],
                   InventoryItemID: item['InventoryItemID'],
                   UnitOfMeasurement: item.UnitOfMeasurement['Description'],
                   UnitOfMeasurementID: item.UnitOfMeasurement['UnitOfMeasurementID']
               });
           }
       },
       function errorCallback(response) {

       });
    }

    //Function to display add new item modal
    $scope.showAddItemModal = function () {
        $scope.reloadAllItem();
        $("#addItemModal").modal("show");
        $http.post('/Requisition/AllUnitOfMeasurement')//gett all existing UnitOfMeasurement(UOM)
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

    //Function to close supplier information
    $scope.closeSupplierInfo = function () {
        $("#supplierInfoModal").modal("hide");
    }
    $scope.closeAddItemModal = function () {
        $("#addItemModal").modal("hide");
    }
    //Function to show supplier information
    $scope.showSupplierInfo = function (supplierData) {
        $scope.supplierInfo = [];
        for (var i in supplierData) {
            var suppInfo = supplierData[i];
            if (suppInfo['SupplierID'] === $scope.supplierIDChoice) {
                $scope.supplierInfo = [{
                    SupplierName: "" + suppInfo['SupplierName'],
                    Address: suppInfo['Address'],
                    ContactPerson: suppInfo['ContactPerson'],
                    ContactNo: suppInfo['ContactNo'],
                    Email: suppInfo['Email'],
                    SupplierID: suppInfo['SupplierID'],
                }];
                $("#supplierInfoModal").modal("show");
            }
        }

    }

    //Submit Request
    $scope.submitRequisition = function (requestType, requiredDate, instruction, items, locationID, cnf) {

        if (cnf == "Yes") {
            status = "Need Supplier"
        } else if (cnf == "No") {
            status = "For Initial Review"
        }
        var data = {
            //RequisitionDate: requisitionDate,
            RequisitionType: requestType,
            RequiredDate: requiredDate,
            SpecialInstruction: instruction,
            Status: status,
            SupplierID: $scope.supplierIDChoice,
            LocationID:locationID,
            RequisitionItems: items

        };
        $http.post('/Requisition/AddRequisitionItem', data)
            .then(
        function successCallback(response) {
            if (response.data.length === 0) {
                $scope.items = [{ Quantity: 1, Description: "", UnitPrice: 0 }];
                $scope.initialize();
                $scope.supplierID = "";
                $("#confirmModal").modal("hide");
                $('#instruction').val('');
                alert("Your requesistion has been sent.");
            }
        },
        function errorCallback(response) {

        });
    }

    //add new item in inventory to database
    $scope.addInventoryItem = function (newItemName, forNewItem, unitOfMeasurementID) {
        var data = {
            newItemName: newItemName,
            InventoryItem: forNewItem,
            unitOfMeasurementID: (unitOfMeasurementID == null ? 0 : unitOfMeasurementID),
            supplierID: $scope.supplierIDChoice
        };
        $http.post('/Requisition/AddNewItem', data)
            .then(
        function successCallback(response) {

            if (response.data == "ItemExist") {
                alert("Item Already exist!");
                $("#newItem").val("");
                //$scope.ctrl.forNewItem = [];
            } else {
                $("#focusedInput").val("");
                $("#newItem").val("");
                $scope.initialize();
                $("#addItemModal").modal("hide");
                $scope.custom = true;
                $scope.toggleText = "Existing Item"
                $scope.toggleStyle = "btn btn-primary"

                alert("New item successfully added...");
            }
        },
        function errorCallback(response) {
        });
    }

    //Add new Unit of Measurement to database
    $scope.addNewUOM = function () {
        vm.existingUOMsx = []
        var data = {
            unitDescription: $scope.tempNewUOM
        };
        $http.post('/Requisition/AddNewUnitOfMeasurement', data)
            .then(
        function successCallback(response) {
            if (response.data.length === 0) {
                $http.post('/Requisition/AllUnitOfMeasurement')//gett all existing UnitOfMeasurement(UOM)
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

    //Check if item is existing
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
    //$(function () {
    //    $("#toggle-item").change(function () {
    //        if ($(this).prop("checked") == true) {
    //            $scope.toggleItem = true;
    //        } else {
    //            $scope.toggleItem = false;
    //        }
    //    });
    //})
    $scope.custom = true;
    $scope.toggleText = "Existing Item"
    $scope.toggleStyle = "btn btn-primary"
    $scope.toggleCustom = function () {
        $scope.ctrl.forNewItem = [];
        $scope.custom = $scope.custom === false ? true : false;
        if ($scope.custom == true) {
            $scope.toggleText = "Existing Item"
            $scope.toggleStyle = "btn btn-primary"
        } else {
            $scope.toggleText = "New Item"
            $scope.toggleStyle = "btn btn-success"
        }
    };


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
        $scope.dateOptions.minDate = new Date();
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
