/*global angular*/
(function () {
    var adminApp = angular.module('semp', [
        'ui.router',
        'ngMaterial',
        'ngMessages',
        'smart-table',
        'ngFileUpload',
        /*'ui.bootstrap',
        'ui.bootstrap.datetimepicker',*/
        'ui.tree',
        'summernote',
        'colorpicker.module',
        'semp.common',
        'semp.dashboard',
        'semp.core',
        /*'semp.catalog',
        'semp.orders',*/
        'semp.cms',
        /*'semp.search',
        'semp.reviews',
        'semp.activityLog',
        'semp.vendors',*/
        'semp.localization'
        /*'semp.news',
        'semp.contacts',
        'semp.pricing',
        'semp.tax',
        'semp.shippings',
        'semp.shipping-tablerate',
        'semp.payments',
        'semp.paymentStripe',
        'semp.paymentPaypalExpress',
        'semp.inventory',
        'semp.shipment',
        'semp.paymentCoD'*/
    ]);

    toastr.options.closeButton = true;
    adminApp
        .config([
            '$urlRouterProvider', '$httpProvider',
            function ($urlRouterProvider, $httpProvider) {
                $urlRouterProvider.otherwise("/dashboard");

                $httpProvider.interceptors.push(function () {
                    return {
                        request: function (config) {
                            if (/modules.*admin.*\.html/i.test(config.url)) {
                                var separator = config.url.indexOf('?') === -1 ? '?' : '&';
                                config.url = config.url + separator + 'v=' + window.Global_AssetVersion;
                            }

                            return config;
                        }
                    };
                });
            }
        ]);
}());