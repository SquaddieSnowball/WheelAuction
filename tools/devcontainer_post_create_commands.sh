#! /usr/bin/bash

# Generates a development certificate.
dotnet dev-certs https -ep ${HOME}/.aspnet/https/wheelauction.pfx -p password

# Creates "docker-compose.override.yml" as a symbolic link to "docker-compose.dev.linux.yml".
cd ./build/WheelAuction.DockerCompose
ln -s docker-compose.dev.linux.yml docker-compose.override.yml

# Restores solution projects.
cd ../..
dotnet restore