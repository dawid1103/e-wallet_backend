#!/bin/bash
cd ../src/EwalletCommon
dotnet pack -o ../../nuget --no-build --no-restore --include-source --version-suffix preview