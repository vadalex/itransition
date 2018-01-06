var todoListApp = angular.module('TodoListApp', []);

todoListApp.controller('TodoListController', function($scope, $http) {
    $scope.updateTodoList = function() {
        $http.get('/todoservice/api/values')
            .success(function(data) { $scope.items = data });
    };

    $scope.updateTodoList();

    $scope.getTotalTodos = function () {
        return $scope.items.filter(function(value) { return !value.IsDone; }).length;
    };
    
    $scope.addTodo = function () {
        if (!$scope.formTodoText) return;
        var item = {};
        item.TodoText = $scope.formTodoText;
        item.IsDone = false;
        item.Order = 1;
        $scope.formTodoText = '';
        $http.post('/todoservice/api/values', JSON.stringify(item))
            .success(function() { $scope.updateTodoList(); });
    };

    $scope.deleteTodo = function () {
        $http.delete('/todoservice/api/values/' + this.item.TodoItemId)
            .success(function() { $scope.updateTodoList(); });
    }

    $scope.updateTodo = function() {
        $http.put('/todoservice/api/values/', this.item);
    }
});