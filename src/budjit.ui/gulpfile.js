var gulp = require('gulp');
var sass = require('gulp-sass');
var util = require('gulp-util');
var rename = require('gulp-rename');
var del = require('del');
var ts = require('gulp-typescript');
var runSequence = require('run-sequence');

var tsProject = ts.createProject('tsconfig.json');

var config = {
	devEnv: 'Development',
	scssConfig: {
		bootstrapDir: 'node_modules/bootstrap-sass/assets/stylesheets',
		sourceDir: 'Styles',
		outDir: 'wwwroot/css',
		options: function() {
			let sassOpt = {
				includePaths: [config.scssConfig.bootstrapDir]
			}
			return process.env.ASPNETCORE_ENVIRONMENT !== config.devEnv
				? Object.assign(sassOpt, {outputStyle: 'compressed'})
				: sassOpt;
		}
	},
	fontsConfig: {
		bootstrapDir: 'node_modules/bootstrap-sass/assets/fonts',
		outDir: 'wwwroot/fonts'
	},
	jsConfig: {
		outDir: tsProject.config.compilerOptions.outDir,
		libs: [
			'node_modules/bootstrap/dist/js/bootstrap.js',
			'node_modules/jquery/dist/jquery.js'
		]
	}
};

function renameIfNeeded() {
	return process.env.ASPNETCORE_ENVIRONMENT !== config.devEnv
	? rename({
		suffix: '.min'
	})
	: util.noop();
}

gulp.task('build:css', function () {
	return gulp.src(`${config.scssConfig.sourceDir}/*`)
		.pipe(sass(config.scssConfig.options()))
		.pipe(renameIfNeeded())
		.pipe(gulp.dest(config.scssConfig.outDir));
});

gulp.task('build:fonts', function () {
	return gulp.src(`${config.fontsConfig.bootstrapDir}/**/*`)
		.pipe(gulp.dest(config.fontsConfig.outDir))
});

gulp.task('build:js', ['lib:js'], function() {
	var tsResult = tsProject.src().pipe(tsProject());
	return tsResult.js.pipe(gulp.dest(config.jsConfig.outDir));
});

gulp.task('lib:js', function() {
	return gulp.src(config.jsConfig.libs).pipe(gulp.dest(`${config.jsConfig.outDir}/lib`));
});

gulp.task('clean:css', function () {
	return del(`${config.scssConfig.outDir}/**/*`);
});

gulp.task('clean:fonts', function () {
	return del(`${config.fontsConfig.outDir}/**/*`);
});

gulp.task('clean:js', function(){
	return del(`${config.jsConfig.outDir}/**/*`)
});

gulp.task('clean', ['clean:css', 'clean:fonts', 'clean:js']);

gulp.task('rebuild', function () {
	return runSequence('clean', ['build:css', 'build:fonts', 'build:js']);
});

gulp.task('default', ['rebuild']);
