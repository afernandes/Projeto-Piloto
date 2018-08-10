/*global angular*/
(function () {
    angular
        .module('semp.integrator')
        .factory('orderService', orderService);

    /* @ngInject */
    function orderService($http) {
        var service = {
            getOrders: getOrders,
            getOrdersForGrid: getOrdersForGrid,
            getOrder: getOrder,
            getOrderStatus: getOrderStatus,
            changeOrderStatus: changeOrderStatus,
            getOrderHistory: getOrderHistory
        };
        return service;

        function getOrdersForGrid(params) {
            return $http.post('api/integrator/grid', params);
        }

        function getOrders(status, numRecords) {
            return $http.get('api/integrator?status=' + status + '&numRecords=' + numRecords);
        }

        function getOrder(orderId) {
            return $http.get('api/integrator/' + orderId);
        }

        function getOrderStatus() {
            return $http.get('api/integrator/order-status');
        }

        function changeOrderStatus(orderId, statusModel) {
            return $http.post('api/integrator/change-order-status/' + orderId, statusModel);
        }

        function getOrderHistory(orderId) {
            return $http.get('api/integrator/' + orderId + '/history');
        }
    }
})();