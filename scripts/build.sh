#!/bin/bash
cd ../src
list='EwalletService EwalletCommon EwalletTests'
for i in $list; do
    echo building: $i
    cd $i && dotnet restore && dotnet build && cd ..
done