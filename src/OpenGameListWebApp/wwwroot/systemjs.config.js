﻿(function (global) {
    // map tells the system loader where to look for things
    var map = {
        'app': 'app',                  // our application files
        '@angular': 'js/@angular',   // angular2 packages
        //'rxjs': 'https://npmcdn.com/rxjs@5.0.0-beta.6'
        'rxjs': 'js/rxjs'           // Rxjs package
    };

    // packages tells the Sytem loader which filename and/or extensions to look for by default (when none are specificed)
    var packages = {
        'app': { main: 'main.js', defaultExtension: 'js' },
        'rxjs': { defaultExtension: 'js' }
    };

    // configure @angular packages
    var ngPackageNames = [
        'common',
        'compiler',
        'core',
        'http',
        'platform-browser',
        'platform-browser-dynamic',
        'upgrade',
        'forms'
    ];

    function packIndex(pkgName) {
        packages['@angular/' + pkgName] = { main: 'index.js', defaultExtension: 'js' };
    };
    
    function packUmd(pkgName) {
        packages['@angular/' + pkgName] = { main: '/bundles/' + pkgName + '.umd.js', defaultExtension: 'js' };
    };

    var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;
    ngPackageNames.forEach(setPackageConfig);
    var config = {
        map: map,
        packages: packages
    }
    System.config(config);
})(this);