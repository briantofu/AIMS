app.controller('myCtrl', function ($scope, $http, $timeout, $interval) {
    var vm = this;
    $scope.supplierIDChoice = 0;
    $scope.forCheckItem; //variable for cheking if InventoryItem exist
    $scope.forCheckUOM; //variable for cheking if UnitOfMeasurement(UOM) is exist
    $scope.toggleItem = true;
    $scope.items = [{
        InventoryItemID: undefined,
        Quantity: 1,
        Description: "",
        UnitOfMeasurement: "Select from items",
    }];//Initialize default item
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
                //vm.existingSuppliers = [{ SupplierName: 'No Supplier', SupplierID: 0 }];//initialize array of Suppliers
                //$scope.loadSupplier();
                $scope.loadLocation();
                //$scope.setItemViaSupplier($scope.supplierIDChoice);
                for (var i in $scope.existingItem) {
                    var item = $scope.existingItem[i];
                    vm.existingItemsx.push({ ItemName: '' + item['ItemName'], InventoryItemID: item['InventoryItemID'], UnitOfMeasurement: item.UnitOfMeasurement['Description'] });
                }
            },
            function errorCallback(response) {

            });
    }
    //Display all existing location
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
    //Add new item
    $scope.addNewItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push({ Quantity: 1, UnitPrice: 0, UnitOfMeasurement: "Select from products" });
    };
    //Remove item
    $scope.removeItem = function (index) {
        $scope.items.splice(index, 1);
    };
    //Display UOM by selecting in combo box
    $scope.setItemUOM = function (item, unitOfMeasure) {
        item.UnitOfMeasurement = unitOfMeasure;
        $scope.forCheckItem = true;
    }
    //Function to show confirmation modal
    $scope.submitRequest = function (requestType, requiredDate, instruction, items, locationID) {
        //if ($scope.supplierIDChoice == 0) {
        //    $("#confirmModal").modal("show");
        //} else {
        //    $scope.submitRequisition(requestType, requiredDate, instruction, items, locationID, 'No');
        //}

        var validation = true;
        validation = (locationID != null && requiredDate != undefined);

        if (!validation) {

        } else {
            for (var i in $scope.items) {
                var item = $scope.items[i];
                validation = (item['Quantity'] != 0 && item['InventoryItemID'] != undefined);
            }
        }


        if (validation) {
            var conf = confirm('Are you sure to submit this requsition?');
            if (conf) {
                var data = {
                    //RequisitionDate: requisitionDate,
                    RequisitionType: requestType,
                    RequiredDate: requiredDate,
                    SpecialInstruction: instruction,
                    Status: "For Approval",
                    //SupplierID: $scope.supplierIDChoice,
                    LocationID: locationID,
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
                            toastr.success("You've successfully sent your requisition.", "Requisition Sent");
                        }
                    },


                    function errorCallback(response) {

                    });
            }
        } else {
            toastr.warning("Please fill out all fields.", "Data can't be empty");
        }


    }
    //add new item in inventory to database
    $scope.addInventoryItem = function (newItemName, unitOfMeasurementID) {
        var addItemConfirm = confirm('Are you sure to add this new item?');
        if (addItemConfirm) {
            var data = {
                newItemName: newItemName,
                unitOfMeasurementID: (unitOfMeasurementID == null ? 0 : unitOfMeasurementID)
            };
            $http.post('/Requisition/AddNewItem', data)
                .then(
                function successCallback(response) {
                    if (response.data == "ItemExist") {
                        $scope.newItemName = '';
                        toastr.warning("There must be no the same item it must be unique.", "Item is already Exists");
                        //$scope.ctrl.forNewItem = [];
                    } else {
                        $scope.newItemName = '';
                        $scope.initialize();
                        $("#addItemModal").modal("hide");
                        toastr.success("You've successfully added a new item/s in the inventory", "New item created");
                        //$scope.custom = true;
                        //$scope.toggleText = "Existing Item"
                        //$scope.toggleStyle = "btn btn-primary"
                    }
                },
                function errorCallback(response) {
                });
        }
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
                    toastr.success("You've successfully added a new unit of description.", "New Unit of Description is added");
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
    //check if  UnitOfMeasurement(UOM) is existing
    $scope.tempNewUOM;
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

    $scope.isUomSelected = function () {
        $scope.forCheckUOM = true;
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
    //Function to close add new item modal
    $scope.closeAddItemModal = function () {
        $("#addItemModal").modal("hide");
    }


    //Submit Request

    //$scope.submitRequisition = function (requestType, requiredDate, instruction, items, locationID, cnf) {
    //    //if (cnf == "Yes") {
    //    //    status = "Need Supplier"
    //    //} else if (cnf == "No") {
    //    //    status = "For Initial Review"
    //    //}
    //    var data = {
    //        //RequisitionDate: requisitionDate,
    //        RequisitionType: requestType,
    //        RequiredDate: requiredDate,
    //        SpecialInstruction: instruction,
    //        Status: "For Initial Review",
    //        SupplierID: $scope.supplierIDChoice,
    //        LocationID: locationID,
    //        RequisitionItems: items
    //    };
    //    $http.post('/Requisition/AddRequisitionItem', data)
    //        .then(
    //    function successCallback(response) {
    //        if (response.data.length === 0) {
    //            $scope.items = [{ Quantity: 1, Description: "", UnitPrice: 0 }];
    //            $scope.initialize();
    //            $scope.supplierID = "";
    //            $("#confirmModal").modal("hide");
    //            $('#instruction').val('');
    //            alert("Your requesistion has been sent.");
    //        }
    //    },
    //    function errorCallback(response) {
    //    });
    //}

    //$(function () {
    //    $("#toggle-item").change(function () {
    //        if ($(this).prop("checked") == true) {
    //            $scope.toggleItem = true;
    //        } else {
    //            $scope.toggleItem = false;
    //        }
    //    });
    //})

    //$scope.custom = true;
    //$scope.toggleText = "Existing Item"
    //$scope.toggleStyle = "btn btn-primary"
    //$scope.toggleCustom = function () {
    //    $scope.ctrl.forNewItem = [];
    //    $scope.custom = $scope.custom === false ? true : false;
    //    if ($scope.custom == true) {
    //        $scope.toggleText = "Existing Item"
    //        $scope.toggleStyle = "btn btn-primary"
    //    } else {
    //        $scope.toggleText = "New Item"
    //        $scope.toggleStyle = "btn btn-success"
    //    }
    //};

    //function ewan(date)
    //{
    //    date = function () {
    //        $scope.dt = new Date();
    //    };
    //}

    //ewan($scope.date);
    //$scope.today = function () {
    //    $scope.dt = new Date();
    //};
    //$scope.today();

    //$scope.clear = function () {
    //    $scope.dt = null;
    //};

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
