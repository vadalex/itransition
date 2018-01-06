require('angular');

module.exports = angular.module('TodoListApp', []).controller('TodoListController', ['$scope', '$http', function ($scope, $http) {
    var serverUrl = 'http://localhost/CSTraining.WebUI/api/todo/';

    $scope.updateTodoList = function () {
        $http.get(serverUrl).then(function (response) { $scope.items = response.data });
    };

    $scope.updateTodoList();

    $scope.getTotalTodos = function () {
        if (typeof $scope.items === "undefined")
            return 0;        
        return $scope.items.filter(function (value) { return !value.IsDone; }).length;
    };

    $scope.addTodo = function () {
        if (!$scope.formTodoText) return;
        var item = {};
        item.TodoText = $scope.formTodoText;
        item.IsDone = false;
        item.Order = 1;
        $scope.formTodoText = '';
        $http.post(serverUrl, JSON.stringify(item))
            .then(function () { $scope.updateTodoList(); });
    };

    $scope.deleteTodo = function () {
        $http.delete(serverUrl + this.item.TodoItemId)
            .then(function () { $scope.updateTodoList(); });
    }

    $scope.updateTodo = function () {
        $http.put(serverUrl, this.item);
    }
}]);