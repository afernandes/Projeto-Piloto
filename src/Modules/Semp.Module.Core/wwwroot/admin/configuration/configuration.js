﻿/*global angular, jQuery*/
(function ($) {
    angular
        .module('semp.core')
        .controller('ConfigurationCtrl', ConfigurationCtrl);

    /* @ngInject */
    function ConfigurationCtrl($state, configurationService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.settings = {};

        vm.save = function save() {
            vm.validationErrors = [];
            configurationService.updateSetting(vm.settings)
                .then(function (result) {
                    toastr.success('Application settings have been saved');
                })
                .catch(function (response) {
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push('Could not save settings.');
                    }
                });
        };

        vm.shutdown = function shutdown() {
            vm.validationErrors = [];
            configurationService.shutdown()
                .then(function (result) {
                    toastr.success('Application restarted.');
                })
                .catch(function (response) {
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push('Could not Application restarted.');
                    }
                });
        }

        function init() {
            configurationService.getSettings().then(function (result) {
                vm.settings = result.data;
            });
        }

        init();
    }
})(jQuery);