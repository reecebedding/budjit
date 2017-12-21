var gulp = require('gulp');
var sass = require('gulp-sass');
var util = require('gulp-util');
var rename = require('gulp-rename');
var del = require('del');
var runSequence = require('run-sequence');

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

gulp.task('clean:css', function () {
	return del(`${config.scssConfig.outDir}/**/*`);
});

gulp.task('clean:fonts', function () {
	return del(`${config.fontsConfig.outDir}/**/*`);
});

gulp.task('clean', ['clean:css', 'clean:fonts']);

gulp.task('rebuild', function () {
	return runSequence('clean', ['build:css', 'build:fonts']);
});

gulp.task('default', ['rebuild']);