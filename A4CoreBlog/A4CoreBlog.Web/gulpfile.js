/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify");

var paths = {
    webroot: "./wwwroot/"
};

paths.bootstrapCss = "./bower_components/bootstrap/dist/css/bootstrap.css";
paths.bootstrapThemeCss = "./bower_components/bootswatch/superhero/bootstrap.css";
paths.tetherCss = "./bower_components/tether/dist/css/tether.css";
paths.siteCss = "./areas/admin/css/site.css";

paths.jqueryJs = "./bower_components/jquery/dist/jquery.js";
paths.jqueryValidationJs = "./bower_components/jquery-validation/dist/jquery.validate.js";
paths.jqueryValidationUnobtrusiveJs = "./bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js";
paths.bootstrapJs = "./bower_components/bootstrap/dist/js/bootstrap.js";
paths.bootboxJs = "./bower_components/bootbox.js/bootbox.js";
paths.tetherJs = "./bower_components/tether/dist/js/tether.js";

paths.app = "./areas/admin/js/app.js";
paths.tmceJs = "./areas/admin/js/common/tmce.js";

paths.jsDest = paths.webroot + "js";
paths.cssDest = paths.webroot + "css";
paths.fontDest = paths.webroot + "fonts";

gulp.task("min:js", function () {
    return gulp.src([
        paths.jqueryJs,
        paths.tetherJs,
        paths.bootstrapJs,
        paths.bootboxJs,
        paths.app
    ])
        .pipe(concat(paths.jsDest + "/min/site.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:js/custom", function () {
    return gulp.src([
        paths.tmceJs
    ])
        .pipe(concat(paths.jsDest + "/custom/min/site.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

// Made so we can invoke validation only if needed where needed
gulp.task("min:js:validation", function () {
    return gulp.src([
        paths.jqueryValidationJs,
        paths.jqueryValidationUnobtrusiveJs
    ])
        .pipe(concat(paths.jsDest + "/min/validation.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("copy:js", function () {
    return gulp.src([
        paths.jqueryJs,
        paths.jqueryValidationJs,
        paths.jqueryValidationUnobtrusiveJs,
        paths.tetherJs,
        paths.bootstrapJs,
        paths.bootboxJs,
        paths.app,
    ])
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task("copy:js/custom", function () {
    return gulp.src([paths.tmceJs])
        .pipe(gulp.dest(paths.jsDest + '/custom'))
});

gulp.task("min:css", function () {
    return gulp.src([
        paths.bootstrapCss,
        paths.bootstrapThemeCss,
        paths.tetherCss,
        paths.siteCss
    ])
        .pipe(concat(paths.cssDest + "/min/site.min.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("copy:css", function () {
    return gulp.src([
        paths.bootstrapCss,
        paths.bootstrapThemeCss,
        paths.tetherCss,
        paths.siteCss
    ])
        .pipe(gulp.dest(paths.cssDest));
});

gulp.task("min", ["min:js", "min:js/custom", "min:js:validation", "min:css"]);
gulp.task("copy", ["copy:js", "copy:js/custom", "copy:css"]);

// vendor
paths.vendorCssDest = paths.webroot + "dist";

//paths.vendorBootstrapCss = "./node_modules/bootstrap/dist/css/bootstrap.css";
paths.vendorBootstrapCss = "./Content/bootstrap-flaty.min.css";

paths.vendorjQueryJs = "./node_modules/jquery/dist/js/jquery.js";
paths.vendorTetherJs = "./node_modules/tether/dist/js/tether.js";
paths.vendorBootstrapJs = "./node_modules/bootstrap/dist/js/bootstrap.js";

gulp.task("vendor:min:css", function () {
    return gulp.src([
        paths.vendorBootstrapCss
    ])
        .pipe(concat(paths.vendorCssDest + "/vendor.css"))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

//gulp.task("vendor:min:js", function () {
//    return gulp.src([
//        paths.vendorjQueryJs,
//        paths.vendorTetherJs
//    ])
//        .pipe(concat(paths.vendorCssDest + "/vendor.custom.js"))
//        .pipe(uglify())
//        .pipe(gulp.dest("."));
//});

gulp.task("vendor:build", ["vendor:min:css"]);