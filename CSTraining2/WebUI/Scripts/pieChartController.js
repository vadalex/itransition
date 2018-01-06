var dashboardApp = require('./dashboardModule');

dashboardApp.controller('pieChartController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/animal/GetPieChartData';

    $scope.animals = [];
    $scope.labels = [];
    $scope.data = [];


    $scope.updateList = function () {
        $http.get(serverUrl).then(function (response) {
            $scope.animals = response.data;
            $scope.labels = [];
            $scope.data = [];
            for (i = 0; i < $scope.animals.length; i++) {
                $scope.labels.push($scope.animals[i].Class);
                $scope.data.push($scope.animals[i].Count);
            }
        });        
    };
    $scope.updateList();    
}]);