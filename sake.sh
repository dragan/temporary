#!/usr/bin/env bash

set -e

install_package() {
  mono --runtime=v4.0.30319 .nuget/nuget.exe install $1 -Version $2 -o packages
}

install_packages() {
  install_package "sake" "0.2"
  install_package "Fixie" "0.0.1.120"
  install_package "Should" "1.1.20"
  install_package "NSubstitute" "1.6.1.0"
}

if [ ! -d "packages/sake.0.2" ]; then
  install_packages
fi

mono packages/Sake.0.2/tools/Sake.exe -f Sakefile.shade -I packages/Sake.0.2/tools/Shared "$@"
