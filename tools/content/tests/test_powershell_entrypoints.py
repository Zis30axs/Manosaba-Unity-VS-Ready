"""Exercise the public wrappers with the actual Windows PowerShell engines."""
import json
import os
from pathlib import Path
import shutil
import subprocess
import sys
import tempfile
import unittest


CONTENT = Path(__file__).resolve().parents[1]


@unittest.skipUnless(os.name == "nt", "Windows PowerShell entrypoints")
class PowerShellEntrypointTests(unittest.TestCase):
    def setUp(self):
        self.directory = tempfile.TemporaryDirectory(prefix="content wrapper ")
        self.addCleanup(self.directory.cleanup)
        root = Path(self.directory.name)
        capture = root / "capture.py"
        capture.write_text(
            "import json, os, sys\n"
            "print(json.dumps(sys.argv[1:]))\n"
            "sys.exit(int(os.environ.get('CONTENT_TEST_EXIT', '0')))\n",
            encoding="utf-8",
        )
        self.interpreter = root / "python recorder.cmd"
        self.interpreter.write_text(
            f'@echo off\n"{sys.executable}" "{capture}" %*\n',
            encoding="utf-8",
        )
        self.shells = [shutil.which(name) for name in ("powershell", "pwsh")]
        self.shells = [shell for shell in self.shells if shell]
        if not self.shells:
            self.skipTest("No PowerShell engine installed")

    def invoke(self, shell, command, arguments=(), exit_code=0):
        env = dict(os.environ, MANOSABA_PYTHON=str(self.interpreter),
                   CONTENT_TEST_EXIT=str(exit_code))
        result = subprocess.run(
            [shell, "-NoProfile", "-ExecutionPolicy", "Bypass", "-File",
             str(CONTENT / (command + ".ps1")), *arguments],
            env=env, capture_output=True, text=True, timeout=30,
        )
        self.assertEqual(result.returncode, exit_code, result.stdout + result.stderr)
        self.assertEqual(result.stderr.strip(), "", result.stderr)
        return json.loads(result.stdout)

    def test_no_arguments_for_every_entrypoint(self):
        for shell in self.shells:
            for command in ("import", "sync", "doctor", "build"):
                with self.subTest(shell=shell, command=command):
                    self.assertEqual(self.invoke(shell, command),
                                     [str(CONTENT / "content.py"), command])

    def test_aliases_python_flags_and_paths_with_spaces(self):
        cases = (
            ("sync", ["-DryRun", "-Category", "SPRITE", "-File", "res/a b.png"],
             ["--dry-run", "--category", "SPRITE", "--file", "res/a b.png"]),
            ("doctor", ["--deep"], ["--deep"]),
            ("import", ["D:/Game with spaces", "-OverwriteUserChanges"],
             ["D:/Game with spaces", "--overwrite-user-changes"]),
            ("build", ["-Player"], ["--player"]),
        )
        for shell in self.shells:
            for command, arguments, expected in cases:
                with self.subTest(shell=shell, command=command):
                    self.assertEqual(self.invoke(shell, command, arguments),
                                     [str(CONTENT / "content.py"), command, *expected])

    def test_failure_exit_code_reaches_caller(self):
        for shell in self.shells:
            with self.subTest(shell=shell):
                self.invoke(shell, "sync", exit_code=7)


if __name__ == "__main__":
    unittest.main()
