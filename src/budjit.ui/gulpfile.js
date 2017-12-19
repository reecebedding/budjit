var gulp = require('gulp');
var del = require('del');
var ts = require('gulp-typescript');
var runSequence = require('run-sequence');

var tsProject = ts.createProject('tsconfig.json');
var tsOutDir = tsProject.config.compilerOptions.outDir;

var paths = {
    libs: ['node_modules/angular2/bundles/angular2.js',
        'node_modules/angular2/bundles/angular2-polyfills.js',
        'node_modules/systemjs/dist/system.src.js',
        'node_modules/rxjs/bundles/Rx.js'
    ]
};

gulp.task('lib', function() {
	return gulp.src(paths.libs).pipe(gulp.dest(`${tsOutDir}/lib`));
});

gulp.task('clean', function () {
	return del([`${tsOutDir}/**/*`]);
});

gulp.task('build:ts', function() {
	var tsResult = tsProject.src().pipe(tsProject());
	return tsResult.js.pipe(gulp.dest(tsOutDir));
});

gulp.task('rebuild', function() {
	return runSequence('clean',['lib', 'build:ts']);
});

gulp.task('default', ['rebuild']);

gulp.task('watch', function() {
	return 	gulp.watch(tsProject.config.include, ['default']);
});
