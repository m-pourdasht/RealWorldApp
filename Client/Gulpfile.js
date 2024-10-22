const gulp = require('gulp');
const concat = require('gulp-concat');
const sass = require('gulp-sass')(require('sass')); // Specify the sass compiler
const uglify = require('gulp-uglify');


// Task to bundle CSS files output
gulp.task('bundle-css', function () {
    return gulp.src('gulp/src/scss/app.scss')
        .pipe(sass().on('error', sass.logError)) // Compile SCSS to CSS
        .pipe(concat('bundle.css'))
        .pipe(gulp.dest('wwwroot/assets/css'));
});

// Task to bundle JS files output
gulp.task('bundle-js', function () {
    return gulp.src('gulp/src/js/*.js')
        .pipe(concat('bundle.js'))
        .pipe(uglify())
        .pipe(gulp.dest('wwwroot/assets/js'));
});

// Watch task to monitor changes in CSS and JS files and trigger bundle tasks
gulp.task('watch', function () {
    gulp.watch('gulp/src/scss/*.scss', gulp.series('bundle-css'));
    gulp.watch('gulp/src/js/*.js', gulp.series('bundle-js'));
});

// Default task to run the watch task along with other tasks
gulp.task('default', gulp.series('bundle-css', 'bundle-js', 'watch'));
