/*global angular*/
(function () {
    'use strict';

    angular
        .module('semp.integrator', [])
        .config(['$stateProvider',
            function ($stateProvider) {
                $stateProvider
                    .state('integrator-order', {
                        url: '/integrator/order',
                        templateUrl: 'modules/integrator/admin/integrator/order-list.html',
                        controller: 'IntegratorListCtrl as vm'
                    })
                    .state('integrator-order-detail', {
                        url: '/integrator/order/detail/:id',
                        templateUrl: 'modules/integrator/admin/integrator/order-detail.html',
                        controller: 'IntegratorDetailCtrl as vm'
                    })
                ;
            }
        ]);
})();