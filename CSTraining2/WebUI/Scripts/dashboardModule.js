require('angular');
require('angular-chart.js');
require('../node_modules/ng-table/bundles/ng-table.css');
require('../node_modules/ng-table/bundles/ng-table.js');
require('angular-ui-bootstrap');
module.exports = angular.module('dashboardApp', ['ngTable', 'chart.js', 'ui.bootstrap']);