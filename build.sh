#!/bin/sh
# 一键构建（Linux/macOS，同样只需要 dotnet SDK）
set -e
ROOT="$(cd "$(dirname "$0")" && pwd)"
export NUGET_PACKAGES="$ROOT/tools/nuget"
export DOTNET_CLI_TELEMETRY_OPTOUT=1
dotnet build "$ROOT/project/Manosaba.sln" --nologo -c "${1:-Debug}"
mkdir -p "$ROOT/dist"
find "$ROOT/project/src" -path "*/bin/*" -name "*.dll" -exec cp -f {} "$ROOT/dist/" \;
echo "-> $ROOT/dist"