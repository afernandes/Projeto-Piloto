/// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    concat = require("gulp-concat"),
    cssmin = require("gulp-clean-css"),
    fs = require('fs'),
    path = require('path'),
    argv = require('util').parseArgs({
        options: { configurationName: { type: 'string', default: 'Debug' } },
        strict: false, allowPositionals: true
    }).values,
    pipeline = require('stream').pipeline;

var buildConfigurationName = argv.configurationName || 'Debug';

var paths = {
    host: {
        wwwroot: "./wwwroot/",
        wwwrootModules: "./wwwroot/modules/",
        modules: "./Modules/",
        moduleBin: "/bin/",
        bower: "./bower_components/"
    },
    dev: {
        modules: "../Modules/",
        moduleBin: "/bin/" + buildConfigurationName + "/netcoreapp2.2/"
    }
};

paths.host.js = paths.host.wwwroot + "js/**/*.js";
paths.host.minJs = paths.host.wwwroot + "js/**/*.min.js";
paths.host.css = paths.host.wwwroot + "css/**/*.css";
paths.host.minCss = paths.host.wwwroot + "css/**/*.min.css";
paths.host.concatJsDest = paths.host.wwwroot + "js/site.min.js";
paths.host.concatCssDest = paths.host.wwwroot + "css/site.min.css";
paths.host.lib = paths.host.wwwroot + "lib/";

var bower = {
    "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,woff2,eot}",
    "bootstrap-ui-datetime-picker": "bootstrap-ui-datetime-picker/dist/*.*",
    "bootstrap-social": "bootstrap-social/*.css",
    "i18n": "angular-i18n/angular-locale*.js",
    "bootstrap-star-rating": "bootstrap-star-rating/**/*.{js,css,gif}",
    "font-awesome": "components-font-awesome/**/*.{css,ttf,svg,woff,woff2,eot,otf}",
    "jquery": "jquery/dist/jquery*.{js,map}",
    "jquery-validation": "jquery-validation/dist/*.js",
    "jquery-validation-unobtrusive": "jquery-validation-unobtrusive/src/*.js",
    "nouislider": "nouislider/distribute/*.{js,css}",
    "wnumb": "wnumb/wNumb.js",
    "angular": "angular/angular.js",
    "angular-animate": "angular-animate/angular*.js",
    "angular-aria": "angular-aria/angular*.js",
    "angular-material": "angular-material/angular-material*.{js,css}",
    "angular-messages": "angular-messages/angular-messages.js",
    "angular-ui-router": "angular-ui-router/release/*.js",
    "angular-smart-table": "angular-smart-table/dist/*.js",
    "ng-file-upload": "ng-file-upload/ng-file-upload.js",
    "summernote": "summernote/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
    "angular-summernote": "angular-summernote/dist/*.js",
    "angular-bootstrap": "angular-bootstrap/ui-bootstrap*",
    "matchheight": "matchheight/dist/*.js",
    "toastr": "toastr/toastr*.{js,css}",
    "bootbox": "bootbox/bootbox*.{js,css}",
    "angular-ui-tree": "angular-ui-tree/dist/*.*",
    "angular-bootstrap-colorpicker": "angular-bootstrap-colorpicker/{js,css,img}/*.*",
    "popper": "popper.js/dist/**/*.*"
};

var modules = loadModules();

function loadModules() {
    var moduleManifestPaths,
        modules = [];

    moduleManifestPaths = fs.readdirSync(paths.dev.modules, { withFileTypes: true })
        .filter(function (entry) { return entry.isDirectory() && entry.name.includes('.'); })
        .map(function (entry) { return path.resolve(paths.dev.modules, entry.name, 'module.json'); })
        .filter(function (manifest) { return fs.existsSync(manifest); });
    moduleManifestPaths.forEach(function (moduleManifestPath) {
        var moduleManifest = require(moduleManifestPath);
        modules.push(moduleManifest);
    });

    return modules;
}

gulp.task('clean-module', function (done) {
    [paths.host.modules, paths.host.wwwrootModules].forEach(function (directory) {
        fs.rmSync(directory, { recursive: true, force: true });
    });
    done();
});

function copyFiles(patterns, destination, options) {
    return new Promise(function (resolve, reject) {
        // Gulp 5 defaults to UTF-8; preserve DLLs, fonts and images byte-for-byte.
        var streams = [gulp.src(patterns, Object.assign({ encoding: false }, options))];
        streams.push(gulp.dest(destination));
        streams.push(function (error) { if (error) reject(error); else resolve(); });
        pipeline.apply(null, streams);
    });
}

gulp.task('copy-static', function () {
    var copies = [];
    modules.forEach(function (module) {
        console.log('copying static contents ' + paths.dev.modules + module.fullName);
        copies.push(copyFiles([paths.dev.modules + module.fullName + '/Views/**/*.*',
            paths.dev.modules + module.fullName + '/module.json'],
            paths.host.modules + module.fullName, { base: paths.dev.modules + module.fullName }));
        copies.push(copyFiles(paths.dev.modules + module.fullName + '/wwwroot/**/*.*',
            paths.host.wwwrootModules + module.name));
    });

    copies.push(copyFiles(paths.dev.modules + 'Semp.Module.SampleData/SampleContent/**/*.*',
        paths.host.modules + 'Semp.Module.SampleData/SampleContent'));
    return Promise.all(copies);
});

gulp.task('copy-module-binaries', function () {
    var copies = [];
    modules.forEach(function (module) {
        if (!module.isBundledWithHost) {
            var output = paths.dev.modules + module.fullName + paths.dev.moduleBin;
            if (fs.existsSync(output + 'win-x64/')) output += 'win-x64/';
            console.log('copying ' + output);
            copies.push(copyFiles(output + '**/*.*',
                paths.host.modules + module.fullName + paths.host.moduleBin));
        }
    });
    return Promise.all(copies);
});

gulp.task('clean-modules', gulp.series('clean-module'));
gulp.task('copy-modules', gulp.series('clean-module', gulp.parallel('copy-static', 'copy-module-binaries')));

gulp.task("clean:js", function (cb) {
    fs.rm(paths.host.concatJsDest, { force: true }, cb);
});

gulp.task("clean:css", function (cb) {
    fs.rm(paths.host.concatCssDest, { force: true }, cb);
});

gulp.task("clean:lib", function (done) {
    for (var desDir in bower)
        fs.rmSync(paths.host.lib + desDir, { recursive: true, force: true });
    done();
});

gulp.task("copy-lib", gulp.series("clean:lib", function () {
    var copies = [];
    for (var desDir in bower)
        copies.push(copyFiles([paths.host.bower + bower[desDir], '!' + paths.host.bower + '**/npm.js'],
            paths.host.lib + desDir, { allowEmpty: true }));
    return Promise.all(copies);
}));

gulp.task("min:js", function () {
    return gulp.src([paths.host.js, "!" + paths.host.minJs], { base: "." })
        .pipe(concat(paths.host.concatJsDest))
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.host.css, "!" + paths.host.minCss])
        .pipe(concat(paths.host.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", gulp.parallel("min:js", "min:css"));
