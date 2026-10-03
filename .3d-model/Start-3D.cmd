@echo off
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0Build-3D.ps1"
if errorlevel 1 pause
