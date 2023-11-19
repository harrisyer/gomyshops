var gulpfile = require('./gulpfile');
const { task } = require('gulp');
const purgecss = require('gulp-purgecss');
var gulp = require('gulp');
//var less = require('gulp-less');
//var babel = require('gulp-babel');
//var concat = require('gulp-concat');
//var uglify = require('gulp-uglify');
//var rename = require('gulp-rename');
//var cleanCSS = require('gulp-clean-css');
//var del = require('del');

//var paths = {
//    webroot: "./wwwroot/"
//};

//paths.js = paths.webroot + "js/**/*.js";
//paths.minJs = paths.webroot + "js/**/*.min.js";
//paths.css = paths.webroot + "css/**/*.css";
//paths.minCss = paths.webroot + "css/**/*.min.css";
//paths.concatJsDest = paths.webroot + "js/site.min.js";
//paths.concatCssDest = paths.webroot + "css/site.min.css";

task('testTask', function (done) {
    console.log('Hello World! We finished a task!11111');
    done();
});

//function testTask(done) {
//    console.log('Hello World! We finished a task!');
//    done();
//}



task('purgecss', () => {
    return gulp.src(['./wwwroot/css/site.css'])
        .pipe(purgecss({
            content: ['./Views/**/*.cshtml', './Pages/**/*.cshtml'],
            safelist: [/modal-.*/]
        }))
        .pipe(gulp.dest('./wwwroot/gulpcss'))
})

task('formhelpers', () => {
    return gulp.src(['./wwwroot/css/cssbootstrapformhelpers.css'])
        .pipe(purgecss({
            content: ['./Views/**/*.cshtml', './Pages/**/*.cshtml'],
            safelist: [/modal-.*/]
        }))
        .pipe(gulp.dest('./wwwroot/gulpcss'))
})


exports.purgecss = 'purgecss';
exports.testTask = 'testTask';
exports.formhelpers = 'formhelpers';
/*exports.default = testTask*/