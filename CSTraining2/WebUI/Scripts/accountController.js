var dashboardApp = require('./dashboardModule');

dashboardApp.controller('accountController', ['$scope', '$http', '$filter', 'NgTableParams', function ($scope, $http, $filter, NgTableParams) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/account';
    $scope.accounts = [];

    $scope.updateList = function () {
        $http.get(serverUrl).then(function (response) {
            $scope.accounts = response.data;
            $scope.tableParams = new NgTableParams({
                sorting: {
                    Username: 'asc'
                },
            }, {
                paginationMaxBlocks: 8,
                total: $scope.accounts.length,
                getData: function (params) {
                    /*
                    $scope.data = params.sorting() ? $filter('orderBy')($scope.accounts, params.orderBy()) : $scope.accounts;
                    $scope.data = params.filter() ? $filter('filter')($scope.data, params.filter()) : $scope.data;
                    params.total($scope.data.length);
                    $scope.data = $scope.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                    params.data = $scope.data;
                    */
                    $http.get(serverUrl).then(function (response) {
                        $scope.accounts = response.data
                        $scope.data = params.sorting() ? $filter('orderBy')($scope.accounts, params.orderBy()) : $scope.accounts;
                        $scope.data = params.filter() ? $filter('filter')($scope.data, params.filter()) : $scope.data;
                        params.total($scope.data.length);
                        $scope.data = $scope.data.slice((params.page() - 1) * params.count(), params.page() * params.count());
                        params.data = $scope.data;
                    });
                }
            });
        });
    };
    $scope.updateList();
}]);