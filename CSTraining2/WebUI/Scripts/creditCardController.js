var dashboardApp = require('./dashboardModule');

dashboardApp.controller('creditCardController', ['$scope', '$http', '$filter', 'NgTableParams', function ($scope, $http, $filter, NgTableParams) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/creditcard/';

    $scope.updateList = function () {
        $http.get(serverUrl).then(function (response) {
            $scope.creditcards = response.data;
            $scope.tableParams = new NgTableParams({}, {
                paginationMaxBlocks: 8,
                total: $scope.creditcards.length,
                getData: function (params) {
                    $http.get(serverUrl).then(function (response) {
                        $scope.creditcards = response.data
                        $scope.data = params.sorting() ? $filter('orderBy')($scope.creditcards, params.orderBy()) : $scope.creditcards;
                        $scope.data = params.filter() ? $filter('filter')($scope.data, params.filter()) : $scope.data;
                        params.total($scope.data.length);
                        $scope.data = $scope.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        params.data = $scope.data;
                    });
                }
            });
        });
    };
    
    $scope.del = function (card) {        
        $http.delete(serverUrl + 'DeleteCreditCard/' + card.CreditCardId).then(function () { card.removed = true }, function (response) { alert('something is wrong') });
    }

    $scope.save = function (card) {
        $http.put(serverUrl + 'PutCreditCard', card).then(function () { card.btnClass = 'btn-success'; }, function (response) { alert('something is wrong') });
    }

    $scope.updateList();
}]);