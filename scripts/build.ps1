cd ../src
$list = @("EwalletService","EwalletCommon","EwalletTests")
foreach ($i in $list){
    echo building: $i
    cd $i
    dotnet restore
    dotnet build
    cd ..
}