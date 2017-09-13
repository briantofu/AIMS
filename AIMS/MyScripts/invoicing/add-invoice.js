app.controller('myCtrl', function ($scope, $http, $timeout, $interval) {

    $scope.items = [{ Quantity: 0, ItemNo: 0, Description: "", UnitPrice: 0, Discount: 0, LineTotal: 0 }];//Initialize default item

    //Add new item
    $scope.addNewItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push({ Quantity: 0, ItemNo: 0, Description: "", UnitPrice: 0, Discount: 0, LineTotal: 0 });
    };

    //Remove item
    $scope.removeItem = function () {
        var lastItem = $scope.items.length - 1;
        $scope.items.splice(lastItem);
        $scope.computation();
    };


    $scope.compute = function () {
        $scope.computation()
    }

    $scope.computation = function () {
        $scope.subtotal = 0;
        for (var i in $scope.items) {
            var x = $scope.items[i];
            $scope.subtotal += x["LineTotal"];
        }
        $scope.salestax = $scope.subtotal * .12;
        $scope.total = $scope.subtotal + $scope.salestax;
    }
    $scope.submitPost = function () {
        alert("Bul");
    };
    $scope.SaveDownloadInvoice = function (customerName, customerID, address, tinNo, phone, invoicePeriod, invoiceDate, dueDate, items, accountName, usdAccountNo, bankName, bankAddress, swiftCode) {
        var data = {
            ClientName: customerName,
            ClientID: customerID,
            Address: address,
            ContactNo: phone,
            TinNo: tinNo,
            ClientItemList: items,
            Subtotal: $scope.subtotal,
            Salestax: $scope.salestax,
            Total: $scope.total,

            InvoiceDate: invoiceDate,
            DueDate: dueDate,
            InvoicePeriod: invoicePeriod,
            AccountName: accountName,
            USDAccountNo: usdAccountNo,
            BankName: bankName,
            BankAddress: bankAddress,
            SwiftCode: swiftCode

        };
        $http.post('/Invoicing/DownloadPdf', data)
                    .then(window.open('/Invoicing/InvoicePdf'));
        $("#confirmModal").modal("hide");
    }
    $scope.SaveInvoice = function (customerName, customerID, address, tinNo, phone, invoicePeriod, invoiceDate, dueDate, items, accountName, usdAccountNo, bankName, bankAddress, swiftCode) {
        var data = {
            ClientName: customerName,
            ClientID: customerID,
            Address: address,
            ContactNo: phone,
            TinNo: tinNo,
            ClientItemList: items,
            Subtotal: $scope.subtotal,
            Salestax: $scope.salestax,
            Total: $scope.total,

            InvoiceDate: invoiceDate,
            DueDate: dueDate,
            InvoicePeriod: invoicePeriod,
            AccountName: accountName,
            USDAccountNo: usdAccountNo,
            BankName: bankName,
            BankAddress: bankAddress,
            SwiftCode: swiftCode

        };
        $http.post('/Invoicing/SavePdf', data)
             .then(
            function successCallBack() {
                alert("Invoice Save!");
                $("#confirmModal").modal("hide");
            }
        );
    }

    $scope.showConfirmModal = function () {
        $("#confirmModal").modal("show");
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
        $scope.dateOptions.minDate = new Date();
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