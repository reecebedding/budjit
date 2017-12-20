var gulp = require('gulp');
var sass = require('gulp-sass');
var del = require('del');
var runSequence = require('run-sequence');

var config = {
	scssConfig: {
		bootstrapDir: 'node_modules/bootstrap-sass/assets/stylesheets',
		sourceDir: 'Styles',
		outDir: 'wwwroot/css'
	},
	fontsConfig: {
		bootstrapDir: 'node_modules/bootstrap-sass/assets/fonts',
		outDir: 'wwwroot/fonts'
	}
};

gulp.task('build:css', function(){
	return gulp.src(`${config.scssConfig.sourceDir}/*`)
				.pipe(sass({
					includePaths: [config.scssConfig.bootstrapDir]
				}))
				.pipe(gulp.dest(config.scssConfig.outDir));
});

gulp.task('build:fonts', function(){
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

gulp.task('rebuild', function() {
	return runSequence('clean',['build:css', 'build:fonts']);
});

gulp.task('default', ['rebuild']);