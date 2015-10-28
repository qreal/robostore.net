var gulp = require('gulp');
var rimraf = require('rimraf');

var paths = {
    bower: "./bower_components/",
    lib: "./wwwroot/lib/"
};

gulp.task('clean', function (callback) {
    rimraf(paths.lib, callback);
});

gulp.task('default', ['clean'], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "jquery": "jquery/jquery*.{js,map}",
        "jquery-validation": "jquery-validation/jquery.validate.js",
        "jquery-validation-unobtrusive":
                "jquery-validation-unobtrusive/jquery.validate.unobtrusive.js"
    };

    for (var destinationDir in bower) {
        gulp.src(paths.bower + bower[destinationDir])
                .pipe(gulp.dest(paths.lib + destinationDir));
    }
});