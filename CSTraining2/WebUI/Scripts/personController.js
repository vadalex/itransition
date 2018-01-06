var dashboardApp = require('./dashboardModule');

dashboardApp.controller('personController', ['$scope', '$http', '$filter', 'NgTableParams', '$uibModal', '$log', function ($scope, $http, $filter, NgTableParams, $uibModal, $log) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/person/';

    $scope.updateList = function () {
        $http.get(serverUrl).then(function (response) {
            $scope.people = response.data;
            $scope.tableParams = new NgTableParams({}, {
                paginationMaxBlocks: 8,
                total: $scope.people.length,
                getData: function (params) {
                    $http.get(serverUrl).then(function (response) {
                        $scope.people = response.data
                        $scope.data = params.sorting() ? $filter('orderBy')($scope.people, params.orderBy()) : $scope.people;
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

    var $ctrl = this;    

    $ctrl.save = function (person) {
        $http.put(serverUrl + 'PutPerson', person).then(function () { }, function (response) { alert('something is wrong') });
    }

    $ctrl.open = function (person) {        
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            controllerAs: '$ctrl',            
            appendTo: undefined,
            resolve: {
                person: function () {
                    return person;
                }
            }
        });

        modalInstance.result.then(function (person) {
            $ctrl.save(person);
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };    
}]);

dashboardApp.controller('ModalInstanceCtrl', function ($uibModalInstance, person) {
    var $ctrl = this;
    $ctrl.person = person;    

    $ctrl.ok = function () {
        $uibModalInstance.close($ctrl.person);
    };

    $ctrl.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});