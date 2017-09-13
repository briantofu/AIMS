app.controller("myCtrl", function ($scope, $http) {
    $scope.requisition;
    $scope.initialize = function () {
        var data =
           {
               Status: "Declined from approval"
           };
        $http.post('/Reviewer/ViewAllRequisition', data).then(
            function successCallback(response) {
                $scope.requisitions = response.data;
                var text = "";
                if ($scope.requisitions.length > 1) {
                    text = 'Your requests have been declined. Click here to view the details...';
                } else {
                    text = 'Your request has been declined. Click here to view the details...';
                }
                if ($scope.requisitions.length != 0) {
                    PNotify.desktop.permission();
                    (new PNotify({
                        title: 'New Notification',
                        text: text,
                        desktop: {
                            desktop: true,
                        }
                    })).get().click(function (e) {
                        PNotify.removeAll();
                        if ($('.ui-pnotify-closer, .ui-pnotify-sticker, .ui-pnotify-closer *, .ui-pnotify-sticker *').is(e.target)) return;
                        alert('Declined Requests');
                    });
                }
            },
            function errorCallback(response) {

            });
    }
    $scope.showViewModal = function (requisition) {
        $scope.requisition = angular.copy(requisition);
        var data =
            {
                RequisitionID: requisition.RequisitionID,
                SupplierID: requisition.SupplierID
            };
        $http.post("/Reviewer/RequisitionItem", data).then(
            function successCallback(response) {
                $scope.requisitionItems = response.data;
                $("#viewModal").modal("show");
            },
            function errorCallback(response) {

            }
        );
    }
    //Show decline modal
    $scope.showDeclineModal = function () {

        $("#declineModal").modal("show");
        $("#viewModal").modal("hide");
    }
    //Show decline modal
    $scope.closeDeclineModal = function () {
        $("#declineModal").modal("hide");
        $("#viewModal").modal("show");
    }
    //Reject requisition
    $scope.declineFunction = function (requisitionID, reasonForDeclining) {
        var declineConfirm = confirm('Are you sure to decline this requisition?');
        if (declineConfirm) {
            var data =
          {
              requisitionID: requisitionID,
              Status: "Declined",
              ReasonForDeclining: reasonForDeclining


          };
            $http.post("/Reviewer/DeclineRequisition", data).then(
                function successCallback(response) {
                    $scope.initialize();
                    $scope.requisitionItems = response.data;
                    alert('Requisition successfully declined.');
                    $("#declineModal").modal("hide");
                },
                function errorCallback(response) {
                }
            );
        }
    }
    //Accept requisition
    $scope.acceptFunction = function (requisitionID, requisitionItems) {
        var declineConfirm = confirm('Are you sure to accept this requisition?');
        var data =
            {
                requisitionID: requisitionID,
                requisitionItems: requisitionItems,
                Status: "For Approval"
            };
        $http.post("/Reviewer/AcceptRequisition", data).then(
            function successCallback(response) {
                $scope.initialize();
                $scope.requisitionItems = response.data;
                alert('Requisition has been accepted.');
                $("#viewModal").modal("hide");
            },
            function errorCallback(response) {
            }
        );
    }
    $scope.items = [];//Initialize default item
    //Show Update requisition modal
    $scope.showUpdateModal = function (requiredDate, items) {
        $scope.requiredDate = requiredDate;
        $scope.items = [];//Initialize default item
        for (var i in items) {
            var item = items[i];
            $scope.items.push({
                InventoryItemID: item['InventoryItemID'],
                ItemName: "" + item['ItemName'],
                Quantity: item['Quantity'],
                Description: "" + item['Description'],
                UnitOfMeasurement: item['UnitOfMeasurement'],
                UnitPrice: item['UnitPrice']
            });
        }
        $("#viewModal").modal("hide");
        $("#updateModal").modal("show");
    }
    //Execute update requisition
    $scope.updateRequisitionFunction = function (requisitionID, requiredDate, items) {
        var data =
            {
                RequisitionID: requisitionID,
                RequiredDate: requiredDate,
                RequisitionItems: items
            };
        $http.post("/Reviewer/UpdateRequisition", data).then(
            function successCallback(response) {
                $scope.initialize();
                $scope.requisitionItems = response.data;
                $("#updateModal").modal("hide");
            },
            function errorCallback(response) {
            }
        );
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
        $scope.dateOptions.minDate = new Date();
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.dateOptions.minDate = new Date();
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