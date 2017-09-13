app.controller("myCtrl", function ($scope, $http) {
    var vm = this;
    $scope.hasSupplier;
    $scope.tempSupplierId;
    $scope.deliveryCharge = 0;
    $scope.lineTotal = function (quantity, unitprice) {
        return (quantity * parseFloat(unitprice))
    }
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
            pagestatus: pagestatus,
            status: "For Purchasing"
        };
        $http.post("/Reviewer/LoadPageData", data).then(
            function successCallback(response) {
                $scope.requisitions = response.data;
                vm.existingSuppliers = [];//initialize array of Suppliers = [{ SupplierName: 'No Supplier', SupplierID: 0 }];
                $scope.loadSupplier();
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
                //        alert('Declined Requests');
                //    });
                //}
            },
            function errorCallback(response) {
            }
        );
        $http.post('/Reviewer/LoadPages', data).then(
        function successCallback(response) {
            $scope.pages = response.data;
            if (!$scope.page) {
                $scope.page = $scope.pages[Object.keys($scope.pages)[0]];
            }
        },
        function errorCallback(response) {
        });
    }
    //show requisition item
    $scope.showViewModal = function (requisition) {
        $scope.allSelected = false;
        $scope.requisition = angular.copy(requisition);
        var data =
            {
                requisition: $scope.requisition
            };
        $http.post("/Reviewer/RequisitionItem", data).then(
            function successCallback(response) {
                $scope.requisitionItems = response.data;
                //$scope.toggleAll();
                $scope.overTotal = 0;
                for (var item in $scope.requisitionItems) {
                    var x = $scope.requisitionItems[item];
                    $scope.overTotal += (x["Quantity"] * x["UnitPrice"]);
                    //x["isItemSelected"] = true;
                }
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {

            }
        );
    }

    //Display all existing supplier 
    $scope.loadSupplier = function () {
        $http.post('/Reviewer/DisplayCompanyName')
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

    //Add supplier
    //$scope.addSupplierProvider = function (tinNumber, supplierName, address, contactPerson, contactNo, email) {
    //    if ($scope.tinNumber == undefined || $scope.supplierName == undefined || $scope.address == undefined || $scope.contactPerson == undefined || $scope.contactNo == undefined || $scope.email == undefined) {
    //        toastr.warning("All fields are important. ","There must be no emty fields");
    //    } else {
    //        var data =
    //     {
    //         TinNumber: tinNumber,
    //         SupplierName: supplierName,
    //         Address: address,
    //         ContactPerson: contactPerson,
    //         ContactNo: contactNo,
    //         Email: email,
    //         requisitionID: $scope.requisition.RequisitionID
    //     };
    //        $http.post("/Reviewer/AddSupplier", data).then(
    //            function successCallback(response) {
    //                $scope.supplier = response.data;
    //                $scope.requisition.SupplierID = $scope.supplier.SupplierId;
    //                $scope.hasSupplier = true;
    //                $scope.initialize();
    //                alert('Supplier has been added.');
    //                $("#provideSupplierModal").modal("hide");
    //                $("#viewModal").modal("show");
    //            },
    //            function errorCallback(response) {
    //            }
    //        );
    //    }
    //}

    //Display supplier info modal
    $scope.SupplierInformation = function (item) {
        $scope.supplierInfo = item;
        $("#supplierInfoModal").modal("show");
    }

    //Close supplier info modal
    $scope.closeSupplierInfo = function () {
        $("#supplierInfoModal").modal("hide");
    }

    //Show decline modal
    $scope.showDeclineModal = function () {
        $("#viewModal").modal("hide");
        $("#declineModal").modal("show");
    }

    //Close decline modal
    $scope.closeDeclineModal = function () {

        $("#declineModal").modal("hide");
        $("#viewModal").modal("show");
    }

    //Show supplier Provider modal
    $scope.showSupplierProvider = function () {
        $("#viewModal").modal("hide");
        $("#provideSupplierModal").modal("show");
    }

    //Close Supplier Provider modal
    $scope.closeSupplierProvider = function () {
        $("#provideSupplierModal").modal("hide");
    }

    $scope.closeUpdateUnitPriceModal = function () {
        $("#updateUnitPriceModal").modal("hide");
        $("#viewModal").modal("show");
    }

    //Decline modal function
    $scope.declineFunction = function (requisitionID, reason) {
        var declineConfirm = confirm('Are you sure to decline this requisition?');
        if (declineConfirm) {
            var data =
      {
          requisitionID: requisitionID,
          ReasonForDeclining: reason,
          Status: "Declined"
      };
            $http.post("/Reviewer/DeclineRequisition", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    $scope.requisitionItems = response.data;
                    alert('Requisition successfully declined.')
                    $("#declineModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }

    }

    //Accept requisition
    $scope.submitFunction = function (requisitionID, requisitionItems, deliveryCharge) {
        var validation = true;
        //for (var i in requisitionItems) {
        //    var item = requisitionItems[i];
        //    validation = (item['UnitPrice'] != 0);
        //}
        if (validation) {
            if ($scope.tempSupplierId == undefined) {
                var conf = confirm('Approve this requisition without supplier?');
                if (conf) {
                    var data = {
                        requisitionID: requisitionID,
                        requisitionItems: requisitionItems,
                        supplierId: $scope.tempSupplierId,
                        deliveryCharges: deliveryCharge,
                        Status: "Approved"
                    };
                    $http.post("/Reviewer/AcceptRequisition", data).then(
                        function successCallback(response) {
                            $scope.isSuccess = response.data;
                            if ($scope.isSuccess == "success") {
                                $scope.initialize();
                                toastr.success("The requsition will sent back to the requestor for some process.", "Requisition has been approved");
                                $("#viewModal").modal("hide");
                            } else {
                                toastr.error("Supplier is already assigned on this requisition. Please choose another.", "Data can't be proccess.");
                            }
                        },
                        function errorCallback(response) {
                        }
                    );
                }
            } else {
                var acceptConfirm = confirm('Are you sure to approve this requisition?');
                if (acceptConfirm) {
                    var data =
                                {
                                    requisitionID: requisitionID,
                                    requisitionItems: requisitionItems,
                                    supplierId: $scope.tempSupplierId,
                                    deliveryCharges: deliveryCharge,
                                    Status: "Approved"
                                };
                    $http.post("/Reviewer/AcceptRequisition", data).then(
                        function successCallback(response) {
                      
                            $scope.isSuccess = response.data;
                            if ($scope.isSuccess == "success") {
                                $scope.initialize();
                                toastr.success("The requsition will sent back to the requestor for some process.", "Requisition has been approved");
                                $("#viewModal").modal("hide");

                            } else {
                                toastr.error("Supplier is already assigned on this requisition. Please choose another.", "Data can't be proccess.");
                            }
                        },
                        function errorCallback(response) {
                        }
                    );
                }
            }
        } else {
            toastr.warning("There must be no zero (0) value of unit price.", "Invalid Unit Price");
        }



    }

    //Show Update requisition modal
    $scope.showUpdateUnitPriceModal = function (items) {
        //$scope.requiredDate = new Date(requiredDate);
        $scope.items = [];//Initialize default item
        for (var i in items) {
            var item = items[i];
            if (item['UnitPrice'] == 0) {
                $scope.items.push({
                    RequisitionItemID: item['RequisitionItemID'],
                    InventoryItemID: item['InventoryItemID'],
                    ItemName: "" + item['ItemName'],
                    Quantity: item['Quantity'],
                    Description: "" + item['Description'],
                    UnitOfMeasurement: item['UnitOfMeasurement'],
                    UnitPrice: item['UnitPrice']
                });
            }
        }
        $("#viewModal").modal("hide");
        $("#updateUnitPriceModal").modal("show");
    }

    //Execute update requisition
    $scope.UpdateUnitPriceFunction = function (requisitionID, items) {
        var con = confirm('Are you sure about the unit price.');
        if (con) {
            var data =
                {
                    RequisitionID: requisitionID,
                    //RequiredDate: requiredDate,
                    RequisitionItems: items
                };
            $http.post("/Reviewer/UpdateUnitPrice", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    //$scope.requisitionItems = response.data;
                    $scope.showViewModal($scope.requisition);
                    alert('Unit price has been updated.');
                    $("#updateUnitPriceModal").modal("hide");
                    $("#viewModal").modal("show");
                },
                function errorCallback(response) {
                }
            );
        }
    }

    //Display all supplier item by selecting in combo box
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
               $scope.items = [{ Quantity: 1, Description: "", UnitOfMeasurement: "Select from items" }];//Initialize default item
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

    //Function if supplier cbox is selected
    $scope.isSupplierSelect = function (supplierId) {

        $scope.hasSupplier = true;
        $scope.tempSupplierId = supplierId;
        var data = {
            SupplierId: supplierId
        }
        $http.post("/Reviewer/SelectUnitPriceViaSupplier", data).then(
                function successCallback(response) {
                    $scope.supplierx = response.data;
                    $scope.overTotal = 0;
                    for (var x = 0; x < $scope.requisitionItems.length; x++) {
                        if ($scope.requisitionItems[x].PurchaseOrderId == 0) {
                            $scope.requisitionItems[x].UnitPrice = 0;
                        }
                    }

                    for (var x = 0; x < $scope.requisitionItems.length ; x++) {
                        for (var i = 0; i < $scope.supplierx[0].supplierItemList.length; i++) {
                            if ($scope.requisitionItems[x].isItemSelected == true) {
                                if ($scope.supplierx[0].supplierItemList[i].InventoryItemID == $scope.requisitionItems[x].InventoryItemID) {
                                    $scope.requisitionItems[x].UnitPrice = $scope.supplierx[0].supplierItemList[i].UnitPrice;
                                    break;
                                }
                            } 
                        }
                    }

                    for (var i = 0; i < $scope.requisitionItems.length; i++) {
                        if ($scope.requisitionItems[i].UnitPrice == 0) {
                            $scope.requisitionItems[i].isItemSelected = false;
                        } else {
                            $scope.overTotal += ($scope.requisitionItems[i].Quantity * $scop$scope.requisition.RequisitionIDe.requisitionItems[i].UnitPrice);
                        }
                    }

                },
                function errorCallback(response) {
                }
            );
    }

    //Download a pdf
    $scope.downloadPdf = function (supplierId) {
        var isOkay = true;
        if (supplierId != undefined) {
            //for (var i = 0; i < $scope.requisitionItems.length; i++) {
            //    if ($scope.requisitionItems[i].UnitPrice == 0) {
            //        isOkay = false;
            //    }
            //}
            //if (isOkay) {
            var data = {
                SupplierID: $scope.supplierx[0].SupplierID,
                SupplierName: $scope.supplierx[0].SupplierName,
                SupplierAddress: $scope.supplierx[0].Address,
                ContactPerson: $scope.supplierx[0].ContactPerson,
                ContactNo: $scope.supplierx[0].ContactNo,
                SupplierEmail: $scope.supplierx[0].Email,
                Vatable: $scope.supplierx[0].Vatable,
                RequisitionID: $scope.requisition.RequisitionID,
                LocationName: $scope.requisition.LocationName,
                LocationAddress: $scope.requisition.LocationAddress,
                RequiredDate: $scope.requisition.RequiredDateString,
                RequisitionItems: $scope.requisitionItems,
                DeliveryCharge: $scope.deliveryCharge
            }
            $http.post('/Reviewer/DownloadPdf', data)
                 .then(window.open('/Reviewer/PurchaseOrderPDF'));

        } else {
            toastr.warning("There must be no zero (0) value of unit price.", "Invalid Unit Price");
        }
        //} else {
        //    toastr.warning("There are no supplier that is selected. Please select one.", "Please select supplier");
        //}


    }

    $scope.allSelected = false;
    //Check specific checkbox
    $scope.cbChecked = function () {

        $scope.allSelected = true;
        angular.forEach($scope.requisitionItems, function (v, k) {
            if (!v.isItemSelected) {
                $scope.allSelected = false;
                $scope.isSupplierSelect($scope.tempSupplierId);
            } else {
                $scope.isSupplierSelect($scope.tempSupplierId);
            }
        });
    }
    //Check all checkbox
    $scope.toggleAll = function () {
        var bool = true;
        if ($scope.allSelected) {
            bool = false;
        }
        angular.forEach($scope.requisitionItems, function (v, k) {
            if (v.PurchaseOrderId == 0) {
                v.isItemSelected = !bool;
                $scope.allSelected = !bool;
            }

            if (!v.isItemSelected) {
                $scope.isSupplierSelect($scope.tempSupplierId);
            } else {
                $scope.isSupplierSelect($scope.tempSupplierId);
            }
        });
    }


    //Function to show supplier information
    //$scope.showSupplierInfo = function (supplierData) {
    //    $scope.supplierInfo = [];
    //    for (var i in supplierData) {
    //        var suppInfo = supplierData[i];
    //        if (suppInfo['SupplierID'] === $scope.supplierIDChoice) {
    //            $scope.supplierInfo = [{
    //                SupplierName: "" + suppInfo['SupplierName'],
    //                Address: suppInfo['Address'],
    //                ContactPerson: suppInfo['ContactPerson'],
    //                ContactNo: suppInfo['ContactNo'],
    //                Email: suppInfo['Email'],
    //                SupplierID: suppInfo['SupplierID'],
    //            }];
    //            $("#supplierInfoModal").modal("show");
    //        }
    //    }
    //}

    //Check if supplier is selected
    //$scope.isSupplierExist = function (value) {
    //    if (checkSuppliers(value)) {
    //        $scope.hasSupplier = true;
    //    } else {
    //        $scope.hasSupplier = false;
    //    }
    //}

    //function checkSuppliers(data) {
    //    return vm.existingSuppliers.some(function (el) {
    //        return el.SupplierName.toLowerCase().includes(data.toLowerCase());
    //    });
    //}
});