@echo off
rem 双击即可构建（只需要 .NET SDK）
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0build.ps1" %*
if errorlevel 1 pause