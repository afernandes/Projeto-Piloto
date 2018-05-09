/*global angular*/
(function () {
    'use strict';

    angular
        .module('semp.dashboard', [])
        .config(['$stateProvider', function ($stateProvider) {
            $stateProvider.state('dashboard', {
                url: '/dashboard',
                templateUrl: "/admin/dashboard-tpl"
            });
        }]);
})();