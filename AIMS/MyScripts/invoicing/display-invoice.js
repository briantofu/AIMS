app.controller("myCtrl", function ($scope, $http) {
    $scope.initialize = function () {
        $http.post('/Invoicing/DisplayInvoice').then(
            function successCallback(response) {
                $scope.invoices = response.data;
            },
            function errorCallback(response) {

            });
    }
    $scope.computation = function (invoice) {
        $scope.subtotal = 0;
        for (var i in invoice.ClientItemList) {
            var x = invoice.ClientItemList[i];
            $scope.subtotal += x["LineTotal"];
        }
        $scope.salestax = $scope.subtotal * .12;
        $scope.total = $scope.subtotal + $scope.salestax;
    }

    $scope.viewInvoice = function (invoice) {
        $scope.invoiceInfo = angular.copy(invoice);
        $scope.computation($scope.invoiceInfo);
        $("#viewInvoiceModal").modal("show");
    }

    $scope.closeViewInvoiceModal = function () {
        $("#viewInvoiceModal").modal("hide");
    }

    $scope.download = function (invoiceInfo) {
        $scope.computation(invoiceInfo);
        invoiceInfo.Subtotal = $scope.subtotal;
        invoiceInfo.Salestax = $scope.salestax;
        invoiceInfo.Total = $scope.total;
        invoiceInfo.DueDate = ConvertJsonDateString(invoiceInfo.DueDate);
        invoiceInfo.InvoiceDate = ConvertJsonDateString(invoiceInfo.InvoiceDate);
        var data = invoiceInfo;
        $http.post('/Invoicing/DownloadPdfFromView', data)
                   .then(window.open('/Invoicing/InvoicePdf'));
        $("#viewInvoiceModal").modal("hide");
    }

    $scope.download2 = function (invoiceInfo) {
        $scope.computation(invoiceInfo);
        invoiceInfo.Subtotal = $scope.subtotal;
        invoiceInfo.Salestax = $scope.salestax;
        invoiceInfo.Total = $scope.total;
        invoiceInfo.DueDate = ConvertJsonDateString(invoiceInfo.DueDate);
        invoiceInfo.InvoiceDate = ConvertJsonDateString(invoiceInfo.InvoiceDate);

        var data = invoiceInfo;
        $http.post('/Invoicing/DownloadPdfFromView', data)
                   .then(window.open('/Invoicing/InvoicePdf'));
        $("#viewInvoiceModal").modal("hide");
    }

    function ConvertJsonDateString(jsonDate) {
        var shortDate = null;
        if (jsonDate) {
            var regex = /-?\d+/;
            var matches = regex.exec(jsonDate);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
        }
        return shortDate;
    };
});