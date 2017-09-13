app.controller("myCtrl", function ($scope, $http) {
    var vm = this;
    $scope.isItemExist;

    $scope.initialize = function () {
        $scope.page;
        $scope.loadpage(1, true);
        $scope.items = [{
            UnitOfMeasurement: '',
            UnitPrice: 0
        }];//Initialize default item/
    }

    $scope.pageChange = function (page) {
        $scope.page = page;
        $scope.loadpage(page.PageNumber, page.PageStatus);
    }

    $scope.loadpage = function (pagenumber, pagestatus) {
        var data = {
            pagenumber: pagenumber,
            pagestatus: pagestatus,
        };
        $http.post("/Reviewer/LoadAllSupplierPageData", data).then(
            function successCallback(response) {
                $scope.suppliers = response.data;
                $scope.reloadAllItem();
            }
        );
        $http.post('/Reviewer/LoadSupplierPage', data).then(
        function successCallback(response) {
            $scope.pages = response.data;
            if (!$scope.page) {
                $scope.page = $scope.pages[Object.keys($scope.pages)[0]];
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
    //add item options
    $scope.addNewItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push({
            UnitOfMeasurement: '',
            UnitPrice: 0
        });
    };
    //remove item options
    $scope.removeItem = function (index) {
        $scope.items.splice(index, 1);
    };

    //Display supplier info modal
    $scope.showSupplierInformation = function (supplier) {
        $scope.supplierInfo = angular.copy(supplier);
        var data = {
            Supplier: supplier
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
    $scope.closeSupplierInformation = function () {
        $("#supplierInfoModal").modal("hide");
    }

    //Show supplier Provider modal
    $scope.showSupplierProvider = function () {
        $("#provideSupplierModal").modal("show");
    }
    //Close Supplier Provider modal
    $scope.closeSupplierProvider = function () {
        $("#provideSupplierModal").modal("hide");
    }
    //Add new Supplier
    $scope.addSupplierProvider = function (tinNumber, supplierName, address, contactPerson, contactNo, email, hasVAT) {
        if ($scope.tinNumber == undefined || $scope.supplierName == undefined || $scope.address == undefined || $scope.contactPerson == undefined || $scope.contactNo == undefined || $scope.email == undefined) {
            toastr.warning("There must be no empty fileds all are important.", "You must fill out all the fileds");
        } else {
            var data =
         {
             TinNumber: tinNumber,
             SupplierName: supplierName,
             Address: address,
             ContactPerson: contactPerson,
             ContactNo: contactNo,
             Email: email,
             Vatable: hasVAT
             //requisitionID: $scope.requisition.RequisitionID
         };
            $http.post("/Reviewer/AddSupplier", data).then(
                function successCallback(response) {
                    $scope.supplier = response.data;
                    //$scope.requisition.SupplierID = $scope.supplier.SupplierId;
                    //$scope.hasSupplier = true;
                    $scope.initialize();
                    toastr.success("You've successfully created a new supplier.", "Supplier successfully added");
                    $("#provideSupplierModal").modal("hide");
                },
                function errorCallback(response) {

                }
            );
        }
    }

    //Update SUpplier's Item Unit Price
    $scope.updateUnitPrice = function (updateUnitPriceItems) {
        var addItemConfirm = confirm('Are you sure about the item\'s unit price?');
        if (addItemConfirm) {
            var data = {
                SupplierID: $scope.supplierInfo.SupplierID,
                supplierItemList: updateUnitPriceItems
            };
            $http.post('/Reviewer/UpdateSupplierItems', data)
                .then(
            function successCallback(response) {
                $scope.initialize();
                toastr.success("The supplier item's unit price is now updated.", "Unit price successfully updated");
                $("#updateUnitPriceModal").modal("hide");
                $scope.showSupplierInformation($scope.supplierInfo);
                $("#supplierInfoModal").modal("show");
            },
            function errorCallback(response) {
            });
        }
    }
    //Show update Unit Price modal
    $scope.showUpdateUnitPriceModal = function (supplierItems) {
        $scope.updateUnitPriceItems = angular.copy(supplierItems);
        //$("#supplierInfoModal").modal("hide");
        $("#updateUnitPriceModal").modal("show");

    }
    //close update unit price modal
    $scope.closeUpdateUnitPrice = function () {
        $("#updateUnitPriceModal").modal("hide");
        $("#supplierInfoModal").modal("show");
    }

    //Show Update Details modal
    $scope.updateSupplierDetailsModal = function (supplierDetails) {
        $scope.supplierDetails = angular.copy(supplierDetails);
        $("#updateSupplierDetailsModal").modal("show");
    }
    //Close Update Details modal
    $scope.closeUpdateSupplierDetailsModal = function () {
        $("#updateSupplierDetailsModal").modal("hide");
    }
    //Function to update supplier details
    $scope.updateSupplierDetails = function (supplierDetails) {
        var data = {
            supplier: supplierDetails
        }
        $http.post("/Reviewer/UpdateSupplierDetails", data).then(
               function successCallback(response) {
                   $scope.supplier = response.data;
                   //$scope.requisition.SupplierID = $scope.supplier.SupplierId;
                   //$scope.hasSupplier = true;
                   $scope.initialize();
                   toastr.success("You've successfully updated the details of the supplier.", "Supplier details is now updated");
                   $("#updateSupplierDetailsModal").modal("hide");
               },
               function errorCallback(response) {

               }
           );
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
    //add new item in inventory to database
    $scope.addInventoryItem = function (newItemName, unitOfMeasurementID, newItemCode) {
        var addItemConfirm = confirm('Are you sure to add this new item?');
        if (addItemConfirm) {
            var data = {
                newItemName: newItemName,
                newItemCode: newItemCode,
                unitOfMeasurementID: (unitOfMeasurementID == null ? 0 : unitOfMeasurementID)
            };
            $http.post('/Requisition/AddNewItem', data)
                .then(
            function successCallback(response) {
                if (response.data == "ItemExist") {
                    $scope.newItemName = '';
                    $scope.newItemCode = '';
                    toastr.warning("There must be no the same item it must be unique.", "Item is already Exists");
                    //$scope.ctrl.forNewItem = [];
                } else {
                    $scope.newItemName = '';
                    $scope.newItemCode = '';
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


    //Display add new supplier item modal
    $scope.showAddNewItem = function () {
        $scope.items = [{
            UnitOfMeasurement: '',
            UnitPrice: 0
        }];//Initialize default item/
        $("#addSupplierItemModal").modal("show");
        //$("#supplierInfoModal").modal("hide");
    }
    //Close add supplier item modal
    $scope.closeAddSupplierItem = function () {
        $scope.items = [{
            UnitOfMeasurement: '',
            UnitPrice: 0
        }];//Initialize default item/
        $("#addSupplierItemModal").modal("hide");
        $("#supplierInfoModal").modal("show");
    }
    //Add new supplier item
    $scope.addNewSupplierItem = function () {

        var validation = true;
        for (var i in $scope.items) {
            var item = $scope.items[i];
            validation = (item['Quantity'] != 0 && item['UnitOfMeasurement'] != "");
        }

        if (!validation) {
            toastr.warning("Please fill out all fields.", "Data cannot be empty.");
        } else {
            var addItemConfirm = confirm('Add new item to this supplier?');
            if (addItemConfirm) {
                var data = {
                    SupplierID: $scope.supplierInfo.SupplierID,
                    supplierItemList: $scope.items
                };
                $http.post('/Reviewer/AddSupplierItem', data)
                    .then(
                function successCallback(response) {
                    if (response.data === true) {
                        $scope.unitPrice = 0;
                        toastr.warning("There must be no the same item it must be unique.", "Supplier item is already Exists");
                        //$scope.ctrl.forNewItem = [];
                    } else {
                        $scope.unitPrice = 0;
                        $scope.initialize();
                        $scope.items = [{ UnitOfMeasurement: '', UnitPrice: 0 }];//Initialize default item/
                        toastr.success("You've successfully added a new item/s in this supplier.", "New item added to supplier");
                        $("#addSupplierItemModal").modal("hide");
                        $scope.showSupplierInformation($scope.supplierInfo);
                        $("#supplierInfoModal").modal("show");
                    }
                },
                function errorCallback(response) {
                });
            }
        }

    }
    //close supplier item modal
    $scope.closeSupplierItemModal = function () {
        $("#addSupplierItemModal").modal("hide");
    }


    //Display UOM by selecting in combo box
    $scope.setItemUOM = function (item, unitOfMeasure) {
        item.UnitOfMeasurement = unitOfMeasure;
        $scope.isItemExist = true;
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
            $scope.isItemExist = true;
        } else {
            $scope.isItemExist = false;
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

    $scope.isUomSelected = function () {
        $scope.forCheckUOM = true;
    }
});