var dashboardApp = require('./dashboardModule');

dashboardApp.controller('lineChartController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/animal/GetLineChartData';
        
    $scope.animals = [];
    $scope.labels = [];
    $scope.series = [];
    $scope.data = [];
    $scope.colors = ['#803690', '#00ADF9', '#FDB45C'];
        
    $scope.options = {
        scales: {
            yAxes: [{
                ticks: {
                    beginAtZero: true
                }
            }]
        }
    };

    $scope.updateList = function () {
        $http.get(serverUrl).then(function (response) {
            $scope.animals = response.data;
            $scope.labels = $scope.animals.Labels;
            $scope.series = $scope.animals.Series;
            $scope.data = [];
            $scope.data.push($scope.animals.Mammal);
            $scope.data.push($scope.animals.Bird);
            $scope.data.push($scope.animals.Fish);            
        });
    };
    $scope.updateList();

}]);