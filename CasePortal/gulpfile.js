/// <binding />
'use strict';

const gulp = require('gulp');
const cleanCSSMinifier = require('gulp-clean-css');
const sass = require('gulp-dart-sass');
const terser = require('gulp-terser');
const del = require('del');
const sourcemaps = require('gulp-sourcemaps');

const sassFolderGlob = 'wwwroot/css/views/**/*.scss';

/**  https://github.com/terser/terser#minify-options  */
/** Used for all build targets != Development */
const terserProdOptions = {
    ecma: 2017,
    toplevel: true,
    keep_classnames: true,
    mangle: true,
    compress: {
        dead_code: true
    }
}

/** Adds sourcemap */
async function sassDevSassFolder() {
    return gulp.src(sassFolderGlob)
        .pipe(sourcemaps.init())
        .pipe(sass().on('error', sass.logError))
        .pipe(cleanCSSMinifier())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest('wwwroot/css/views/'))
}

/** Does not add sourcemap */
async function sassProdSassFolder() {
    return gulp.src(sassFolderGlob)
        .pipe(sass().on('error', sass.logError))
        .pipe(cleanCSSMinifier())
        .pipe(gulp.dest('wwwroot/css/views/'))
}


/** Minifies JS with Production settings 
 Passes all (transpiled) .js files through terser to optimize files for delivery */
async function minifyJsProd() {
    return gulp.src(['wwwroot/js/**/*.js', '!wwwroot/js/**/*.min.js'])
        .pipe(terser(terserProdOptions))
        .pipe(gulp.dest('wwwroot/js/'));
};


/** Watches for changes to SASS files and recompiles the file that changes */
async function watchCSS() {
    gulp.watch(sassFolderGlob, sassDevSassFolder);
}


/** Removes generated css files */
async function cleanCSS() {
    return del(['wwwroot/css/views/**/*.css']);
}

/** These are the ones that show up in Task Runner Explorer 
 NOTE: Series used even for single subtasks for consistency and because it gives automatic  
 informative output in Task Runner Explorer console */
gulp.task('watch:CSS', gulp.series(watchCSS));

gulp.task('minifyForDev', gulp.series(sassDevSassFolder));
gulp.task('minifyForProduction', gulp.series(sassProdSassFolder, minifyJsProd));

gulp.task('clean', gulp.series(cleanCSS));