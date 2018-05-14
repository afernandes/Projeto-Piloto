/*global angular*/
(function () {
    angular
        .module('semp.core')
        .factory('configurationService', configurationService);

    /* @ngInject */
    function configurationService($http) {
        var service = {
            getSettings: getSettings,
            updateSetting: updateSetting
        };
        return service;

        function getSettings() {
            return $http.get('api/appsettings');
        }

        function updateSetting(settings) {
            return $http.put('api/appsettings/', settings);
        }

        function shutdown() {
            return $http.post('api/appsettings/shutdown','')
        }
    }
})();