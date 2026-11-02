const assert = require('node:assert/strict');
const fs = require('node:fs');
const os = require('node:os');
const path = require('node:path');
const { execFileSync } = require('node:child_process');
const { test } = require('node:test');

const project = path.resolve(__dirname, '..');
const gulp = path.join(path.dirname(require.resolve('gulp')), 'bin', 'gulp.js');

test('Gulp tasks finish copying, preserve binaries, clean stale modules, and generate assets', () => {
    const fixture = fs.mkdtempSync(path.join(os.tmpdir(), 'semp-gulp-'));
    const host = path.join(fixture, 'WebHost');
    const write = (relative, content) => {
        const target = path.join(fixture, relative);
        fs.mkdirSync(path.dirname(target), { recursive: true });
        fs.writeFileSync(target, content);
    };
    try {
        write('WebHost/gulpfile.js', fs.readFileSync(path.join(project, 'gulpfile.js')));
        write('Modules/Semp.Module.Fixture/module.json', JSON.stringify({
            name: 'fixture', fullName: 'Semp.Module.Fixture', isBundledWithHost: false
        }));
        write('Modules/Semp.Module.Fixture/Views/Home/Index.cshtml', 'fixture view');
        write('Modules/Semp.Module.Fixture/wwwroot/fixture.js', 'fixture asset');
        const binary = Buffer.from([0x4d, 0x5a, 0x00, 0xff, 0xfe, 0x80, 0x42]);
        write('Modules/Semp.Module.Fixture/bin/Debug/netcoreapp2.2/win-x64/fixture.dll', binary);
        const releaseBinary = Buffer.from([0x4d, 0x5a, 0xff, 0x00]);
        write('Modules/Semp.Module.Fixture/bin/Release/netcoreapp2.2/win-x64/fixture.dll', releaseBinary);
        write('Modules/Semp.Module.Portable/module.json', JSON.stringify({
            name: 'portable', fullName: 'Semp.Module.Portable', isBundledWithHost: false
        }));
        write('Modules/Semp.Module.Portable/bin/Debug/netcoreapp2.2/portable.dll', binary);
        write('WebHost/Modules/stale/stale.txt', 'stale');
        write('WebHost/wwwroot/js/app.js', 'var example = 42;');
        write('WebHost/wwwroot/css/site.css', 'body { color: red; }');
        write('WebHost/bower_components/jquery/dist/jquery.js', 'jquery fixture');
        const run = (...tasks) => execFileSync(process.execPath, [gulp, ...tasks], {
            cwd: host,
            env: { ...process.env, NODE_PATH: path.join(project, 'node_modules') },
            stdio: 'pipe', timeout: 30000
        });

        run('copy-modules');
        assert.equal(fs.existsSync(path.join(host, 'Modules/stale')), false);
        assert.equal(fs.readFileSync(path.join(host, 'Modules/Semp.Module.Fixture/Views/Home/Index.cshtml'), 'utf8'), 'fixture view');
        assert.deepEqual(fs.readFileSync(path.join(host, 'Modules/Semp.Module.Fixture/bin/fixture.dll')), binary);
        assert.deepEqual(fs.readFileSync(path.join(host, 'Modules/Semp.Module.Portable/bin/portable.dll')), binary);
        assert.equal(fs.readFileSync(path.join(host, 'wwwroot/modules/fixture/fixture.js'), 'utf8'), 'fixture asset');

        run('min', 'copy-lib');
        assert.match(fs.readFileSync(path.join(host, 'wwwroot/js/site.min.js'), 'utf8'), /example = 42/);
        assert.match(fs.readFileSync(path.join(host, 'wwwroot/css/site.min.css'), 'utf8'), /color:red/);
        assert.equal(fs.readFileSync(path.join(host, 'wwwroot/lib/jquery/jquery.js'), 'utf8'), 'jquery fixture');
        run('copy-modules', '--configurationName', 'Release');
        assert.deepEqual(fs.readFileSync(path.join(host, 'Modules/Semp.Module.Fixture/bin/fixture.dll')), releaseBinary);
        run('clean-modules');
        assert.equal(fs.existsSync(path.join(host, 'Modules/Semp.Module.Fixture')), false);
    } finally {
        // This exact path was created by mkdtemp above and contains only test fixtures.
        const temporaryRoot = path.resolve(os.tmpdir()) + path.sep;
        assert.ok(path.resolve(fixture).startsWith(temporaryRoot));
        fs.rmSync(fixture, { recursive: true, force: true });
    }
});
